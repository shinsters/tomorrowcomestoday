namespace TomorrowComesToday.Domain.Builders
{
    using System;
    
    using TomorrowComesToday.Domain.Entities;
    using TomorrowComesToday.Domain.Enums;

    /// <summary>
    /// Builds a card
    /// </summary>
    public class CardBuilder
    {
        /// <summary>
        /// The card being constructed
        /// </summary>
        private readonly Card entity = new Card();

        /// <summary>
        /// Sets the text of the card
        /// </summary>
        /// <param name="title"></param>
        /// <returns>The fluent card construction interface</returns>
        public CardBuilder Text(string title)
        {
            this.entity.Text = title;
            return this;
        }

        /// <summary>
        /// Sets the card type
        /// </summary>
        /// <param name="cardType">The type of card being set</param>
        /// <returns>The fluent card construction interface</returns>
        public CardBuilder Type(CardType cardType)
        {
            this.entity.CardType = cardType;
            return this;
        }

        /// <summary>
        /// Finalise and return constructed object
        /// </summary>
        /// <returns>A built card object</returns>
        public Card Create()
        {
            if (string.IsNullOrEmpty(this.entity.Text))
            {
                throw new Exception("Card requires text to be set");
            }

            this.entity.CardGuid = Guid.NewGuid();

            return this.entity;
        }
    }
}
