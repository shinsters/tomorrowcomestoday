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
    public class TestGameStateRepository : IGameStateRepository
    {
        private readonly List<GameState> gameStates = new List<GameState>();

        public IDbContext DbContext { get; private set; }

        public GameState Get(int id)
        {
            throw new NotImplementedException();
        }

        public IList<GameState> GetAll()
        {
            throw new NotImplementedException();
        }

        public GameState SaveOrUpdate(GameState entity)
        {
            this.gameStates.Add(entity);
            return entity;
        }

        public void Delete(GameState entity)
        {
            throw new NotImplementedException();
        }

        public GameState GetByGuid(Guid id)
        {
            return this.gameStates.FirstOrDefault(o => o.GameGuid == id);
        }
    }
}
