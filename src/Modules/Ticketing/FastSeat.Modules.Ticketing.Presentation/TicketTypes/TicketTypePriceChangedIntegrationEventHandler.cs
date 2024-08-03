using FastSeat.Common.Application.EventBus;
using FastSeat.Common.Application.Exceptions;
using FastSeat.Common.Domain;
using FastSeat.Modules.Events.IntegrationEvents;
using FastSeat.Modules.Ticketing.Application.TicketTypes.UpdateTicketTypePrice;
using MediatR;

namespace FastSeat.Modules.Ticketing.Presentation.TicketTypes;

internal sealed class TicketTypePriceChangedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<TicketTypePriceChangedIntegrationEvent>
{
    public override async Task Handle(
        TicketTypePriceChangedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new UpdateTicketTypePriceCommand(integrationEvent.TicketTypeId, integrationEvent.Price),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new FastSeatException(nameof(UpdateTicketTypePriceCommand), result.Error);
        }
    }
}
