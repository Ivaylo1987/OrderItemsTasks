namespace Application.Web.Services
{
    using Application.Data.Repositories;
    using Application.Data.UnitsOfWork;
    using Application.Web.Contracts;

    using Microsoft.AspNet.Identity;

    using System.Net.Http;
    using System.Threading;

    public abstract class ApiService
    {
        private IApplicationData data;

        // Poor man's dependency injection constructor. Use it in case there is no dependency container e.g. Ninject
        public ApiService()
            : this(new ApplicationData())
        {
        }

        public ApiService(IApplicationData dataProvider)
        {
            this.data = dataProvider;
        }

        protected IApplicationData Data { get { return this.data; } }
    }
}