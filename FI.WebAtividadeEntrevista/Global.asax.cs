using FI.AtividadeEntrevista.BLL;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebAtividadeEntrevista.Controllers;

namespace WebAtividadeEntrevista
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {            
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            container.Register<BoCliente>(Lifestyle.Scoped);
            container.Register<BoBeneficiario>(Lifestyle.Scoped);
            container.Register<HomeController>(Lifestyle.Scoped);
            container.Register<ClienteController>(Lifestyle.Scoped);
            container.Register<BeneficiarioController>(Lifestyle.Scoped);
            container.Register<ValuesController>(Lifestyle.Scoped);
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}
