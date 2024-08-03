using FastSeat.Common.Domain;

namespace FastSeat.Modules.Attendance.Domain.Attendees;

public static class AttendeeErrors
{
    public static Error NotFound(Guid attendeeId) =>
        Error.NotFound("Attendees.NotFound", $"The attendee with the identifier {attendeeId} was not found");
}
