using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Ticketing.Application.Orders.CreateOrder;

public sealed record CreateOrderCommand(Guid CustomerId) : ICommand;
