namespace TomorrowComesToday.Domain.Entities
{
    using System;
    using System.Collections.Generic;

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
        public IList<Card> CardsInHand { get; set; }

        /// <summary>
        /// Is the player the active card tzar?
        /// </summary>
        public bool IsActivePlayer { get; set; }

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
