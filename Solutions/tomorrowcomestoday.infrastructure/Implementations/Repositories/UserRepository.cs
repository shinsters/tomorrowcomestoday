namespace TomorrowComesToday.Infrastructure.Implementations.Repositories
{
    using System.Collections;
    using System.Collections.Generic;

    using SharpArch.NHibernate;

    using TomorrowComesToday.Domain;
    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;

    /// <summary>
    /// The user repository.
    /// </summary>
    public class UserRepository : LinqRepository<User>, IUserRepository
    {

    }
}
