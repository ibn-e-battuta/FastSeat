﻿using FastSeat.Common.Domain;
using FastSeat.Modules.Events.Domain.Categories;
using FastSeat.Modules.Events.Domain.Events;
using FastSeat.Modules.Events.Domain.TicketTypes;
using FastSeat.Modules.Events.UnitTests.Abstractions;
using FluentAssertions;

namespace FastSeat.Modules.Events.UnitTests.TicketTypes;

public class TicketTypeTests : BaseTest
{
    [Fact]
    public void Create_ShouldReturnValue_WhenTicketTypeIsCreated()
    {
        //Arrange
        var category = Category.Create(Faker.Music.Genre());
        DateTime startsAtUtc = DateTime.UtcNow;

        Result<Event> eventResult = Event.Create(
            category,
            Faker.Music.Genre(),
            Faker.Music.Genre(),
            Faker.Address.StreetAddress(),
            startsAtUtc,
            null);

        //Act
        Result<TicketType> result = TicketType.Create(
            eventResult.Value,
            Faker.Music.Genre(),
            Faker.Random.Decimal(),
            Faker.Random.String(),
            Faker.Random.Decimal());

        //Assert
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public void UpdatePrice_ShouldRaiseDomainEvent_WhenTicketTypeIsUpdated()
    {
        //Arrange
        var category = Category.Create(Faker.Music.Genre());
        DateTime startsAtUtc = DateTime.UtcNow;

        Result<Event> eventResult = Event.Create(
            category,
            Faker.Music.Genre(),
            Faker.Music.Genre(),
            Faker.Address.StreetAddress(),
            startsAtUtc,
            null);

        Result<TicketType> result = TicketType.Create(
            eventResult.Value,
            Faker.Music.Genre(),
            Faker.Random.Decimal(),
            Faker.Random.String(),
            Faker.Random.Decimal());

        TicketType ticketType = result.Value;

        //Act
        ticketType.UpdatePrice(Faker.Random.Decimal());

        //Assert
        TicketTypePriceChangedDomainEvent domainEvent =
            AssertDomainEventWasPublished<TicketTypePriceChangedDomainEvent>(ticketType);

        domainEvent.TicketTypeId.Should().Be(ticketType.Id);
    }
}
