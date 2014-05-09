namespace TomorrowComesToday.Infrastructure.Interfaces.Repositories
{
    using System;

    using SharpArch.Domain.PersistenceSupport;

    using TomorrowComesToday.Domain.Entities;

    /// <summary>
    /// Holds games
    /// </summary>
    public interface IGameRepository : IRepository<Game>
    {
        /// <summary>
        /// The get by guid.
        /// </summary>
        /// <param name="id">The guid of a game</param>
        /// <returns>The <see cref="Game"/>.</returns>
        Game GetByGuid(Guid id);

        /// <summary>
        /// The get by guid.
        /// </summary>
        /// <param name="id">The guid of a game</param>
        /// <returns>The <see cref="Game"/>.</returns>
        Game GetByGuid(string id);
    }
}
