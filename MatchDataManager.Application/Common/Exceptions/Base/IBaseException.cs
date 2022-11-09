using System.Net;

namespace MatchDataManager.Application.Common.Exceptions.Base;

public interface IBaseException
{
    public HttpStatusCode StatusCode { get; }
    public string Message { get; }
}