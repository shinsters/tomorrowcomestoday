namespace TomorrowComesToday.Tests.TestImplementations.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SharpArch.Domain.PersistenceSupport;

    using TomorrowComesToday.Domain.Entities;
    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;

    /// <summary>
    /// Game state repository for tests. This might end up being moved into live if we don't persist them.
    /// </summary>
    public class TestGameRepository : IGameRepository
    {
        private readonly List<Game> gameStates = new List<Game>();

        public IDbContext DbContext { get; private set; }

        public Game Get(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Game> GetAll()
        {
            throw new NotImplementedException();
        }

        public Game SaveOrUpdate(Game entity)
        {
            this.gameStates.Add(entity);
            return entity;
        }

        public void Delete(Game entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The get by guid.
        /// </summary>
        /// <param name="id">The guid of a game</param>
        /// <returns>The <see cref="Game"/>.</returns>
        public Game GetByGuid(Guid id)
        {
            return this.gameStates.FirstOrDefault(o => o.GameGuid == id);
        }

        /// <summary>
        /// The get by guid.
        /// </summary>
        /// <param name="id">The guid of a game</param>
        /// <returns>The <see cref="Game"/>.</returns>
        public Game GetByGuid(string id)
        {
            var guid = Guid.ParseExact(id, "D");
            return this.GetByGuid(guid);
        }
    }
}
