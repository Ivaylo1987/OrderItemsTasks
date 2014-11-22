namespace Application.Web.Contracts
{
    using Application.Web.Models;

    public interface IOrderItemsService
    {
        IServiceResult<string> AddItemToOrder(int orderId, OrderItemModel item);
    }
}
