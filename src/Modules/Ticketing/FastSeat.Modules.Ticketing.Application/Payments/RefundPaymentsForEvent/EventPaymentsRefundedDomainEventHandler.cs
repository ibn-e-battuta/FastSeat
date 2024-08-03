using FastSeat.Common.Application.EventBus;
using FastSeat.Common.Application.Messaging;
using FastSeat.Modules.Ticketing.Domain.Events;
using FastSeat.Modules.Ticketing.IntegrationEvents;

namespace FastSeat.Modules.Ticketing.Application.Payments.RefundPaymentsForEvent;

internal sealed class EventPaymentsRefundedDomainEventHandler(IEventBus eventBus)
     : DomainEventHandler<EventPaymentsRefundedDomainEvent>
{
    public override async Task Handle(
        EventPaymentsRefundedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await eventBus.PublishAsync(
            new EventPaymentsRefundedIntegrationEvent(
                domainEvent.EventId,
                domainEvent.OccurredOnUtc,
                domainEvent.EventId),
            cancellationToken);
    }
}
