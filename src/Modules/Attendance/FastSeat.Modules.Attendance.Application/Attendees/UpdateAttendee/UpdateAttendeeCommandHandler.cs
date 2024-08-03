﻿using FastSeat.Common.Application.Messaging;
using FastSeat.Common.Domain;
using FastSeat.Modules.Attendance.Application.Abstractions.Data;
using FastSeat.Modules.Attendance.Domain.Attendees;

namespace FastSeat.Modules.Attendance.Application.Attendees.UpdateAttendee;

internal sealed class UpdateAttendeeCommandHandler(IAttendeeRepository attendeeRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateAttendeeCommand>
{
    public async Task<Result> Handle(UpdateAttendeeCommand request, CancellationToken cancellationToken)
    {
        Attendee? attendee = await attendeeRepository.GetAsync(request.AttendeeId, cancellationToken);

        if (attendee is null)
        {
            return Result.Failure(AttendeeErrors.NotFound(request.AttendeeId));
        }

        attendee.Update(request.FirstName, request.LastName);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
