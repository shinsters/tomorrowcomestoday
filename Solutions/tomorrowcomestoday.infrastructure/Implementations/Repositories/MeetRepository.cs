namespace TomorrowComesToday.Infrastructure.Implementations.Repositories
{
    using TomorrowComesToday.Domain;
    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;

    using SharpArch.NHibernate;

    /// <summary>
    /// The user repository.
    /// </summary>
    public class MeetRepository : LinqRepository<Meet>, IMeetRepository
    {
        /// <summary>
        /// The get event from slug.
        /// </summary>
        /// <param name="slug">The slug</param>
        /// <returns>The <see cref="Meet"/></returns>
        public Meet GetEventFromSlug(string slug)
        {
            return Session.QueryOver<Meet>()
                .Where(o => o.Slug == slug)
                .SingleOrDefault();
        }

        /// <summary>
        /// Does a slug exist
        /// </summary>
        /// <param name="slug">The slug</param>
        /// <returns>The <see cref="bool"/></returns>
        public bool SlugExists(string slug)
        {
            return Session.QueryOver<Meet>()
                    .And(o => o.Slug == slug)
                    .RowCount() != 0;
        }
    }
}
