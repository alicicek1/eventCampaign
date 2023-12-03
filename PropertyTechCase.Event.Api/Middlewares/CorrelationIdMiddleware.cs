namespace PropertyTechCase.Event.Api.Middlewares;

public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Try to get the CorrelationId from the request header
        var correlationId = context.Request.Headers["CorrelationId"].ToString();

        // If not found, generate a new one
        if (string.IsNullOrEmpty(correlationId))
        {
            correlationId = Guid.NewGuid().ToString();
            // Add the CorrelationId to the response header
            context.Response.Headers.Add("CorrelationId", correlationId);
        }

        // Add the CorrelationId to the request items for later use
        context.Items["CorrelationId"] = correlationId;

        // Pass the request to the next middleware
        await _next(context);
    }
}