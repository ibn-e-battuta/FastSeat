﻿using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Attendance.Application.Events.CreateEvent;

public sealed record CreateEventCommand(
    Guid EventId,
    string Title,
    string Description,
    string Location,
    DateTime StartsAtUtc,
    DateTime? EndsAtUtc) : ICommand;
