using System.Net;

namespace PropertyTechCase.Event.Api.Model.Results
{
    public class ErrorResult : Result
    {
        public ErrorResult(string message, string errorCode, HttpStatusCode statusCode)
            : base(false, message,
                errorCode, statusCode)
        {
        }
    }
}