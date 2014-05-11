namespace TomorrowComesToday.Web.CastleWindsor
{
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    using SharpArch.Domain.Commands;
    using SharpArch.Domain.PersistenceSupport;
    using SharpArch.NHibernate;
    using SharpArch.NHibernate.Contracts.Repositories;
    using SharpArch.Web.Mvc.Castle;

    using TomorrowComesToday.Infrastructure.Implementations.Repositories;
    using TomorrowComesToday.Infrastructure.Implementations.Services;
    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;
    using TomorrowComesToday.Infrastructure.Interfaces.Services;
    using TomorrowComesToday.Web.Controllers;

    public class ComponentRegistrar
    {
        public static void AddComponentsTo(IWindsorContainer container) 
        {
            AddGenericRepositoriesTo(container);
            AddCustomRepositoriesTo(container);
            AddQueryObjectsTo(container);
        }

        private static void AddCustomRepositoriesTo(IWindsorContainer container) 
        {
            container.Register(
                AllTypes
                    .FromAssemblyNamed("TomorrowComesToday.Infrastructure")
                    .BasedOn(typeof(IRepositoryWithTypedId<,>))
                    .WithService.FirstNonGenericCoreInterface("TomorrowComesToday.Domain"));

            container.Register(
                  Component.For(typeof(IGameService))
                      .ImplementedBy(typeof(GameService))
                      .Named("GameService"));

            container.Register(
                    Component.For(typeof(IGameRepository))
                        .ImplementedBy(typeof(GameRepository))
                        .Named("GameRepository"));

            container.Register(
                    Component.For(typeof(ICardRepository))
                        .ImplementedBy(typeof(CardRepository))
                        .Named("CardRepository"));

            container.Register(
                    Component.For(typeof(IPlayerRepository))
                        .ImplementedBy(typeof(PlayerRepository))
                        .Named("PlayerRepository"));

            container.Register(
                Component.For(typeof(IConnectedPlayerService))
                    .ImplementedBy(typeof(ConnectedPlayerService))
                    .Named("ConnectedPlayerService"));

            container.Register(
                    Component.For(typeof(IUserContextService))
                        .ImplementedBy(typeof(UserContextService))
                        .Named("UserContextService")
                        .LifestylePerWebRequest());

                container.Register(
                    Component.For(typeof(IGameLobbyService))
                        .ImplementedBy(typeof(GameLobbyService))
                        .Named("GameLobbyService")
                        .LifestyleSingleton());
        }

        private static void AddGenericRepositoriesTo(IWindsorContainer container)
        {
            container.Register(
                Component.For(typeof(IEntityDuplicateChecker))
                    .ImplementedBy(typeof(EntityDuplicateChecker))
                    .Named("entityDuplicateChecker"));

            container.Register(
                Component.For(typeof(INHibernateRepository<>))
                    .ImplementedBy(typeof(NHibernateRepository<>))
                    .Named("nhibernateRepositoryType")
                    .Forward(typeof(IRepository<>)));

            container.Register(
                Component.For(typeof(INHibernateRepositoryWithTypedId<,>))
                    .ImplementedBy(typeof(NHibernateRepositoryWithTypedId<,>))
                    .Named("nhibernateRepositoryWithTypedId")
                    .Forward(typeof(IRepositoryWithTypedId<,>)));

            container.Register(
                    Component.For(typeof(ISessionFactoryKeyProvider))
                        .ImplementedBy(typeof(DefaultSessionFactoryKeyProvider))
                        .Named("sessionFactoryKeyProvider"));

            container.Register(
                    Component.For(typeof(ICommandProcessor))
                        .ImplementedBy(typeof(CommandProcessor))
                        .Named("commandProcessor"));
        }

        private static void AddQueryObjectsTo(IWindsorContainer container) 
        {
            container.Register(
                AllTypes.FromAssemblyNamed("TomorrowComesToday.Web")
                    .BasedOn<NHibernateQuery>() 
                    .WithService.DefaultInterfaces());

            container.Register(
                AllTypes.FromAssemblyNamed("TomorrowComesToday.Infrastructure")
                    .BasedOn(typeof(NHibernateQuery))
                    .WithService.DefaultInterfaces());

            container.Register(
                Classes.FromThisAssembly()
                .InSameNamespaceAs<GameHub>());

        }
    }
}