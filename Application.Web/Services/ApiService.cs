namespace Application.Web.Services
{
    using Application.Data.Repositories;
    using Application.Data.UnitsOfWork;
    using Application.Models;
    using Application.Web.Contracts;
    using Microsoft.AspNet.Identity;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;

    public abstract class ApiService
    {
        private IApplicationData data;

        // Injected with a dependecy container e.g. Ninject
        public ApiService(IApplicationData dataProvider)
        {
            this.data = dataProvider;
        }

        protected IApplicationData Data { get { return this.data; } }


        protected Order FindOrderById(int id)
        {
            var order = this.Data.Orders.All()
                                        .FirstOrDefault(o => o.Id == id);

            return order;
        }

        protected ShoeModel FindShoeModelById(int id)
        {
            var shoeModel = this.Data.ShoeModels.All()
                                     .FirstOrDefault(shoe => shoe.Id == id);

            return shoeModel;
        }

        protected AvailableSize FindAvailableSize(ShoeModel shoeModel, ShoeSizes size)
        {
            var availableSize = shoeModel.AvailableSizes
                                         .FirstOrDefault(avs => avs.Size == size);

            return availableSize;
        }
    }
}