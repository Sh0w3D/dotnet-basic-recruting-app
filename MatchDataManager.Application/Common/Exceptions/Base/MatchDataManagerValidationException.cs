using System.Net;
using MatchDataManager.Domain.Common.Constants;

namespace MatchDataManager.Application.Common.Exceptions.Base;

public class MatchDataManagerValidationException : Exception, IBaseValidationException
{
    public MatchDataManagerValidationException(IDictionary<string, string[]> errors)
    {
        Errors = errors;
        Message = ErrorMessages.Validation.BaseMessage;
        StatusCode = HttpStatusCode.BadRequest;
    }

    public MatchDataManagerValidationException() : base()
    {
        Message = ErrorMessages.Validation.BaseMessage;
        Errors = null!;
    }

    public MatchDataManagerValidationException(
        string? message) : base(message)
    {
        Errors = null!;
        Message = ErrorMessages.Validation.BaseMessage;
    }

    public MatchDataManagerValidationException(
        string? message,
        Exception? innerException) : base(message, innerException)
    {
        Message = ErrorMessages.Validation.BaseMessage;
        Errors = null!;
    }

    public override string Message { get; }
    public HttpStatusCode StatusCode { get; }
    public IDictionary<string, string[]> Errors { get; }
}