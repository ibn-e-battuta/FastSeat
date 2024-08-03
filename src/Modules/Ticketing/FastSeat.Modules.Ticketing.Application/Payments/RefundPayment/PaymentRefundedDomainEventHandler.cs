using FastSeat.Common.Application.Messaging;
using FastSeat.Modules.Ticketing.Application.Abstractions.Payments;
using FastSeat.Modules.Ticketing.Domain.Payments;

namespace FastSeat.Modules.Ticketing.Application.Payments.RefundPayment;

internal sealed class PaymentRefundedDomainEventHandler(IPaymentService paymentService)
    : DomainEventHandler<PaymentRefundedDomainEvent>
{
    public override async Task Handle(
        PaymentRefundedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await paymentService.RefundAsync(domainEvent.TransactionId, domainEvent.RefundAmount);
    }
}
