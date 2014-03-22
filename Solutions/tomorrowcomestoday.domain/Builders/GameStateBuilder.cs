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
        /// Adds a single player to the game
        /// </summary>
        /// <param name="player"></param>
        /// <returns>The game state builder</returns>
        public GameStateBuilder AddPlayer(Player player)
        {
            if (this.entity.ActivePlayers == null)
            {
                this.entity.ActivePlayers = new List<Player>();
            }

            return this;
        }

        /// <summary>
        /// Adds a collection of players to the game
        /// </summary>
        /// <param name="players"></param>
        /// <returns>The game state builder</returns>
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

        /// <summary>
        /// Only add a GUID if you need to know the id of it, like testing. These will be randomly allocated
        /// when the object is constructed otherwise.
        /// </summary>
        /// <param name="id">The GUID to use as the Id</param>
        /// <returns>The game state builder</returns>
        public GameStateBuilder WithGuid(Guid id)
        {
            this.entity.GameGuid = id;
            return this;
        }

        /// <summary>
        /// Returns a completed game state object
        /// </summary>
        /// <returns>A completed game state object</returns>
        public GameState Create()
        {
            // GUIDs are only manually set if in a testing environment and the id of the game is needed. Otherwise this
            // should be done automatically
            if (this.entity.GameGuid == Guid.Empty)
            {
                this.entity.GameGuid = Guid.NewGuid();
            }

            return this.entity;
        }
    }
}
