using FastSeat.Common.Infrastructure.Inbox;
using FastSeat.Common.Infrastructure.Outbox;
using FastSeat.Modules.Attendance.Application.Abstractions.Data;
using FastSeat.Modules.Attendance.Domain.Attendees;
using FastSeat.Modules.Attendance.Domain.Events;
using FastSeat.Modules.Attendance.Domain.Tickets;
using FastSeat.Modules.Attendance.Infrastructure.Attendees;
using FastSeat.Modules.Attendance.Infrastructure.Events;
using FastSeat.Modules.Attendance.Infrastructure.Tickets;
using Microsoft.EntityFrameworkCore;

namespace FastSeat.Modules.Attendance.Infrastructure.Database;

public sealed class AttendanceDbContext(DbContextOptions<AttendanceDbContext> options)
    : DbContext(options), IUnitOfWork
{
    internal DbSet<Attendee> Attendees { get; set; }

    internal DbSet<Event> Events { get; set; }

    internal DbSet<Ticket> Tickets { get; set; }

    internal DbSet<EventStatistics> EventStatistics { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Attendance);

        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new AttendeeConfiguration());
        modelBuilder.ApplyConfiguration(new EventConfiguration());
        modelBuilder.ApplyConfiguration(new TicketConfiguration());
        modelBuilder.ApplyConfiguration(new EventStatisticsConfiguration());
    }
}
