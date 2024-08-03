using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Ticketing.Application.Carts.ClearCart;

public sealed record ClearCartCommand(Guid CustomerId) : ICommand;
