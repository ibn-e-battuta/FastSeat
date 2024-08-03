using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Ticketing.Application.Customers.GetCustomer;

public sealed record GetCustomerQuery(Guid CustomerId) : IQuery<CustomerResponse>;
