using FastSeat.Common.Domain;
using FastSeat.Modules.Events.Application.TicketTypes.UpdateTicketTypePrice;
using FastSeat.Modules.Events.Domain.TicketTypes;
using FastSeat.Modules.Events.IntegrationTests.Abstractions;
using FluentAssertions;

namespace FastSeat.Modules.Events.IntegrationTests.TicketTypes;

public class UpdateTicketTypeTests : BaseIntegrationTest
{
    public UpdateTicketTypeTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenTicketTypeDoesNotExist()
    {
        // Arrange
        var command = new UpdateTicketTypePriceCommand(Guid.NewGuid(), Faker.Random.Decimal());

        // Act
        Result result = await Sender.Send(command);

        // Assert
        result.Error.Should().Be(TicketTypeErrors.NotFound(command.TicketTypeId));
    }

    [Fact]
    public async Task Should_ReturnSuccess_WhenTicketTypeExists()
    {
        // Arrange
        Guid categoryId = await Sender.CreateCategoryAsync(Faker.Music.Genre());
        Guid eventId = await Sender.CreateEventAsync(categoryId);
        Guid ticketTypeId = await Sender.CreateTicketTypeAsync(eventId);

        var command = new UpdateTicketTypePriceCommand(ticketTypeId, Faker.Random.Decimal());

        // Act
        Result result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}
