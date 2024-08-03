using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Ticketing.Application.Carts.RemoveItemFromCart;

public sealed record RemoveItemFromCartCommand(Guid CustomerId, Guid TicketTypeId) : ICommand;
