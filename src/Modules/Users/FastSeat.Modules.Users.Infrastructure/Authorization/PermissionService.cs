using FastSeat.Common.Application.Authorization;
using FastSeat.Common.Domain;
using FastSeat.Modules.Users.Application.Users.GetUserPermissions;
using MediatR;

namespace FastSeat.Modules.Users.Infrastructure.Authorization;

internal sealed class PermissionService(ISender sender) : IPermissionService
{
    public async Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId)
    {
        return await sender.Send(new GetUserPermissionsQuery(identityId));
    }
}
