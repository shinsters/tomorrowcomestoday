namespace TomorrowComesToday.Domain.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// The state of a player in a specific game
    /// </summary>
    public class GamePlayerState
    {
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
    }
}
