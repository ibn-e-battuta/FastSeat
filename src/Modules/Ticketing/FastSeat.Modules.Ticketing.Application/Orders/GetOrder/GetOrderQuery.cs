using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Ticketing.Application.Orders.GetOrder;

public sealed record GetOrderQuery(Guid OrderId) : IQuery<OrderResponse>;
