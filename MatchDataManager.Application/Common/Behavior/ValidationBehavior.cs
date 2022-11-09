using FluentValidation;
using FluentValidation.Results;
using MatchDataManager.Application.Common.Exceptions.Base;
using MediatR;

namespace MatchDataManager.Application.Common.Behavior;

public class ValidationBehavior<TRequest, TResponse> : 
    IPipelineBehavior<TRequest, TResponse>
    where TRequest: IRequest<TResponse>
    where TResponse: class
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validator is null)
            return await next();

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
            return await next();

        IDictionary<string, string[]> errors = new Dictionary<string, string[]>();

        foreach (var error in validationResult.Errors)
        {
            if (error is null)
                continue;

            if (errors.Count is not 0)
                AppendErrorToDictionary(errors, error);
            else
                AppendNewErrorToDictionary(errors, error);
        }

        throw new MatchDataManagerValidationException(errors);
    }

    /// <summary>
    /// Adds new key with array of strings containing errors
    /// </summary>
    /// <param name="errors"></param>
    /// <param name="error"></param>
    private static void AppendNewErrorToDictionary(
        IDictionary<string, string[]> errors,
        ValidationFailure error)
    {
        var key = error.PropertyName;
        var errorMessage = error.ErrorMessage;

        errors.Add(key, new[] { errorMessage });
    }

    /// <summary>
    /// Appends error to dictionary
    /// if key of name error.PropertyName exists than call <see cref="AddErrorToExistingKey"/>
    /// if key of name error.PropertyName does not exist then call <see cref="AppendNewErrorToDictionary"/>
    /// </summary>
    /// <param name="errors"></param>
    /// <param name="error"></param>
    private static void AppendErrorToDictionary(
        IDictionary<string, string[]> errors,
        ValidationFailure error)
    {
        if (errors.ContainsKey(error.PropertyName))
            AddErrorToExistingKey(errors, error);
        else
            AppendNewErrorToDictionary(errors, error);
    }

    /// <summary>
    /// Adds new error to array where key equals error.PropertyName
    /// </summary>
    /// <param name="errors"></param>
    /// <param name="error"></param>
    private static void AddErrorToExistingKey(
        IDictionary<string, string[]> errors,
        ValidationFailure error)
    {
        ICollection<string> errorList = errors[error.PropertyName].ToList();
        errorList.Add(error.ErrorMessage);

        errors[error.PropertyName] = errorList.ToArray();
    }
}