using System.Diagnostics.CodeAnalysis;
using System.Net;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using PropertyTechCase.Api.Model.Results;
using PropertyTechCase.Api.MvcFilters;

namespace PropertyTechCase.Api.Controllers;

[Route("api/[action]/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[ServiceFilter(typeof(ResponseLoggingActionAttribute))]
public class BaseController : ControllerBase
{
    protected virtual IActionResult ApiResponse<T>(DataResult<T> data)
    {
        CommonApiResponse<T> response = new CommonApiResponse<T>();
        response.ErrorCode = data.ErrorCode;
        response.IsSuccess = data.Success;
        response.Data = data.Data;
        response.Message = data.Message;

        switch (data.StatusCode)
        {
            // Informational
            case HttpStatusCode.Continue:
            case HttpStatusCode.SwitchingProtocols:
            case HttpStatusCode.Processing:
            case HttpStatusCode.EarlyHints:

            //Success
            case HttpStatusCode.OK:
                return Ok(response);
            case HttpStatusCode.Created:
                return Created("", response);
            case HttpStatusCode.Accepted:
            case HttpStatusCode.NonAuthoritativeInformation:
            case HttpStatusCode.NoContent:
                return NoContent();
            case HttpStatusCode.ResetContent:
            case HttpStatusCode.PartialContent:
            case HttpStatusCode.MultiStatus:
            case HttpStatusCode.AlreadyReported:
            case HttpStatusCode.IMUsed:

            // Redirection  
            case HttpStatusCode.MultipleChoices:
            case HttpStatusCode.MovedPermanently:
            case HttpStatusCode.Found:
            case HttpStatusCode.SeeOther:
            case HttpStatusCode.NotModified:
            case HttpStatusCode.TemporaryRedirect:
            case HttpStatusCode.PermanentRedirect:

            // Client Error  
            case HttpStatusCode.BadRequest:
                return BadRequest(response);
            case HttpStatusCode.Unauthorized:
                return Unauthorized();
            case HttpStatusCode.PaymentRequired:
            case HttpStatusCode.Forbidden:
            case HttpStatusCode.NotFound:
                return NotFound(response);
            case HttpStatusCode.MethodNotAllowed:
            case HttpStatusCode.NotAcceptable:

            case HttpStatusCode.ProxyAuthenticationRequired:
            case HttpStatusCode.RequestTimeout:
            case HttpStatusCode.Conflict:
                return Conflict();
            case HttpStatusCode.PreconditionFailed:
            case HttpStatusCode.RequestEntityTooLarge:
            case HttpStatusCode.RequestUriTooLong:
            case HttpStatusCode.UnsupportedMediaType:
            case HttpStatusCode.RequestedRangeNotSatisfiable:
            case HttpStatusCode.ExpectationFailed:
            case HttpStatusCode.MisdirectedRequest:
            case HttpStatusCode.UnprocessableEntity:
            case HttpStatusCode.Locked:
            case HttpStatusCode.FailedDependency:
            case HttpStatusCode.UpgradeRequired:
            case HttpStatusCode.PreconditionRequired:
            case HttpStatusCode.TooManyRequests:
            case HttpStatusCode.RequestHeaderFieldsTooLarge:
            case HttpStatusCode.UnavailableForLegalReasons:


            // Server Error
            case HttpStatusCode.InternalServerError:
            case HttpStatusCode.NotImplemented:
            case HttpStatusCode.BadGateway:
            case HttpStatusCode.ServiceUnavailable:
            case HttpStatusCode.GatewayTimeout:
            case HttpStatusCode.HttpVersionNotSupported:
            case HttpStatusCode.VariantAlsoNegotiates:
            case HttpStatusCode.InsufficientStorage:
            case HttpStatusCode.LoopDetected:
            case HttpStatusCode.NetworkAuthenticationRequired:


            default:
                return new JsonResult("");
        }
    }
}

public class CommonApiResponse<T>
{
    public T? Data { get; set; }
    public string? ErrorCode { get; set; }
    public string? Message { get; set; }
    public bool IsSuccess { get; set; } = false;
    public Guid? LogId { get; set; }
}