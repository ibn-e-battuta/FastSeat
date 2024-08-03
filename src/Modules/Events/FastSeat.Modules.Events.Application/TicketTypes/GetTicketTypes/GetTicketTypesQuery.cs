using FastSeat.Common.Application.Messaging;
using FastSeat.Modules.Events.Application.TicketTypes.GetTicketType;

namespace FastSeat.Modules.Events.Application.TicketTypes.GetTicketTypes;

public sealed record GetTicketTypesQuery(Guid EventId) : IQuery<IReadOnlyCollection<TicketTypeResponse>>;
