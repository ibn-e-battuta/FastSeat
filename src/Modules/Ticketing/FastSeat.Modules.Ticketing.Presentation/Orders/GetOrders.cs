using FastSeat.Common.Domain;
using FastSeat.Common.Presentation.Endpoints;
using FastSeat.Common.Presentation.Results;
using FastSeat.Modules.Ticketing.Application.Abstractions.Authentication;
using FastSeat.Modules.Ticketing.Application.Orders.GetOrders;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FastSeat.Modules.Ticketing.Presentation.Orders;

internal sealed class GetOrders : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("orders", async (ICustomerContext customerContext, ISender sender) =>
        {
            Result<IReadOnlyCollection<OrderResponse>> result = await sender.Send(
                new GetOrdersQuery(customerContext.CustomerId));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.GetOrders)
        .WithTags(Tags.Orders);
    }
}
