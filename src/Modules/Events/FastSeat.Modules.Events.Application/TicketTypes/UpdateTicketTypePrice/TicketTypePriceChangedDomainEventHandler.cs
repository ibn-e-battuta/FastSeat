using FastSeat.Common.Application.EventBus;
using FastSeat.Common.Application.Messaging;
using FastSeat.Modules.Events.Domain.TicketTypes;
using FastSeat.Modules.Events.IntegrationEvents;

namespace FastSeat.Modules.Events.Application.TicketTypes.UpdateTicketTypePrice;

internal sealed class TicketTypePriceChangedDomainEventHandler(IEventBus eventBus)
     : DomainEventHandler<TicketTypePriceChangedDomainEvent>
{
    public override async Task Handle(
        TicketTypePriceChangedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await eventBus.PublishAsync(
            new TicketTypePriceChangedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                domainEvent.TicketTypeId,
                domainEvent.Price),
            cancellationToken);
    }
}
