﻿using FastSeat.Common.Domain;
using FastSeat.Modules.Ticketing.Application.Events.RescheduleEvent;
using FastSeat.Modules.Ticketing.Domain.Events;
using FastSeat.Modules.Ticketing.IntegrationTests.Abstractions;
using FluentAssertions;

namespace FastSeat.Modules.Ticketing.IntegrationTests.Events;

public class RescheduleEventTests : BaseIntegrationTest
{
    private const decimal Quantity = 10;

    public RescheduleEventTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenEventDoesNotExist()
    {
        //Arrange
        var eventId = Guid.NewGuid();
        DateTime startsAtUtc = DateTime.UtcNow;
        DateTime endsAtUtc = startsAtUtc.AddHours(1);

        var command = new RescheduleEventCommand(eventId, startsAtUtc, endsAtUtc);

        //Act
        Result result = await Sender.Send(command);

        //Assert
        result.Error.Should().Be(EventErrors.NotFound(command.EventId));
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenEventAlreadyStarted()
    {
        //Arrange
        var eventId = Guid.NewGuid();
        var ticketTypeId = Guid.NewGuid();
        DateTime startsAtUtc = DateTime.UtcNow.AddMinutes(-5);
        DateTime endsAtUtc = startsAtUtc.AddHours(1);

        await Sender.CreateEventWithTicketTypeAsync(eventId, ticketTypeId, Quantity);

        var command = new RescheduleEventCommand(eventId, startsAtUtc, endsAtUtc);

        //Act
        Result result = await Sender.Send(command);

        //Assert
        result.Error.Should().Be(EventErrors.StartDateInPast);
    }

    [Fact]
    public async Task Should_ReturnSuccess_WhenEventRescheduled()
    {
        //Arrange
        var eventId = Guid.NewGuid();
        var ticketTypeId = Guid.NewGuid();
        DateTime startsAtUtc = DateTime.UtcNow;
        DateTime endsAtUtc = startsAtUtc.AddHours(1);

        await Sender.CreateEventWithTicketTypeAsync(eventId, ticketTypeId, Quantity);

        var command = new RescheduleEventCommand(eventId, startsAtUtc, endsAtUtc);

        //Act
        Result result = await Sender.Send(command);

        //Assert
        result.Error.Should().Be(EventErrors.StartDateInPast);
    }
}
