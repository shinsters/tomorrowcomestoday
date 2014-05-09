namespace TomorrowComesToday.Domain.Builders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using TomorrowComesToday.Domain.Entities;
    using TomorrowComesToday.Domain.Enums;

    /// <summary>
    /// Creates a game
    /// </summary>
    public class GameBuilder
    {
        private readonly Game entity = new Game();

        static Random random = new Random();

        /// <summary>
        /// Adds a single player to the game
        /// </summary>
        /// <param name="player"></param>
        /// <returns>The game state builder</returns>
        public GameBuilder AddPlayer(Player player)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a collection of players to the game
        /// </summary>
        /// <param name="players"></param>
        /// <returns>The game state builder</returns>
        public GameBuilder AddPlayers(IList<Player> players)
        {
            if (this.entity.GamePlayers == null)
            {
                this.entity.GamePlayers = new List<GamePlayer>();
            }

            var playerCounter = 1;
            foreach (var player in players)
            {

                this.entity.GamePlayers.Add(new GamePlayer
                                                     {
                                                         GamePlayerId = playerCounter,
                                                         CardsInHand = new List<Card>(),
                                                         Player = player,
                                                         Points = 0
                                                     });

                playerCounter++;
            }

            // a first player now needs to be randomly selected
            var randomPlayerId = random.Next(players.Count) + 1;
            var randomFirstPlayer = this.entity.GamePlayers.First(o => o.GamePlayerId == randomPlayerId);

            randomFirstPlayer.IsActivePlayer = true;

            return this;
        }

        /// <summary>
        /// Only add a GUID if you need to know the id of it, like testing. These will be randomly allocated
        /// when the object is constructed otherwise.
        /// </summary>
        /// <param name="id">The GUID to use as the Id</param>
        /// <returns>The game state builder</returns>
        public GameBuilder WithGuid(Guid id)
        {
            this.entity.GameGuid = id;
            return this;
        }

        /// <summary>
        /// Returns a completed game state object
        /// </summary>
        /// <returns>A completed game state object</returns>
        public Game Create()
        {
            // GUIDs are only manually set if in a testing environment and the id of the game is needed. Otherwise this
            // should be done automatically
            if (this.entity.GameGuid == Guid.Empty)
            {
                this.entity.GameGuid = Guid.NewGuid();
            }

            this.entity.IsActive = true;
            this.entity.WhiteCardsInDeck = new List<InGameCard>();
            this.entity.BlackCardsInDeck = new List<InGameCard>();
            this.entity.GameState = GameState.Beginning;

            return this.entity;
        }
    }
}
