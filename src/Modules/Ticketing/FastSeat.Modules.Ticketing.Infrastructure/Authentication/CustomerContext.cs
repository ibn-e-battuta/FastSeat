using FastSeat.Common.Application.Exceptions;
using FastSeat.Common.Infrastructure.Authentication;
using FastSeat.Modules.Ticketing.Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;

namespace FastSeat.Modules.Ticketing.Infrastructure.Authentication;

internal sealed class CustomerContext(IHttpContextAccessor httpContextAccessor) : ICustomerContext
{
    public Guid CustomerId => httpContextAccessor.HttpContext?.User.GetUserId() ??
                              throw new FastSeatException("User identifier is unavailable");
}
