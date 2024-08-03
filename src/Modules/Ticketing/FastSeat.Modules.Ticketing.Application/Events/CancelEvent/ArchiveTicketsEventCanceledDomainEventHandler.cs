using FastSeat.Common.Application.Exceptions;
using FastSeat.Common.Application.Messaging;
using FastSeat.Common.Domain;
using FastSeat.Modules.Ticketing.Application.Tickets.ArchiveTicketsForEvent;
using FastSeat.Modules.Ticketing.Domain.Events;
using MediatR;

namespace FastSeat.Modules.Ticketing.Application.Events.CancelEvent;

internal sealed class ArchiveTicketsEventCanceledDomainEventHandler(ISender sender)
    : DomainEventHandler<EventCanceledDomainEvent>
{
    public override async Task Handle(
        EventCanceledDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new ArchiveTicketsForEventCommand(domainEvent.EventId), cancellationToken);

        if (result.IsFailure)
        {
            throw new FastSeatException(nameof(ArchiveTicketsForEventCommand), result.Error);
        }
    }
}
