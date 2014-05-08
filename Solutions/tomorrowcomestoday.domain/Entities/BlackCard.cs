﻿namespace TomorrowComesToday.Domain.Entities
{
    using System;

    /// <summary>
    /// The cards with the questions that are played one a turn
    /// </summary>
    class BlackCard
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
        /// Has this card been dealt 
        /// </summary>
        public bool HasBeenDealt { get; set; }
    }
}