namespace TomorrowComesToday.Domain.Entities
{
    using System;
    using System.Collections.Generic;

    using TomorrowComesToday.Domain.Enums;

    /// <summary>
    /// The state of a player in a specific game
    /// </summary>
    public class GamePlayer
    {
        /// <summary>
        /// The numerical ID of the player in the game
        /// </summary>
        public int GamePlayerId { get; set; }

        /// <summary>
        /// The GUID of the player in the current game
        /// </summary>
        public Guid GamePlayerGuid { get; set; }

        /// <summary>
        /// The player this represents
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        /// Number of points a player has
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// The cards the player current has in their hand
        /// </summary>
        public IList<GameCard> WhiteCardsInHand { get; set; }

        /// <summary>
        /// What is the current state of the player in the game
        /// </summary>
        public PlayerState PlayerState { get; set; }

        /// <summary>
        /// The last time the player interacted with the game. 
        /// </summary>
        public DateTime LastAction { get; set; }

        /// <summary>
        /// Last time the game received a heart beat from the users client
        /// </summary>
        public DateTime LastHeartBeat { get; set; }
    }
}
