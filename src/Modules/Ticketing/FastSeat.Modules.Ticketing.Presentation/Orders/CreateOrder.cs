using FastSeat.Common.Domain;
using FastSeat.Common.Presentation.Endpoints;
using FastSeat.Common.Presentation.Results;
using FastSeat.Modules.Ticketing.Application.Abstractions.Authentication;
using FastSeat.Modules.Ticketing.Application.Orders.CreateOrder;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FastSeat.Modules.Ticketing.Presentation.Orders;

internal sealed class CreateOrder : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("orders", async (ICustomerContext customerContext, ISender sender) =>
        {
            Result result = await sender.Send(new CreateOrderCommand(customerContext.CustomerId));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.CreateOrder)
        .WithTags(Tags.Orders);
    }
}
