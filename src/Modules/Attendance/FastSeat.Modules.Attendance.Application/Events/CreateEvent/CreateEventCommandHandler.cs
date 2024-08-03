using FastSeat.Common.Application.Messaging;
using FastSeat.Common.Domain;
using FastSeat.Modules.Attendance.Application.Abstractions.Data;
using FastSeat.Modules.Attendance.Domain.Events;

namespace FastSeat.Modules.Attendance.Application.Events.CreateEvent;

internal sealed class CreateEventCommandHandler(
    IEventRepository eventRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateEventCommand>
{
    public async Task<Result> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var @event = Event.Create(
            request.EventId,
            request.Title,
            request.Description,
            request.Location,
            request.StartsAtUtc,
            request.EndsAtUtc);

        eventRepository.Insert(@event);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
