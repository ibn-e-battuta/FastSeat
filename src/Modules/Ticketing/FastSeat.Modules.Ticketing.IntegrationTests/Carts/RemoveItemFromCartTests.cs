using FastSeat.Common.Domain;
using FastSeat.Modules.Ticketing.Application.Carts.RemoveItemFromCart;
using FastSeat.Modules.Ticketing.Domain.Customers;
using FastSeat.Modules.Ticketing.Domain.Events;
using FastSeat.Modules.Ticketing.IntegrationTests.Abstractions;
using FluentAssertions;

namespace FastSeat.Modules.Ticketing.IntegrationTests.Carts;

public class RemoveItemFromCartTests : BaseIntegrationTest
{
    private const decimal Quantity = 10;

    public RemoveItemFromCartTests(IntegrationTestWebAppFactory factory)
        :base(factory)
    { 
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenCustomerDoesNotExist()
    {
        //Arrange
        var command = new RemoveItemFromCartCommand(
            Guid.NewGuid(),
            Guid.NewGuid());

        //Act
        Result result = await Sender.Send(command);

        //Assert
        result.Error.Should().Be(CustomerErrors.NotFound(command.CustomerId));
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenTicketTypeDoesNotExist()
    {
        //Arrange
        Guid customerId = await Sender.CreateCustomerAsync(Guid.NewGuid());

        var command = new RemoveItemFromCartCommand(
            customerId,
            Guid.NewGuid());

        //Act
        Result result = await Sender.Send(command);

        //Assert
        result.Error.Should().Be(TicketTypeErrors.NotFound(command.TicketTypeId));
    }

    [Fact]
    public async Task Should_ReturnSuccess_WhenRemovedItemFromCart()
    {
        //Arrange
        Guid customerId = await Sender.CreateCustomerAsync(Guid.NewGuid());
        var eventId = Guid.NewGuid();
        var ticketTypeId = Guid.NewGuid();

        await Sender.CreateEventWithTicketTypeAsync(eventId, ticketTypeId, Quantity);

        var command = new RemoveItemFromCartCommand(
            customerId,
            ticketTypeId);

        //Act
        Result result = await Sender.Send(command);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }
}
