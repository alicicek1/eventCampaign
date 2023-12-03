using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;
using PropertyTechCase.Event.Api.Model.Log;

namespace PropertyTechCase.Event.Api.MvcFilters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class ResponseLoggingActionAttribute : Attribute, IResourceFilter, IAsyncResourceFilter
{
    private readonly ILogger<ResponseLoggingActionAttribute> _logger;

    public ResponseLoggingActionAttribute(ILogger<ResponseLoggingActionAttribute> logger)
    {
        _logger = logger;
    }

    public void OnResourceExecuting(ResourceExecutingContext context)
    {
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
    }

    public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {
        // This method is called asynchronously before the action method is invoked.
        // You can log request information here if needed.

        var resultContext = await next();

        // This method is called asynchronously after the action method has been invoked.

        // Log the response information
        LogResponse(resultContext);
    }

    private void LogResponse(ResourceExecutedContext context)
    {
        // Create your custom logging model here
        var loggingModel = new CustomLoggingModel
        {
            CorrelationId = GetCorrelationId(context),
            StatusCode = context.HttpContext.Response.StatusCode,
            HttpMethod = context.HttpContext.Request.Method,
            Path = context.HttpContext.Request.Path,
            ResponseBody = GetResponseBody(context)
            // Add other properties as needed
        };

        var logMessage = "Custom Response Log: " +
                         string.Join(", ", loggingModel.GetType().GetProperties()
                             .Select(property => $"{property.Name}={property.GetValue(loggingModel)}"));

        _logger.LogInformation(logMessage);
    }

    private string GetCorrelationId(ResourceExecutedContext context)
    {
        // Your logic to retrieve or generate a correlation ID
        // Example: You might retrieve it from the request headers
        return context.HttpContext.Request.Headers["CorrelationId"].ToString();
    }

    private string GetResponseBody(ResourceExecutedContext context)
    {
        // Your logic to retrieve the response body
        // Note: This might not work if the response has already been sent to the client
        try
        {
            var result = context.Result as Microsoft.AspNetCore.Mvc.ObjectResult;
            return result?.Value?.ToString();
        }
        catch (Exception)
        {
            // Handle exceptions when attempting to get the response body
            return null;
        }
    }
}