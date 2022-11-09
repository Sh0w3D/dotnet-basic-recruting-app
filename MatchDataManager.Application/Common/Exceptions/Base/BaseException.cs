using System.Net;
using MatchDataManager.Domain.Common.Constants;

namespace MatchDataManager.Application.Common.Exceptions.Base;

public abstract class BaseException : Exception, IBaseException
{
    public virtual HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public override string Message { get; } = ErrorMessages.SharedExceptions.SharedExceptionMessage;
}