namespace TomorrowComesToday.Infrastructure.Interfaces.Repositories
{
    using TomorrowComesToday.Domain;

    using SharpArch.Domain.PersistenceSupport;

    /// <summary>
    /// Meet repository
    /// </summary>
    public interface IMeetRepository : IRepository<Meet>
    {
        /// <summary>
        /// The get event from slug.
        /// </summary>
        /// <param name="slug">The slug</param>
        /// <returns>The <see cref="Meet"/></returns>
        Meet GetEventFromSlug(string slug);

        /// <summary>
        /// Does a slug exist
        /// </summary>
        /// <param name="slug">The slug</param>
        /// <returns>The <see cref="bool"/></returns>
        bool SlugExists(string slug);
    }
}
