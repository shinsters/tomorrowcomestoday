namespace TomorrowComesToday.Tests
{
    using Castle.Windsor;

    using CommonServiceLocator.WindsorAdapter;

    using Microsoft.Practices.ServiceLocation;

    using SharpArch.Domain.Events;

    using TomorrowComesToday.Tests.CastleWindsor;

    /// <summary>
    /// Initialise dependencies for tests
    /// </summary>
    public class TestKernel
    {
        /// <summary>
        /// Castle windsor container
        /// </summary>
        public static WindsorContainer Container { get; private set; }

        /// <summary>
        /// Set up background dependencies
        /// </summary>
        public static void Initialise()
        {
            InitaliseServiceLocator();
        }

        /// <summary>
        /// Start up castle windsor
        /// </summary>
        public static void InitaliseServiceLocator()
        {
            IWindsorContainer container = new WindsorContainer();

            ComponentRegistrar.AddComponentsTo(container);

            var windsorServiceLocator = new WindsorServiceLocator(container);
            DomainEvents.ServiceLocator = windsorServiceLocator;
            ServiceLocator.SetLocatorProvider(() => windsorServiceLocator);

            Container = (WindsorContainer)container;
        }
    }
}
