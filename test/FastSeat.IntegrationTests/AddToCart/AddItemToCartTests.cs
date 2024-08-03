﻿using FastSeat.Common.Domain;
using FastSeat.IntegrationTests.Abstractions;
using FastSeat.Modules.Ticketing.Application.Carts.AddItemToCart;
using FastSeat.Modules.Ticketing.Application.Customers.GetCustomer;
using FastSeat.Modules.Users.Application.Users.RegisterUser;
using FluentAssertions;

namespace FastSeat.IntegrationTests.AddToCart;

public sealed class AddItemToCartTests : BaseIntegrationTest
{
    private const decimal Quantity = 10;

    public AddItemToCartTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Customer_ShouldBeAbleTo_AddItemToCart()
    {
        // Register user
        var command = new RegisterUserCommand(
            Faker.Internet.Email(),
            Faker.Internet.Password(6),
            Faker.Name.FirstName(),
            Faker.Name.LastName());

        Result<Guid> userResult = await Sender.Send(command);

        userResult.IsSuccess.Should().BeTrue();

        // Get customer
        Result<CustomerResponse> customerResult = await Poller.WaitAsync(
            TimeSpan.FromSeconds(15),
            async () =>
            {
                var query = new GetCustomerQuery(userResult.Value);

                Result<CustomerResponse> customerResult = await Sender.Send(query);

                return customerResult;
            });

        customerResult.IsSuccess.Should().BeTrue();

        // Add item to cart
        CustomerResponse customer = customerResult.Value;
        var ticketTypeId = Guid.NewGuid();

        await Sender.CreateEventAsync(Guid.NewGuid(), ticketTypeId, Quantity);

        Result result = await Sender.Send(new AddItemToCartCommand(customer.Id, ticketTypeId, Quantity));

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}
