using Application.Data;
using Application.Data.Repositories;
using Application.Data.UnitsOfWork;
using Application.Web.Contracts;
using Application.Web.Services;
using Microsoft.Owin;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using System.Reflection;
using System.Web.Http;

[assembly: OwinStartup(typeof(Application.Web.Startup))]

namespace Application.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(GlobalConfiguration.Configuration);
        }

        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            RegisterMappings(kernel);
            return kernel;
        }

        private static void RegisterMappings(StandardKernel kernel)
        {
            kernel.Bind<IApplicationData>().To<ApplicationData>()
                .WithConstructorArgument("context",
                    c => new ApplicationDbContext());

            kernel.Bind<IOrderItemsService>().To<OrderItemsService>();
        }
    }
}
