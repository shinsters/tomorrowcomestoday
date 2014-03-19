namespace TomorrowComesToday.Infrastructure.Interfaces.Repositories
{
    /// <summary>
    /// The RepositoryFactory interface. This is for resolving dependencies when normal castle windsor
    /// dependency injection isn't totally straight forward, such as in the tests.
    /// </summary>
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Return an injected dependency
        /// </summary>
        /// <typeparam name="T">The interface to return an instantiated dependency
        /// </typeparam>
        /// <returns>The <see cref="T"/></returns>
        T For<T>() where T : class;
    }
}
