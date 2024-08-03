using FastSeat.Common.Domain;
using MediatR;

namespace FastSeat.Common.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
