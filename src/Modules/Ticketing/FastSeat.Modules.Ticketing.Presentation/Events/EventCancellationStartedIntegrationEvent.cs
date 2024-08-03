using FastSeat.Common.Application.EventBus;
using FastSeat.Common.Application.Exceptions;
using FastSeat.Common.Domain;
using FastSeat.Modules.Events.IntegrationEvents;
using FastSeat.Modules.Ticketing.Application.Events.CancelEvent;
using MediatR;

namespace FastSeat.Modules.Ticketing.Presentation.Events;

internal sealed class EventCancellationStartedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<EventCancellationStartedIntegrationEvent>
{
    public override async Task Handle(
        EventCancellationStartedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new CancelEventCommand(integrationEvent.EventId), cancellationToken);

        if (result.IsFailure)
        {
            throw new FastSeatException(nameof(CancelEventCommand), result.Error);
        }
    }
}
