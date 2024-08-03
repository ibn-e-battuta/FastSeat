using FastSeat.Common.Application.Messaging;
using FastSeat.Modules.Ticketing.Application.Tickets.GetTicket;

namespace FastSeat.Modules.Ticketing.Application.Tickets.GetTicketByCode;

public sealed record GetTicketByCodeQuery(string Code) : IQuery<TicketResponse>;
