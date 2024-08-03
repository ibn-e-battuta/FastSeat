using FastSeat.Common.Domain;
using FastSeat.Modules.Ticketing.Application.Customers.UpdateCustomer;
using FastSeat.Modules.Ticketing.Domain.Customers;
using FastSeat.Modules.Ticketing.IntegrationTests.Abstractions;
using FluentAssertions;

namespace FastSeat.Modules.Ticketing.IntegrationTests.Customers;

public class UpdateCustomerTests : BaseIntegrationTest
{
    public UpdateCustomerTests(IntegrationTestWebAppFactory factory) 
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenCustomerDoesNotExist()
    {
        //Arrange
        var command = new UpdateCustomerCommand(
            Guid.NewGuid(),
            Faker.Name.FirstName(),
            Faker.Name.LastName());

        //Act
        Result result = await Sender.Send(command);

        //Assert
        result.Error.Should().Be(CustomerErrors.NotFound(command.CustomerId));
    }

    [Fact]
    public async Task Should_ReturnSuccess_WhenCustomerIsUpdated()
    {
        //Arrange
        Guid customerId = await Sender.CreateCustomerAsync(Guid.NewGuid());

        var command = new UpdateCustomerCommand(
            customerId,
            Faker.Name.FirstName(),
            Faker.Name.LastName());

        //Act
        Result result = await Sender.Send(command);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }
}