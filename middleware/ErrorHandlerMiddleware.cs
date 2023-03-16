using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// Summary:
//  This catches an exception bobbling from a request call.
//  If an exception is caught it is turned into a ProblemDetail object that is serialized into a json reponse.
//  ProblemDetail is "A machine-readable format for specifying errors in HTTP API responses based on https://tools.ietf.org/html/rfc7807".
//  The ProblemDetail content can be cusomized, if needed, by registering an IErrorHandlerResponseObjectCustomizer implementation in the 'Services' IoC container.
[ExcludeFromCodeCoverage]
public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IErrorHandlerProblemDetailsCustomizer? _problemDetailsCustomizer;
    private readonly ILogger _logger;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger,
        IErrorHandlerProblemDetailsCustomizer? problemDetailsCustomizer = null)
    {
        _next = next;
        _problemDetailsCustomizer = problemDetailsCustomizer;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            var response = context.Response;

            // Setup defaults for ProblemDetails
            var contentType = DeriveContentType(exception);
            var statusCode = DeriveStatusCode(exception);
            var problemDetails = new ProblemDetails()
            {
                Title = exception.Message,
                Detail = exception.ToString(),
                Status = (int)statusCode,
            };

            // Call potential customizer
            var responseProblemDetails = problemDetails;
            if (_problemDetailsCustomizer != null)
            {
                var problemDetailsReturned = _problemDetailsCustomizer.CustomizeProblemDetails(exception, responseProblemDetails);
                // If return object is usable then do so, otherwise stick with byRef argument
                if (problemDetailsReturned != null)
                    responseProblemDetails = problemDetailsReturned;
            }

            // Set content type
            response.ContentType = contentType;

            // Set status code
            {
                response.StatusCode = (int)statusCode;
                // Use possible override
                if (responseProblemDetails.Status.HasValue)
                    response.StatusCode = responseProblemDetails.Status.Value;
            }

            // Serialize return object into response body
            var responseBody = JsonSerializer.Serialize(responseProblemDetails, new JsonSerializerOptions(JsonSerializerDefaults.Web));
            _logger.LogInformation($"responseBody={responseBody}");

            await response.WriteAsync(responseBody);
        }
    }

    private string DeriveContentType(Exception exception)
    {
        const string contentType = "application/problem+json; charset=utf-8";
        return contentType;
    }

    private HttpStatusCode DeriveStatusCode(Exception exception)
    {
        var statusCode = HttpStatusCode.InternalServerError;

        if (exception.GetType() == typeof(HttpRequestException))
        {
            HttpRequestException? ex = exception as HttpRequestException;
            if (ex != null && ex.StatusCode.HasValue)
                statusCode = ex.StatusCode.Value;
        }

        return statusCode;
    }
}