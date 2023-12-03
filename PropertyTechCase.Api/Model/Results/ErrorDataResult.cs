using System.Net;

namespace PropertyTechCase.Api.Model.Results
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T? data, string message, string errorCode,
            HttpStatusCode statusCode) : base(data, false,
            message, errorCode, statusCode)
        {
        }
    }
}