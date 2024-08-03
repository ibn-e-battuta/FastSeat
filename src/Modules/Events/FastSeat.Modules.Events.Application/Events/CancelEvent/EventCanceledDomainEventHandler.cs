using FastSeat.Common.Application.EventBus;
using FastSeat.Common.Application.Messaging;
using FastSeat.Modules.Events.Domain.Events;
using FastSeat.Modules.Events.IntegrationEvents;

namespace FastSeat.Modules.Events.Application.Events.CancelEvent;

internal sealed class EventCanceledDomainEventHandler(IEventBus eventBus)
    : DomainEventHandler<EventCanceledDomainEvent>
{
    public override async Task Handle(
        EventCanceledDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await eventBus.PublishAsync(
            new EventCanceledIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                domainEvent.EventId),
            cancellationToken);
    }
}
