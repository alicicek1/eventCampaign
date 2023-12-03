using System.Net;

namespace PropertyTechCase.Api.Model.Results;

public class NotFoundResult<T> : DataResult<T>
{
    public NotFoundResult(string errorCode) : base(default, true, "Not found.", errorCode,
        HttpStatusCode.OK)
    {
    }
}