using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Ticketing.Application.Tickets.CreateTicketBatch;

public sealed record CreateTicketBatchCommand(Guid OrderId) : ICommand;
