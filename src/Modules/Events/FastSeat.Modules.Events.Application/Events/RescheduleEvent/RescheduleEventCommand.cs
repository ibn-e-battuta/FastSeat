using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Events.Application.Events.RescheduleEvent;

public sealed record RescheduleEventCommand(Guid EventId, DateTime StartsAtUtc, DateTime? EndsAtUtc) : ICommand;
