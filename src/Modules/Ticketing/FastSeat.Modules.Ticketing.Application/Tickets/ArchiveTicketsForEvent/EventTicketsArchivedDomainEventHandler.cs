using FastSeat.Common.Application.EventBus;
using FastSeat.Common.Application.Messaging;
using FastSeat.Modules.Ticketing.Domain.Events;
using FastSeat.Modules.Ticketing.IntegrationEvents;

namespace FastSeat.Modules.Ticketing.Application.Tickets.ArchiveTicketsForEvent;

internal sealed class EventTicketsArchivedDomainEventHandler(IEventBus eventBus)
     : DomainEventHandler<EventTicketsArchivedDomainEvent>
{
    public override async Task Handle(
        EventTicketsArchivedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await eventBus.PublishAsync(
            new EventTicketsArchivedIntegrationEvent(
                domainEvent.EventId,
                domainEvent.OccurredOnUtc,
                domainEvent.EventId),
            cancellationToken);
    }
}
