using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MatchDataManager.Application.Common.Exceptions.Base;
using MatchDataManager.Domain.Common.Constants;

namespace MatchDataManager.Api.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        var (statusCode, message, errors) = exception switch
        {
            IBaseValidationException validationException => (
                (int)validationException.StatusCode,
                validationException.Message,
                validationException.Errors),
            IBaseException baseException => (
                (int)baseException.StatusCode,
                baseException.Message,
                null
            ),
            _ => (StatusCodes.Status500InternalServerError,
            SharedUnexpectedError: ErrorMessages.SharedExceptions.SharedUnexpectedErrorMessage,
            null),
        };

        return errors is null
            ? Problem(
                statusCode: statusCode,
                title: message)
            : ValidationProblem(
                modelStateDictionary: CreateModelStateDictionary(errors),
                title: message);
    }

    private static ModelStateDictionary CreateModelStateDictionary(IDictionary<string, string[]> errorsDictionary)
    {
        ModelStateDictionary modelStateDictionary = new();
        foreach (var errors in errorsDictionary.Values)
        {
            foreach (var error in errors)
            {
                modelStateDictionary.AddModelError(errorsDictionary.Keys.First(), error);
            }
        }

        return modelStateDictionary;
    }
}