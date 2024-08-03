using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Ticketing.Application.Events.RescheduleEvent;

public sealed record RescheduleEventCommand(Guid EventId, DateTime StartsAtUtc, DateTime? EndsAtUtc) : ICommand;
