namespace TomorrowComesToday.Infrastructure.Implementations.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using SharpArch.Domain.PersistenceSupport;

    using TomorrowComesToday.Domain.Entities;
    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;

    /// <summary>
    /// Temporarily borrowed from the testing project
    /// </summary>
    public class PlayerRepository : IPlayerRepository
    {
        private readonly List<Player> players = new List<Player>();

        public IDbContext DbContext { get; private set; }

        /// <summary>
        /// Get a player by name
        /// </summary>
        /// <param name="name">The name of the player to get</param>
        /// <returns>The <see cref="Player"/>.</returns>
        public Player GetByName(string name)
        {
            return this.players.FirstOrDefault(o => o.Name == name);
        }

        public Player Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IList<Player> GetAll()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Add the player to the collection. At the moment this doesn't update because I don't know if they'll have an ID yet. Maybe I should use my own GUIDs 
        /// rather than relying on sharp arch to give it an id
        /// </summary>
        /// <param name="entity">Player to save</param>
        /// <returns>The player you just sent.</returns>
        public Player SaveOrUpdate(Player entity)
        {
            this.players.Add(entity);
            return entity;
        }

        public void Delete(Player entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
