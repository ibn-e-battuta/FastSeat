using System.Data.Common;
using FastSeat.Common.Application.Messaging;
using FastSeat.Common.Domain;
using FastSeat.Modules.Ticketing.Application.Abstractions.Data;
using FastSeat.Modules.Ticketing.Domain.Events;
using FastSeat.Modules.Ticketing.Domain.Payments;

namespace FastSeat.Modules.Ticketing.Application.Payments.RefundPaymentsForEvent;

internal sealed class RefundPaymentsForEventCommandHandler(
    IEventRepository eventRepository,
    IPaymentRepository paymentRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RefundPaymentsForEventCommand>
{
    public async Task<Result> Handle(RefundPaymentsForEventCommand request, CancellationToken cancellationToken)
    {
        await using DbTransaction transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

        Event? @event = await eventRepository.GetAsync(request.EventId, cancellationToken);

        if (@event is null)
        {
            return Result.Failure(EventErrors.NotFound(request.EventId));
        }

        IEnumerable<Payment> payments = await paymentRepository.GetForEventAsync(@event, cancellationToken);

        foreach (Payment payment in payments)
        {
            payment.Refund(payment.Amount - (payment.AmountRefunded ?? decimal.Zero));
        }

        @event.PaymentsRefunded();

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        return Result.Success();
    }
}
