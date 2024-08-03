using FastSeat.Common.Application.Exceptions;
using FastSeat.Common.Infrastructure.Authentication;
using FastSeat.Modules.Attendance.Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;

namespace FastSeat.Modules.Attendance.Infrastructure.Authentication;

internal sealed class AttendanceContext(IHttpContextAccessor httpContextAccessor) : IAttendanceContext
{
    public Guid AttendeeId => httpContextAccessor.HttpContext?.User.GetUserId() ??
                              throw new FastSeatException("User identifier is unavailable");
}
