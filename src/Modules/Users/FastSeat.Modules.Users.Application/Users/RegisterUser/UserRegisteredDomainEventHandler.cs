using FastSeat.Common.Application.EventBus;
using FastSeat.Common.Application.Exceptions;
using FastSeat.Common.Application.Messaging;
using FastSeat.Common.Domain;
using FastSeat.Modules.Users.Application.Users.GetUser;
using FastSeat.Modules.Users.Domain.Users;
using FastSeat.Modules.Users.IntegrationEvents;
using MediatR;

namespace FastSeat.Modules.Users.Application.Users.RegisterUser;

internal sealed class UserRegisteredDomainEventHandler(ISender sender, IEventBus bus)
    : DomainEventHandler<UserRegisteredDomainEvent>
{
    public override async Task Handle(
        UserRegisteredDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        Result<UserResponse> result = await sender.Send(
            new GetUserQuery(domainEvent.UserId),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new FastSeatException(nameof(GetUserQuery), result.Error);
        }

        await bus.PublishAsync(
            new UserRegisteredIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                result.Value.Id,
                result.Value.Email,
                result.Value.FirstName,
                result.Value.LastName),
            cancellationToken);
    }
}
