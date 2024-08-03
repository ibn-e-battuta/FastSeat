using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Users.Application.Users.UpdateUser;

public sealed record UpdateUserCommand(Guid UserId, string FirstName, string LastName) : ICommand;
