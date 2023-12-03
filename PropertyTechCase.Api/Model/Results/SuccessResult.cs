using System.Net;

namespace PropertyTechCase.Api.Model.Results
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message) : base(true, message, HttpStatusCode.OK)
        {
        }

        public SuccessResult() : base(true, "Successful operation.", HttpStatusCode.OK)
        {
        }
    }
}