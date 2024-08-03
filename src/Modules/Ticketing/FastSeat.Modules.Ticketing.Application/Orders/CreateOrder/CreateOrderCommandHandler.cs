﻿using System.Data.Common;
using FastSeat.Common.Application.Messaging;
using FastSeat.Common.Domain;
using FastSeat.Modules.Ticketing.Application.Abstractions.Data;
using FastSeat.Modules.Ticketing.Application.Abstractions.Payments;
using FastSeat.Modules.Ticketing.Application.Carts;
using FastSeat.Modules.Ticketing.Domain.Customers;
using FastSeat.Modules.Ticketing.Domain.Events;
using FastSeat.Modules.Ticketing.Domain.Orders;
using FastSeat.Modules.Ticketing.Domain.Payments;

namespace FastSeat.Modules.Ticketing.Application.Orders.CreateOrder;

internal sealed class CreateOrderCommandHandler(
    ICustomerRepository customerRepository,
    IOrderRepository orderRepository,
    ITicketTypeRepository ticketTypeRepository,
    IPaymentRepository paymentRepository,
    IPaymentService paymentService,
    CartService cartService,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateOrderCommand>
{
    public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        await using DbTransaction transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

        Customer? customer = await customerRepository.GetAsync(request.CustomerId, cancellationToken);

        if (customer is null)
        {
            return Result.Failure(CustomerErrors.NotFound(request.CustomerId));
        }

        var order = Order.Create(customer);

        Cart cart = await cartService.GetAsync(customer.Id, cancellationToken);

        if (!cart.Items.Any())
        {
            return Result.Failure(CartErrors.Empty);
        }

        foreach (CartItem cartItem in cart.Items)
        {
            // This acquires a pessimistic lock or throws an exception if already locked.
            TicketType? ticketType = await ticketTypeRepository.GetWithLockAsync(
                cartItem.TicketTypeId,
                cancellationToken);

            if (ticketType is null)
            {
                return Result.Failure(TicketTypeErrors.NotFound(cartItem.TicketTypeId));
            }

            Result result = ticketType.UpdateQuantity(cartItem.Quantity);

            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }

            order.AddItem(ticketType, cartItem.Quantity, cartItem.Price, ticketType.Currency);
        }

        orderRepository.Insert(order);

        // We're faking a payment gateway request here...
        PaymentResponse paymentResponse = await paymentService.ChargeAsync(order.TotalPrice, order.Currency);

        var payment = Payment.Create(
            order,
            paymentResponse.TransactionId,
            paymentResponse.Amount,
            paymentResponse.Currency);

        paymentRepository.Insert(payment);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        await cartService.ClearAsync(customer.Id, cancellationToken);

        return Result.Success();
    }
}
