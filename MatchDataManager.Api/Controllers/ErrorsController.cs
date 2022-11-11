using MatchDataManager.Application.Common.Exceptions.Base;
using MatchDataManager.Domain.Common.Constants;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MatchDataManager.Api.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
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
                null),
            _ => (StatusCodes.Status500InternalServerError,
                ErrorMessages.SharedExceptions.SharedUnexpectedErrorMessage,
                null)
        };

        return errors is null
            ? Problem(
                statusCode: statusCode,
                title: message)
            : ValidationProblem(
                statusCode: statusCode,
                title: message,
                modelStateDictionary: CreateModelStateDictionary(errors));
    }

    private static ModelStateDictionary CreateModelStateDictionary(IDictionary<string, string[]> errors)
    {
        ModelStateDictionary modelStateDictionary = new();

        foreach(var (key, errorsArray) in errors)
        {
            modelStateDictionary.AddModelError(key, errorsArray.Single());
        }

        return modelStateDictionary;
    }
}