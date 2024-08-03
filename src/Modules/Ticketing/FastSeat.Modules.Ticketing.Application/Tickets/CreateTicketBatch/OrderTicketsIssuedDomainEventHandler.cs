using FastSeat.Common.Application.Exceptions;
using FastSeat.Common.Application.Messaging;
using FastSeat.Common.Domain;
using FastSeat.Modules.Ticketing.Application.Tickets.GetTicket;
using FastSeat.Modules.Ticketing.Application.Tickets.GetTicketForOrder;
using FastSeat.Modules.Ticketing.Domain.Orders;
using MediatR;

namespace FastSeat.Modules.Ticketing.Application.Tickets.CreateTicketBatch;

internal sealed class OrderTicketsIssuedDomainEventHandler(ISender sender)
    : DomainEventHandler<OrderTicketsIssuedDomainEvent>
{
    public override async Task Handle(
        OrderTicketsIssuedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        Result<IReadOnlyCollection<TicketResponse>> result = await sender.Send(
            new GetTicketsForOrderQuery(domainEvent.OrderId), cancellationToken);

        if (result.IsFailure)
        {
            throw new FastSeatException(nameof(GetTicketsForOrderQuery), result.Error);
        }

        // Send ticket confirmation notification.
    }
}
