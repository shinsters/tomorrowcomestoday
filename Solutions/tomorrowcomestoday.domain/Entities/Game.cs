namespace TomorrowComesToday.Domain.Entities
{
    using System;
    using System.Collections.Generic;

    using SharpArch.Domain.DomainModel;

    using TomorrowComesToday.Domain.Enums;

    /// <summary>
    /// A game
    /// </summary>
    public class Game : Entity
    {
        /// <summary>
        /// Is the game currently active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// The external ID for the game
        /// </summary>
        public Guid GameGuid { get; set; }

        /// <summary>
        /// The state of active players
        /// </summary>
        public IList<GamePlayer> GamePlayers { get; set; }

        /// <summary>
        /// White cards that are in the active game
        /// </summary>
        public IList<GameCard> WhiteCardsInDeck { get; set; }

        /// <summary>
        /// Black cards that are in the active game
        /// </summary>
        public IList<GameCard> BlackCardsInDeck { get; set; }

        /// <summary>
        /// When was the last in game action performed
        /// </summary>
        public DateTime LastAction { get; set; }

        /// <summary>
        /// What is the state of this game
        /// </summary>
        public GameState GameState { get; set; }
    }
}
