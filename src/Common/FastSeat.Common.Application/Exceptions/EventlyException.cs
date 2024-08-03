using FastSeat.Common.Domain;

namespace FastSeat.Common.Application.Exceptions;

public sealed class FastSeatException : Exception
{
    public FastSeatException(string requestName, Error? error = default, Exception? innerException = default)
        : base("Application exception", innerException)
    {
        RequestName = requestName;
        Error = error;
    }

    public string RequestName { get; }

    public Error? Error { get; }
}
