using System.Net;
using MatchDataManager.Application.Common.Exceptions.Base;
using MatchDataManager.Domain.Common.Constants;

namespace MatchDataManager.Application.Common.Exceptions.Shared;

public class SaveToDatabaseException : Exception, IBaseException
{
    public SaveToDatabaseException()
    {
        Message = ErrorMessages.SharedExceptions.DatabaseSaveMessage;
        StatusCode = HttpStatusCode.BadRequest;
    }

    public SaveToDatabaseException(
        string? message,
        Exception? innerException) : base(message, innerException)
    {
        Message = message ?? ErrorMessages.SharedExceptions.DatabaseSaveMessage;
        StatusCode = HttpStatusCode.BadRequest;
    }

    public override string Message { get; }
    public HttpStatusCode StatusCode { get; }
}