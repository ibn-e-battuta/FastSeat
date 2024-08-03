using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Events.Application.Events.GetEvents;

public sealed record GetEventsQuery : IQuery<IReadOnlyCollection<EventResponse>>;
