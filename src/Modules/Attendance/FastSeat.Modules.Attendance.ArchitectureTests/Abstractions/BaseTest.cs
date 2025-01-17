﻿using System.Reflection;
using FastSeat.Modules.Attendance.Domain.Attendees;
using FastSeat.Modules.Attendance.Infrastructure;

namespace FastSeat.Modules.Attendance.ArchitectureTests.Abstractions;

public abstract class BaseTest
{
    protected static readonly Assembly ApplicationAssembly = typeof(Attendance.Application.AssemblyReference).Assembly;

    protected static readonly Assembly DomainAssembly = typeof(Attendee).Assembly;

    protected static readonly Assembly InfrastructureAssembly = typeof(AttendanceModule).Assembly;

    protected static readonly Assembly PresentationAssembly = typeof(Attendance.Presentation.AssemblyReference).Assembly;
}
