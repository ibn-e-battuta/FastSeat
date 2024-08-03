using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Events.Application.Events.PublishEvent;

public sealed record PublishEventCommand(Guid EventId) : ICommand;
