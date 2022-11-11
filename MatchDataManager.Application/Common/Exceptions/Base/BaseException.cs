using System.Net;
using MatchDataManager.Domain.Common.Constants;

namespace MatchDataManager.Application.Common.Exceptions.Base;

public abstract class BaseException : Exception, IBaseException
{
    protected BaseException() : base()
    {
    }

    protected BaseException(string? message) : base(message)
    {
        Message = ErrorMessages.SharedExceptions.SharedExceptionMessage;
    }

    protected BaseException(
        string? message,
        Exception? innerException) : base(message, innerException)
    {
        Message = ErrorMessages.SharedExceptions.SharedExceptionMessage;
    }

    public virtual HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public override string Message { get; } = ErrorMessages.SharedExceptions.SharedExceptionMessage;
}