using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Events.Application.TicketTypes.CreateTicketType;

public sealed record CreateTicketTypeCommand(
    Guid EventId,
    string Name,
    decimal Price,
    string Currency,
    decimal Quantity) : ICommand<Guid>;
