﻿using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Attendance.Application.Tickets.CreateTicket;

public sealed record CreateTicketCommand(Guid TicketId, Guid AttendeeId, Guid EventId, string Code) : ICommand;
