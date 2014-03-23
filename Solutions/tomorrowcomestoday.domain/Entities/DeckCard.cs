namespace TomorrowComesToday.Domain.Entities
{
    using System;

    /// <summary>
    /// These are cards that have been 
    /// </summary>
    public class DeckCard
    {
        /// <summary>
        /// The card 
        /// </summary>
        public Card Card { get; set; }

        /// <summary>
        /// Game specific card id. This is regenerated each time to obfuscate the ids for external post backs.
        /// </summary>
        public Guid CardGuid { get; set; }

        /// <summary>
        /// Has this card been dealt to a player
        /// </summary>
        public bool HasBeenDealt { get; set; }
    }
}
