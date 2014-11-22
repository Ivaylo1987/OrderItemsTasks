namespace Application.Web.Services
{
    using Application.Data.Repositories;
    using Application.Data.UnitsOfWork;
    using Application.Models;
    using Application.Web.Contracts;
    using Application.Web.Models;
    using Application.Web.Services.Results;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;

    public class OrderItemsService : ApiService, IOrderItemsService
    {
        private const string NoSuchOrder = "Such order does not exist.";
        private const string NoSuchModel = "There is no such shoe model.";
        private const string NoSizeAvailable = "This size is not available.";
        private const string NotEnoughCounts = "There are not enough counts left from this size.";

        // Injected with a dependecy container e.g. Ninject
        public OrderItemsService(IApplicationData dataProvider)
            : base(dataProvider)
        {
        }

        public IServiceResult<string> AddItemToOrder(int orderId, OrderItemModel item)
        {
            var result = new ItemToOrderServiceResult();
            result.StatusCode = HttpStatusCode.BadRequest;

            var order = this.FindOrderById(orderId);

            if (order == null)
            {
                result.Value = NoSuchOrder;
                return result;
            }

            var shoeModel = this.FindShoeModelById(item.ShoeModelId);

            if (shoeModel == null)
            {
                result.Value = NoSuchModel;
                return result;
            }

            var availableSize = this.FindAvailableSize(shoeModel, item.Size);

            if (availableSize == null)
            {
                result.Value = NoSizeAvailable;
                return result;
            }


            if (availableSize.Count < item.Quantity)
            {
                result.Value = NotEnoughCounts;
                return result;
            }

            this.AttachToOrder(item, availableSize, shoeModel, order);

            result.Value = string.Format("You have {0} items in your cart", order.OrderItems.Count);

            return result;
        }

        private void AttachToOrder(OrderItemModel item, AvailableSize aAvailableSize, ShoeModel shoeModel, Order order)
        {
            aAvailableSize.Count -= item.Quantity;
            if (aAvailableSize.Count == 0)
            {
                shoeModel.AvailableSizes.Remove(aAvailableSize);
            }

            var orderItem = new OrderItem()
            {
                Order = order,
                Quantity = item.Quantity,
                Size = item.Size,
                ShoeModel = shoeModel
            };

            this.Data.OrderItems.Add(orderItem);

            this.Data.SaveChanges();
        }
    }
}