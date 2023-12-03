using System.Net;

namespace PropertyTechCase.Event.Api.Model.Results
{
    public interface IResult
    {
        bool Success { get; }
        string? Message { get; }
        string? ErrorCode { get; }
        HttpStatusCode? StatusCode { get; }
    }
}