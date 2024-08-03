using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Ticketing.Application.Tickets.ArchiveTicketsForEvent;

public sealed record ArchiveTicketsForEventCommand(Guid EventId) : ICommand;
