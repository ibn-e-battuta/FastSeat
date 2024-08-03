﻿using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Users.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(string Email, string Password, string FirstName, string LastName)
    : ICommand<Guid>;
