using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Events.Application.Events.CancelEvent;

public sealed record CancelEventCommand(Guid EventId) : ICommand;
