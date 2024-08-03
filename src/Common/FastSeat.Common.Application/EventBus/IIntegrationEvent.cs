﻿namespace FastSeat.Common.Application.EventBus;

public interface IIntegrationEvent
{
    Guid Id { get; }

    DateTime OccurredOnUtc { get; }
}
