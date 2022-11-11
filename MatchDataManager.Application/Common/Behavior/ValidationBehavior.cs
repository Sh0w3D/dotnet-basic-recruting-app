using FluentValidation;
using FluentValidation.Results;
using MatchDataManager.Application.Common.Exceptions.Base;
using MediatR;

namespace MatchDataManager.Application.Common.Behavior;

public class ValidationBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        if (_validator is null)
            return await next();

        var validationResult = await _validator
            .ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
            return await next();

        IDictionary<string, string[]> errors = new Dictionary<string, string[]>();

        foreach (var result in validationResult.Errors)
        {
            if (result is null)
                continue;

            if (errors.Count != 0)
                AppendErrorsToDictionary(errors, result);
            else
                AddNewErrorToDictionary(errors, result);
        }

        throw new MatchDataManagerValidationException(errors);
    }

    /// <summary>
    /// Adds new key with array of strings containing errors
    /// </summary>
    /// <param name="errors"></param>
    /// <param name="result"></param>
    private static void AddNewErrorToDictionary(
        IDictionary<string, string[]> errors,
        ValidationFailure result)
    {
        errors.Add(result.PropertyName, new string[] { result.ErrorMessage });
    }

    /// <summary>
    /// Appends error to dictionary
    /// if key of name error.PropertyName exists than call <see cref="AddAdditionalErrorToProperty"/>
    /// if key of name error.PropertyName does not exist then call <see cref="AddNewErrorToDictionary"/>
    /// </summary>
    /// <param name="errors"></param>
    /// <param name="result"></param>
    private static void AppendErrorsToDictionary(
        IDictionary<string, string[]> errors,
        ValidationFailure result)
    {
        if (errors.ContainsKey(result.PropertyName))
            AddAdditionalErrorToProperty(errors, result);
        else
            AddNewErrorToDictionary(errors, result);
    }

    /// <summary>
    /// Adds new error to array where key equals error.PropertyName
    /// </summary>
    /// <param name="errors"></param>
    /// <param name="result"></param>
    private static void AddAdditionalErrorToProperty(
        IDictionary<string, string[]> errors,
        ValidationFailure result)
    {
        ICollection<string> errorList = errors[result.PropertyName].ToList();

        errorList.Add(result.ErrorMessage);

        errors[result.PropertyName] = errorList.ToArray();
    }
}