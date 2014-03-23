namespace TomorrowComesToday.Infrastructure.Interfaces.Repositories
{
    using System.Collections;
    using System.Collections.Generic;

    using SharpArch.Domain.PersistenceSupport;

    using TomorrowComesToday.Domain.Entities;
    using TomorrowComesToday.Domain.Enums;

    /// <summary>
    /// The card repository.
    /// </summary>
    public interface ICardRepository : IRepository<Card>
    {
        /// <summary>
        /// Get a number of cards from the deck
        /// </summary>
        /// <param name="numberRequired">The number Required</param>
        /// <param name="cardType">The card Type</param>
        /// <param name="cardsToExclude">The cards to exclude, so already dealt</param>
        /// <returns>The <see cref="IList"/> of cards</returns>
        IList<Card> GetCardFromDeck(int numberRequired, CardType cardType, IList<Card> cardsToExclude);

        /// <summary>
        /// Get a number of cards from the deck
        /// </summary>
        /// <param name="cardType">The card Type</param>
        /// <param name="cardsToExclude">The cards to exclude, so already dealt</param>
        /// <returns>The <see cref="IList"/> of cards</returns>
        IList<Card> GetCardFromDeck(CardType cardType, IList<Card> cardsToExclude);
    }
}
