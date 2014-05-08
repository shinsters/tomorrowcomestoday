namespace TomorrowComesToday.Domain.Entities
{
    using System;
    using System.Collections.Generic;

    using SharpArch.Domain.DomainModel;

    /// <summary>
    /// A game state
    /// </summary>
    public class GameState : Entity
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
        public IList<GamePlayerState> GamePlayerStates { get; set; }

        /// <summary>
        /// White cards that are in the active game
        /// </summary>
        public IList<InGameCard> WhiteCardsInDeck { get; set; }

        /// <summary>
        /// Black cards that are in the active game
        /// </summary>
        public IList<InGameCard> BlackCardsInDeck { get; set; }

        /// <summary>
        /// When was the last in game action performed
        /// </summary>
        public DateTime LastAction { get; set; }
    }
}
