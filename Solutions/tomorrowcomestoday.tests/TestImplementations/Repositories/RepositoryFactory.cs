namespace TomorrowComesToday.Tests.TestImplementations.Repositories
{
    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;

    /// <summary>
    /// The repository factory.
    /// </summary>
    public class RepositoryFactory : IRepositoryFactory
    {
        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// The for.
        /// </summary><typeparam name="T">Type T</typeparam>
        /// <returns>The <see cref="T"/>.</returns>
        public T For<T>() where T : class
        {
            return TestKernel.Container.Resolve<T>();
        }
    }
}

