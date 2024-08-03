﻿using FastSeat.Common.Domain;
using FastSeat.Common.Presentation.Endpoints;
using FastSeat.Common.Presentation.Results;
using FastSeat.Modules.Events.Application.TicketTypes.GetTicketType;
using FastSeat.Modules.Events.Application.TicketTypes.GetTicketTypes;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FastSeat.Modules.Events.Presentation.TicketTypes;

internal sealed class GetTicketTypes : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("ticket-types", async (Guid eventId, ISender sender) =>
        {
            Result<IReadOnlyCollection<TicketTypeResponse>> result = await sender.Send(
                new GetTicketTypesQuery(eventId));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.GetTicketTypes)
        .WithTags(Tags.TicketTypes);
    }
}
