namespace Application.Web.Services.Results
{
    using Application.Web.Contracts;
    using System.Net;

    public class ItemToOrderServiceResult : IServiceResult<string>
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Value { get; set; }
    }
}