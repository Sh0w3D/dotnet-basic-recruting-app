using System.Net;
using MatchDataManager.Application.Common.Exceptions.Base;
using MatchDataManager.Domain.Common.Constants;

namespace MatchDataManager.Application.Common.Exceptions.Shared;

public class NotFoundException : IBaseException
{
    public NotFoundException()
    {
        StatusCode = HttpStatusCode.NotFound;
        Message = ErrorMessages.SharedExceptions.NotFoundMessage;
    }

    public HttpStatusCode StatusCode { get; }
    public string Message { get; }
}