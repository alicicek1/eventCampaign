using System.Net;

namespace PropertyTechCase.Api.Model.Results
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