using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Ticketing.Application.Orders.GetOrders;

public sealed record GetOrdersQuery(Guid CustomerId) : IQuery<IReadOnlyCollection<OrderResponse>>;
