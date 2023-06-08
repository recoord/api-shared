namespace Api.Shared;

using Microsoft.AspNetCore.Mvc;

public interface IErrorHandlerProblemDetailsCustomizer
{
    ProblemDetails CustomizeProblemDetails(Exception exception, ProblemDetails defaultProblemDetails);
}