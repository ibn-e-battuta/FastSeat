using FastSeat.Common.Domain;
using FastSeat.Common.Presentation.Endpoints;
using FastSeat.Common.Presentation.Results;
using FastSeat.Modules.Events.Application.Events.CancelEvent;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FastSeat.Modules.Events.Presentation.Events;

internal sealed class CancelEvent : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("events/{id}/cancel", async (Guid id, ISender sender) =>
        {
            Result result = await sender.Send(new CancelEventCommand(id));

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.ModifyEvents)
        .WithTags(Tags.Events);
    }
}
