using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Ticketing.Application.Payments.RefundPayment;

public sealed record RefundPaymentCommand(Guid PaymentId, decimal Amount) : ICommand;
