using FastSeat.Common.Application.Authorization;
using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Users.Application.Users.GetUserPermissions;

public sealed record GetUserPermissionsQuery(string IdentityId) : IQuery<PermissionsResponse>;
