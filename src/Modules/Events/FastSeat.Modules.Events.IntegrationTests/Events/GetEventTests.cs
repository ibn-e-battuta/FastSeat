﻿using FastSeat.Common.Domain;
using FastSeat.Modules.Events.Application.Events.GetEvent;
using FastSeat.Modules.Events.Domain.Events;
using FastSeat.Modules.Events.IntegrationTests.Abstractions;
using FluentAssertions;

namespace FastSeat.Modules.Events.IntegrationTests.Events;

public class GetEventTests : BaseIntegrationTest
{
    public GetEventTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenEventDoesNotExist()
    {
        // Arrange
        var query = new GetEventQuery(Guid.NewGuid());

        // Act
        Result<EventResponse> result = await Sender.Send(query);

        // Assert
        result.Error.Should().Be(EventErrors.NotFound(query.EventId));
    }

    [Fact]
    public async Task Should_ReturnEvent_WhenEventExists()
    {
        // Arrange
        await CleanDatabaseAsync();

        Guid categoryId = await Sender.CreateCategoryAsync(Faker.Music.Genre());

        Guid eventId = await Sender.CreateEventAsync(categoryId);

        var query = new GetEventQuery(eventId);

        // Act
        Result<EventResponse> result = await Sender.Send(query);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }
}
