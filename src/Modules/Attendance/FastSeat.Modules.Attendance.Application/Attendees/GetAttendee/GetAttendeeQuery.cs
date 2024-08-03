using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Attendance.Application.Attendees.GetAttendee;

public sealed record GetAttendeeQuery(Guid CustomerId) : IQuery<AttendeeResponse>;
