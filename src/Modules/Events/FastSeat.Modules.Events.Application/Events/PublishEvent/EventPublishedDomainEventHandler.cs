using FastSeat.Common.Application.EventBus;
using FastSeat.Common.Application.Exceptions;
using FastSeat.Common.Application.Messaging;
using FastSeat.Common.Domain;
using FastSeat.Modules.Events.Application.Events.GetEvent;
using FastSeat.Modules.Events.Domain.Events;
using FastSeat.Modules.Events.IntegrationEvents;
using MediatR;

namespace FastSeat.Modules.Events.Application.Events.PublishEvent;

internal sealed class EventPublishedDomainEventHandler(ISender sender, IEventBus eventBus)
    : DomainEventHandler<EventPublishedDomainEvent>
{
    public override async Task Handle(
        EventPublishedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        Result<EventResponse> result = await sender.Send(new GetEventQuery(domainEvent.EventId), cancellationToken);

        if (result.IsFailure)
        {
            throw new FastSeatException(nameof(GetEventQuery), result.Error);
        }

        await eventBus.PublishAsync(
            new EventPublishedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                result.Value.Id,
                result.Value.Title,
                result.Value.Description,
                result.Value.Location,
                result.Value.StartsAtUtc,
                result.Value.EndsAtUtc,
                result.Value.TicketTypes.Select(t => new TicketTypeModel
                {
                    Id = t.TicketTypeId,
                    EventId = result.Value.Id,
                    Name = t.Name,
                    Price = t.Price,
                    Currency = t.Currency,
                    Quantity = t.Quantity
                }).ToList()),
            cancellationToken);
    }
}
