namespace TomorrowComesToday.Tests.TestImplementations.Repositories
{
    using System.Collections.Generic;

    using SharpArch.Domain.PersistenceSupport;

    using TomorrowComesToday.Domain.Entities;
    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;

    /// <summary>
    /// Player repository holding everything in memory for tests
    /// </summary>
    public class TestPlayerRepository : IPlayerRepository
    {
        private readonly List<Player> players = new List<Player>();

        public IDbContext DbContext { get; private set; }

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
