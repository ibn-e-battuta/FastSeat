using Microsoft.AspNetCore.Routing;

namespace FastSeat.Common.Presentation.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
