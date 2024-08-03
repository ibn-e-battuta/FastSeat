using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Attendance.Application.EventStatistics.GetEventStatistics;

public sealed record GetEventStatisticsQuery(Guid EventId) : IQuery<EventStatisticsResponse>;
