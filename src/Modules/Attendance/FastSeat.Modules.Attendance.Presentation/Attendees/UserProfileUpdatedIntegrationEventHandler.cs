using FastSeat.Common.Application.EventBus;
using FastSeat.Common.Application.Exceptions;
using FastSeat.Common.Domain;
using FastSeat.Modules.Attendance.Application.Attendees.UpdateAttendee;
using FastSeat.Modules.Users.IntegrationEvents;
using MediatR;

namespace FastSeat.Modules.Attendance.Presentation.Attendees;

internal sealed class UserProfileUpdatedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<UserProfileUpdatedIntegrationEvent>
{
    public override async Task Handle(
        UserProfileUpdatedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new UpdateAttendeeCommand(
                integrationEvent.UserId,
                integrationEvent.FirstName,
                integrationEvent.LastName),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new FastSeatException(nameof(UpdateAttendeeCommand), result.Error);
        }
    }
}
