namespace TomorrowComesToday.Infrastructure.Implementations.Repositories
{
    using TomorrowComesToday.Domain;
    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;

    using SharpArch.NHibernate;

    /// <summary>
    /// The user repository.
    /// </summary>
    public class ForumRepository : LinqRepository<Forum>, IForumRepository
    {

    }
}
