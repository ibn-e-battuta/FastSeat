using FastSeat.Modules.Ticketing.Domain.Events;

namespace FastSeat.Modules.Ticketing.Domain.Tickets;

public interface ITicketRepository
{
    Task<IEnumerable<Ticket>> GetForEventAsync(Event @event, CancellationToken cancellationToken = default);

    void InsertRange(IEnumerable<Ticket> tickets);
}
