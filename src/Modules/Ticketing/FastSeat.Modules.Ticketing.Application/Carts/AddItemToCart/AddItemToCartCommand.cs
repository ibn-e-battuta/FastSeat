using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Ticketing.Application.Carts.AddItemToCart;

public sealed record AddItemToCartCommand(Guid CustomerId, Guid TicketTypeId, decimal Quantity)
    : ICommand;
