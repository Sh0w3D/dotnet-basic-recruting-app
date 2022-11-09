namespace MatchDataManager.Application.Common.Exceptions.Base;

public interface IBaseValidationException : IBaseException
{
    public IDictionary<string, string[]> Errors { get; }
}