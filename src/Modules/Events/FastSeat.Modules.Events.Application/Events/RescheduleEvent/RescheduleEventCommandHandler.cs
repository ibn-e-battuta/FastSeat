using FastSeat.Common.Application.Clock;
using FastSeat.Common.Application.Messaging;
using FastSeat.Common.Domain;
using FastSeat.Modules.Events.Application.Abstractions.Data;
using FastSeat.Modules.Events.Domain.Events;

namespace FastSeat.Modules.Events.Application.Events.RescheduleEvent;

internal sealed class RescheduleEventCommandHandler(
    IDateTimeProvider dateTimeProvider,
    IEventRepository eventRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RescheduleEventCommand>
{
    public async Task<Result> Handle(RescheduleEventCommand request, CancellationToken cancellationToken)
    {
        Event? @event = await eventRepository.GetAsync(request.EventId, cancellationToken);

        if (@event is null)
        {
            return Result.Failure(EventErrors.NotFound(request.EventId));
        }

        if (request.StartsAtUtc < dateTimeProvider.UtcNow)
        {
            return Result.Failure(EventErrors.StartDateInPast);
        }

        @event.Reschedule(request.StartsAtUtc, request.EndsAtUtc);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
