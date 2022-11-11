using System.Net;
using MatchDataManager.Application.Common.Exceptions.Base;
using MatchDataManager.Domain.Common.Constants;

namespace MatchDataManager.Application.Common.Exceptions.Shared;

public class NotFoundException : Exception, IBaseException
{
    public NotFoundException()
    {
        StatusCode = HttpStatusCode.NotFound;
        Message = ErrorMessages.SharedExceptions.NotFoundMessage;
    }

    public NotFoundException(string? message) : base(message)
    {
        StatusCode = HttpStatusCode.NotFound;
        Message = message ?? ErrorMessages.SharedExceptions.NotFoundMessage;
    }

    public NotFoundException(
        string? message,
        Exception? innerException) : base(message, innerException)
    {
        Message = ErrorMessages.SharedExceptions.NotFoundMessage;
    }

    public HttpStatusCode StatusCode { get; }
    public override string Message { get; }
}