using FastSeat.Modules.Ticketing.Domain.Events;
using FastSeat.Modules.Ticketing.Domain.Tickets;
using FastSeat.Modules.Ticketing.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace FastSeat.Modules.Ticketing.Infrastructure.Tickets;

internal sealed class TicketRepository(TicketingDbContext context) : ITicketRepository
{
    public async Task<IEnumerable<Ticket>> GetForEventAsync(
        Event @event,
        CancellationToken cancellationToken = default)
    {
        return await context.Tickets.Where(t => t.EventId == @event.Id).ToListAsync(cancellationToken);
    }

    public void InsertRange(IEnumerable<Ticket> tickets)
    {
        context.Tickets.AddRange(tickets);
    }
}
