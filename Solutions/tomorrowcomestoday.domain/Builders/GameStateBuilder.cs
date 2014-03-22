namespace TomorrowComesToday.Domain.Builders
{
    using System;
    using System.Collections.Generic;

    using TomorrowComesToday.Domain.Entities;

    /// <summary>
    /// Creates a game state
    /// </summary>
    public class GameStateBuilder
    {
        private readonly GameState entity = new GameState();

        /// <summary>
        /// Adds a single player to a game
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public GameStateBuilder AddPlayer(Player player)
        {
            if (this.entity.ActivePlayers == null)
            {
                this.entity.ActivePlayers = new List<Player>();
            }

            return this;
        }

        public GameStateBuilder AddPlayers(IList<Player> players)
        {
            if (this.entity.ActivePlayers == null)
            {
                this.entity.ActivePlayers = new List<Player>();
            }

            foreach (var player in players)
            {
                this.entity.ActivePlayers.Add(player);
            }

            return this;
        }

        public GameState Create()
        {
            this.entity.GameGuid = Guid.NewGuid();
            return this.entity;
        }

    }
}
