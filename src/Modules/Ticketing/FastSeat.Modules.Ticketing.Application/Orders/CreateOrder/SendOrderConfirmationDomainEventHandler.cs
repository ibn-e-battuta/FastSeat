using FastSeat.Common.Application.Exceptions;
using FastSeat.Common.Application.Messaging;
using FastSeat.Common.Domain;
using FastSeat.Modules.Ticketing.Application.Orders.GetOrder;
using FastSeat.Modules.Ticketing.Domain.Orders;
using MediatR;

namespace FastSeat.Modules.Ticketing.Application.Orders.CreateOrder;

internal sealed class SendOrderConfirmationDomainEventHandler(ISender sender)
    : DomainEventHandler<OrderCreatedDomainEvent>
{
    public override async Task Handle(
        OrderCreatedDomainEvent notification,
        CancellationToken cancellationToken = default)
    {
        Result<OrderResponse> result = await sender.Send(new GetOrderQuery(notification.OrderId), cancellationToken);

        if (result.IsFailure)
        {
            throw new FastSeatException(nameof(GetOrderQuery), result.Error);
        }

        // Send order confirmation notification.
    }
}
