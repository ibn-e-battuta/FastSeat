using FastSeat.Common.Application.EventBus;
using FastSeat.Common.Application.Messaging;
using FastSeat.Modules.Events.Domain.Events;
using FastSeat.Modules.Events.IntegrationEvents;

namespace FastSeat.Modules.Events.Application.Events.RescheduleEvent;

internal sealed class EventRescheduledDomainEventHandler(IEventBus eventBus)
    : DomainEventHandler<EventRescheduledDomainEvent>
{
    public override async Task Handle(
        EventRescheduledDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await eventBus.PublishAsync(
            new EventRescheduledIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                domainEvent.EventId,
                domainEvent.StartsAtUtc,
                domainEvent.EndsAtUtc),
            cancellationToken);
    }
}
