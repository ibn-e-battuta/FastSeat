﻿using System.Data.Common;
using Dapper;
using FastSeat.Common.Application.Data;
using FastSeat.Common.Application.Messaging;
using FastSeat.Common.Domain;
using FastSeat.Modules.Ticketing.Application.Tickets.GetTicket;

namespace FastSeat.Modules.Ticketing.Application.Tickets.GetTicketForOrder;

internal sealed class GetTicketsForOrderQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetTicketsForOrderQuery, IReadOnlyCollection<TicketResponse>>
{
    public async Task<Result<IReadOnlyCollection<TicketResponse>>> Handle(
        GetTicketsForOrderQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
            SELECT
                id AS {nameof(TicketResponse.Id)},
                customer_id AS {nameof(TicketResponse.CustomerId)},
                order_id AS {nameof(TicketResponse.OrderId)},
                event_id AS {nameof(TicketResponse.EventId)},
                ticket_type_id AS {nameof(TicketResponse.TicketTypeId)},
                code AS {nameof(TicketResponse.Code)},
                created_at_utc AS {nameof(TicketResponse.CreatedAtUtc)}
            FROM ticketing.tickets
            WHERE order_id = @OrderId
            """;

        List<TicketResponse> tickets = (await connection.QueryAsync<TicketResponse>(sql, request)).AsList();

        return tickets;
    }
}
