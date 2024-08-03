using FastSeat.Modules.Ticketing.Domain.Orders;

namespace FastSeat.Modules.Ticketing.Application.Orders.GetOrders;

public sealed record OrderResponse(
    Guid Id,
    Guid CustomerId,
    OrderStatus Status,
    decimal TotalPrice,
    DateTime CreatedAtUtc);
