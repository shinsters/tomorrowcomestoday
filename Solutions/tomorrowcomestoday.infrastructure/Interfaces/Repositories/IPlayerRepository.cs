namespace TomorrowComesToday.Infrastructure.Interfaces.Repositories
{
    using SharpArch.Domain.PersistenceSupport;

    using TomorrowComesToday.Domain.Entities;

    /// <summary>
    /// Holds players in the system
    /// At the moment, these are just going to be in memory I think.
    /// </summary>
    public interface IPlayerRepository : IRepository<Player>
    {
        /// <summary>
        /// Get a player by name
        /// </summary>
        /// <param name="name">The name of the player to get</param>
        /// <returns>The <see cref="Player"/>.</returns>
        Player GetByName(string name);
    }
}
