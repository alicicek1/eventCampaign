using System.Net;

namespace PropertyTechCase.Api.Model.Results
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data, string message) : base(data, true, message,
            HttpStatusCode.OK)
        {
        }

        public SuccessDataResult(T data) : base(data, true, "Successful operation.", HttpStatusCode.OK)
        {
        }
    }
}