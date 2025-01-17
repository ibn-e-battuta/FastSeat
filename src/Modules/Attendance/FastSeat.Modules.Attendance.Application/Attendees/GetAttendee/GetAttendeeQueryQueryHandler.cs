﻿using System.Data.Common;
using Dapper;
using FastSeat.Common.Application.Data;
using FastSeat.Common.Application.Messaging;
using FastSeat.Common.Domain;
using FastSeat.Modules.Attendance.Domain.Attendees;

namespace FastSeat.Modules.Attendance.Application.Attendees.GetAttendee;

internal sealed class GetAttendeeQueryQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetAttendeeQuery, AttendeeResponse>
{
    public async Task<Result<AttendeeResponse>> Handle(GetAttendeeQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(AttendeeResponse.Id)},
                 email AS {nameof(AttendeeResponse.Email)},
                 first_name AS {nameof(AttendeeResponse.FirstName)},
                 last_name AS {nameof(AttendeeResponse.LastName)}
             FROM attendance.attendees
             WHERE id = @CustomerId
             """;

        AttendeeResponse? customer = await connection.QuerySingleOrDefaultAsync<AttendeeResponse>(sql, request);

        if (customer is null)
        {
            return Result.Failure<AttendeeResponse>(AttendeeErrors.NotFound(request.CustomerId));
        }

        return customer;
    }
}
