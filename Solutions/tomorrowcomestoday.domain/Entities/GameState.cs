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
        /// Cards that have been dealt so shouldn't be reused
        /// </summary>
        public IList<Card> DealtCards { get; set; }

        /// <summary>
        /// When was the last in game action performed
        /// </summary>
        public DateTime LastAction { get; set; }
    }
}
