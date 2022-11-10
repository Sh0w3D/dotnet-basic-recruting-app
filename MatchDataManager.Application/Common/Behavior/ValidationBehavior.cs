using FluentValidation;
using FluentValidation.Results;
using MatchDataManager.Application.Common.Exceptions.Base;
using MediatR;

namespace MatchDataManager.Application.Common.Behavior;

public class ValidationBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : class
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
    /// Adding new Key and string array to the dictionary.
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
    /// Adding new Key and string array to the dictionary while the property name and error key is equals,
    /// if the property name is not equal AddNewErrorToDictionary function is executed.
    /// </summary>
    /// <param name="errors"></param>
    /// <param name="result"></param>
    private static void AppendErrorsToDictionary(
        IDictionary<string, string[]> errors,
        ValidationFailure result)
    {
        if (errors[result.PropertyName] is not null)
            AddAdditionalErrorToProperty(errors, result);
        else
            AddNewErrorToDictionary(errors, result);
    }

    /// <summary>
    /// Adding another error to the property that is having already validation error.
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