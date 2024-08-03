using FastSeat.Common.Application.EventBus;
using FastSeat.Common.Application.Exceptions;
using FastSeat.Common.Domain;
using FastSeat.Modules.Attendance.Application.Attendees.CreateAttendee;
using FastSeat.Modules.Users.IntegrationEvents;
using MediatR;

namespace FastSeat.Modules.Attendance.Presentation.Attendees;

internal sealed class UserRegisteredIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<UserRegisteredIntegrationEvent>
{
    public override async Task Handle(
        UserRegisteredIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new CreateAttendeeCommand(
                integrationEvent.UserId,
                integrationEvent.Email,
                integrationEvent.FirstName,
                integrationEvent.LastName),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new FastSeatException(nameof(CreateAttendeeCommand), result.Error);
        }
    }
}
