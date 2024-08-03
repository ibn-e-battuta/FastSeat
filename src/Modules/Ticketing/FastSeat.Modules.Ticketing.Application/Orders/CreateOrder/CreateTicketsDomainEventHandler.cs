using FastSeat.Common.Application.Exceptions;
using FastSeat.Common.Application.Messaging;
using FastSeat.Common.Domain;
using FastSeat.Modules.Ticketing.Application.Tickets.CreateTicketBatch;
using FastSeat.Modules.Ticketing.Domain.Orders;
using MediatR;

namespace FastSeat.Modules.Ticketing.Application.Orders.CreateOrder;

internal sealed class CreateTicketsDomainEventHandler(ISender sender)
    : DomainEventHandler<OrderCreatedDomainEvent>
{
    public override async Task Handle(
        OrderCreatedDomainEvent notification,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new CreateTicketBatchCommand(notification.OrderId), cancellationToken);

        if (result.IsFailure)
        {
            throw new FastSeatException(nameof(CreateTicketBatchCommand), result.Error);
        }
    }
}
