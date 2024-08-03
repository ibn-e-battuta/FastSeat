using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Ticketing.Application.Customers.CreateCustomer;

public sealed record CreateCustomerCommand(Guid CustomerId, string Email, string FirstName, string LastName)
    : ICommand;
