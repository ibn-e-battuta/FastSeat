using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Attendance.Application.Attendees.CheckInAttendee;

public sealed record CheckInAttendeeCommand(Guid AttendeeId, Guid TicketId) : ICommand;
