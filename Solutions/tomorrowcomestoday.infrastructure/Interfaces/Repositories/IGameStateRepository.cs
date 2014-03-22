namespace TomorrowComesToday.Infrastructure.Interfaces.Repositories
{
    using System;

    using SharpArch.Domain.PersistenceSupport;

    using TomorrowComesToday.Domain.Entities;

    /// <summary>
    /// Holds game states
    /// </summary>
    public interface IGameStateRepository : IRepository<GameState>
    {
        /// <summary>
        /// The get by guid.
        /// </summary>
        /// <param name="id">The guid of a game</param>
        /// <returns>The <see cref="GameState"/>.</returns>
        GameState GetByGuid(Guid id);
    }
}
