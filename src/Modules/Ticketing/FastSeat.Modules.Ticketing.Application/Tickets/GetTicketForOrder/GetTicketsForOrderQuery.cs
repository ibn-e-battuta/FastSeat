using FastSeat.Common.Application.Messaging;
using FastSeat.Modules.Ticketing.Application.Tickets.GetTicket;

namespace FastSeat.Modules.Ticketing.Application.Tickets.GetTicketForOrder;

public sealed record GetTicketsForOrderQuery(Guid OrderId) : IQuery<IReadOnlyCollection<TicketResponse>>;
