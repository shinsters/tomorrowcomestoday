namespace TomorrowComesToday.Domain.Entities
{
    using System;

    using TomorrowComesToday.Domain.Enums;

    /// <summary>
    /// These are the cards dealt to all players 
    /// </summary>
    public class GameCard 
    {
        /// <summary>
        /// The card 
        /// </summary>
        public Card Card { get; set; }

        /// <summary>
        /// Game specific card id. This is regenerated each time to obfuscate the ids for external post backs.
        /// </summary>
        public Guid GameCardGuid { get; set; }

        /// <summary>
        /// What is the current play state of this card
        /// </summary>
        public GameCardState GameCardState { get; set; }

        /// <summary>
        /// Has this card been sent to the client or not
        /// </summary>
        public bool HasBeenSentToClient { get; set; }
    }
}
