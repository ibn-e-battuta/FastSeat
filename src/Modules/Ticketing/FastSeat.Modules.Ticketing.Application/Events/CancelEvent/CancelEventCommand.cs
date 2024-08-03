using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Ticketing.Application.Events.CancelEvent;

public sealed record CancelEventCommand(Guid EventId) : ICommand;
