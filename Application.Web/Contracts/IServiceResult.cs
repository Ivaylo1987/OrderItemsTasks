namespace Application.Web.Contracts
{
    using System.Net;
    public interface IServiceResult<T>
    {
        HttpStatusCode StatusCode { get; set; }
        T Value { get; set; }
    }
}