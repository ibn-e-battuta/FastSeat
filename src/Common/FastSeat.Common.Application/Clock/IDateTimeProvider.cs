﻿namespace FastSeat.Common.Application.Clock;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}
