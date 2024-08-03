using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Ticketing.Application.Payments.RefundPaymentsForEvent;

public sealed record RefundPaymentsForEventCommand(Guid EventId) : ICommand;
