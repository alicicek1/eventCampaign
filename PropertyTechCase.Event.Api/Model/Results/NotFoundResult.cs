using System.Net;

namespace PropertyTechCase.Event.Api.Model.Results;

public class NotFoundResult<T> : DataResult<T>
{
    public NotFoundResult(string errorCode) : base(default, true, "Not found.", errorCode,
        HttpStatusCode.OK)
    {
    }
}