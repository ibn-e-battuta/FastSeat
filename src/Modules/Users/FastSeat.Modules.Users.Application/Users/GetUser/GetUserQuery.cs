﻿using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Users.Application.Users.GetUser;

public sealed record GetUserQuery(Guid UserId) : IQuery<UserResponse>;