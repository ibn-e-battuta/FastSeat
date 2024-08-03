using FastSeat.Common.Infrastructure.Inbox;
using FastSeat.Common.Infrastructure.Outbox;
using FastSeat.Modules.Events.Application.Abstractions.Data;
using FastSeat.Modules.Events.Domain.Categories;
using FastSeat.Modules.Events.Domain.Events;
using FastSeat.Modules.Events.Domain.TicketTypes;
using FastSeat.Modules.Events.Infrastructure.Events;
using FastSeat.Modules.Events.Infrastructure.TicketTypes;
using Microsoft.EntityFrameworkCore;

namespace FastSeat.Modules.Events.Infrastructure.Database;

public sealed class EventsDbContext(DbContextOptions<EventsDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<Event> Events { get; set; }

    internal DbSet<Category> Categories { get; set; }

    internal DbSet<TicketType> TicketTypes { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Events);

        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new EventConfiguration());
        modelBuilder.ApplyConfiguration(new TicketTypeConfiguration());
    }
}
