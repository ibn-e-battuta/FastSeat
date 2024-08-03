﻿using FastSeat.Common.Application.Messaging;

namespace FastSeat.Modules.Ticketing.Application.Carts.GetCart;

public sealed record GetCartQuery(Guid CustomerId) : IQuery<Cart>;
