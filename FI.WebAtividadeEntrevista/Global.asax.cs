using FI.AtividadeEntrevista.BLL;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            var boClient = new BoCliente();
            container.RegisterInstance(boClient);
            container.RegisterInstance(new HomeController());
            container.RegisterInstance(new ClienteController(boClient));
            container.RegisterInstance(new ValuesController());
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}
