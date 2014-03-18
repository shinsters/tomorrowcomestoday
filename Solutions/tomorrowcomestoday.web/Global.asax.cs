namespace TomorrowComesToday.Web
{
    using System;
    using System.Reflection;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Castle.Windsor;

    using CommonServiceLocator.WindsorAdapter;

    using log4net.Config;

    using Microsoft.AspNet.SignalR;
    using Microsoft.Practices.ServiceLocation;

    using SharpArch.Domain.Events;
    using SharpArch.NHibernate;
    using SharpArch.NHibernate.Web.Mvc;
    using SharpArch.Web.Mvc.Castle;
    using SharpArch.Web.Mvc.ModelBinder;

    using TomorrowComesToday.Infrastructure.NHibernateMaps;
    using TomorrowComesToday.Web.CastleWindsor;
    using TomorrowComesToday.Web.Controllers;

    /// <summary>
    /// Represents the MVC Application
    /// </summary>
    /// <remarks>
    /// For instructions on enabling IIS6 or IIS7 classic mode, 
    /// visit http://go.microsoft.com/?LinkId=9394801
    /// </remarks>
    public class MvcApplication : System.Web.HttpApplication
    {
        private WebSessionStorage webSessionStorage;

        /// <summary>
        /// Assign dependencies. This should be automated really
        /// </summary>
        private void Dependencies()
        {
        }

        private void InitialiseNHibernateSessions()
        {
            NHibernateSession.ConfigurationCache = new NHibernateConfigurationFileCache();

            NHibernateSession.Init(
                this.webSessionStorage,
                new[] { this.Server.MapPath("~/bin/TomorrowComesToday.Infrastructure.dll") },
                new AutoPersistenceModelGenerator().Generate(),
                this.Server.MapPath("~/NHibernate.config"));
        }

        /// <summary>
        /// Due to issues on IIS7, the NHibernate initialization must occur in Init().
        /// But Init() may be invoked more than once; accordingly, we introduce a thread-safe
        /// mechanism to ensure it's only initialized once.
        /// See http://msdn.microsoft.com/en-us/magazine/cc188793.aspx for explanation details.
        /// </summary>
        public override void Init()
        {
            base.Init();
            this.webSessionStorage = new WebSessionStorage(this);
            this.Dependencies();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            NHibernateInitializer.Instance().InitializeNHibernateOnce(this.InitialiseNHibernateSessions);
        }

        protected void Application_Error(object sender, EventArgs e) 
        {
            // Useful for debugging
            var ex = this.Server.GetLastError();
            var reflectionTypeLoadException = ex as ReflectionTypeLoadException;
        }

        protected void Application_Start()
        {
            XmlConfigurator.Configure();
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            ModelBinders.Binders.DefaultBinder = new SharpModelBinder();
            ModelValidatorProviders.Providers.Add(new ClientDataTypeModelValidatorProvider());
            this.InitializeServiceLocator();
            RouteTable.Routes.MapHubs();
            AreaRegistration.RegisterAllAreas();
            RouteRegistrar.RegisterRoutesTo(RouteTable.Routes);
        }

        /// <summary>
        /// Instantiate the container and add all Controllers that derive from
        /// WindsorController to the container.  Also associate the Controller
        /// with the WindsorContainer ControllerFactory.
        /// </summary>
        protected virtual void InitializeServiceLocator() 
        {
            IWindsorContainer container = new WindsorContainer();

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));

            container.RegisterControllers(typeof(HomeController).Assembly);
            ComponentRegistrar.AddComponentsTo(container);

            var windsorServiceLocator = new WindsorServiceLocator(container);
            DomainEvents.ServiceLocator = windsorServiceLocator;
            ServiceLocator.SetLocatorProvider(() => windsorServiceLocator);

            GlobalHost.DependencyResolver = new SignalR.Castle.Windsor.WindsorDependencyResolver(container);
        } 
    }
}