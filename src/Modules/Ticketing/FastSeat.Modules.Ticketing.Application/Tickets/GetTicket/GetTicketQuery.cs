using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Ticketing.Application.Tickets.GetTicket;

public sealed record GetTicketQuery(Guid TicketId) : IQuery<TicketResponse>;
