using FastSeat.Common.Application.EventBus;
using FastSeat.Common.Application.Exceptions;
using FastSeat.Common.Domain;
using FastSeat.Modules.Attendance.Application.Tickets.CreateTicket;
using FastSeat.Modules.Ticketing.IntegrationEvents;
using MediatR;

namespace FastSeat.Modules.Attendance.Presentation.Tickets;

internal sealed class TicketIssuedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<TicketIssuedIntegrationEvent>
{
    public override async Task Handle(
        TicketIssuedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new CreateTicketCommand(
                integrationEvent.TicketId,
                integrationEvent.CustomerId,
                integrationEvent.EventId,
                integrationEvent.Code),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new FastSeatException(nameof(CreateTicketCommand), result.Error);
        }
    }
}
