using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Events.Application.Events.SearchEvents;

public sealed record SearchEventsQuery(
    Guid? CategoryId,
    DateTime? StartDate,
    DateTime? EndDate,
    int Page,
    int PageSize) : IQuery<SearchEventsResponse>;
