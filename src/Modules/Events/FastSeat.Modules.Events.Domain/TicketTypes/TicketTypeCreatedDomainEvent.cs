using FastSeat.Common.Domain;

namespace FastSeat.Modules.Events.Domain.TicketTypes;

public sealed class TicketTypeCreatedDomainEvent(Guid ticketTypeId) : DomainEvent
{
    public Guid TicketTypeId { get; init; } = ticketTypeId;
}
