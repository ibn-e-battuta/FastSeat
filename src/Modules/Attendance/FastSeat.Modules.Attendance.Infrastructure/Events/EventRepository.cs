﻿using FastSeat.Modules.Attendance.Domain.Events;
using FastSeat.Modules.Attendance.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace FastSeat.Modules.Attendance.Infrastructure.Events;

internal sealed class EventRepository(AttendanceDbContext context) : IEventRepository
{
    public async Task<Event?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Events.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public void Insert(Event @event)
    {
        context.Events.Add(@event);
    }
}
