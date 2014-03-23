namespace TomorrowComesToday.Domain.Entities
{
    using System;

    using SharpArch.Domain.DomainModel;

    using TomorrowComesToday.Domain.Enums;

    /// <summary>
    /// The cards used in play
    /// </summary>
    public class Card : Entity
    {
        /// <summary>
        /// What type of card is this.
        /// </summary>
        public CardType CardType { get; set; }

        /// <summary>
        /// The text for the card
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The persisted card guid, this is here for tests really
        /// </summary>
        public Guid CardGuid { get; set; }
    }
}
