namespace Application.Web.Controllers
{
    using Application.Web.Contracts;
    using Application.Web.Models;
    using Application.Web.Services;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class OrderItemsController : BasicController
    {
        private IOrderItemsService orderItemsService;

        // Poor man's dependency injection constructor. Use it in case there is no dependency container e.g. Ninject
        public OrderItemsController()
            :this(new OrderItemsService())
        {
        }

        // Inject this dependency with a dependecy container e.g. Ninject
        public OrderItemsController(IOrderItemsService service)
        {
            this.orderItemsService = service;
        }

        // PUT: api/orderItems?orerId={orderId}
        public HttpResponseMessage Put(int orderId, [FromBody]OrderItemModel item)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var result = this.orderItemsService.AddItemToOrder(orderId, item);

            var response = this.Request.CreateResponse(result.StatusCode, result.Value);
            return response;
        }
    }
}
