using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Ticketing.Application.Customers.UpdateCustomer;

public sealed record UpdateCustomerCommand(Guid CustomerId, string FirstName, string LastName) : ICommand;
