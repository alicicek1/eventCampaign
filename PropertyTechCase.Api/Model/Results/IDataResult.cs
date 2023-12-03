namespace PropertyTechCase.Api.Model.Results
{
    public interface IDataResult<out T> : IResult
    {
        T? Data { get; }
    }
}