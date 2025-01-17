﻿using FastSeat.Common.Application.EventBus;

namespace FastSeat.Modules.Events.IntegrationEvents;

public sealed class EventCancellationCompletedIntegrationEvent : IntegrationEvent
{
    public EventCancellationCompletedIntegrationEvent(Guid id, DateTime occurredOnUtc, Guid eventId)
        : base(id, occurredOnUtc)
    {
        EventId = eventId;
    }

    public Guid EventId { get; init; }
}
