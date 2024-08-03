using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Events.Application.Events.GetEvent;

public sealed record GetEventQuery(Guid EventId) : IQuery<EventResponse>;
