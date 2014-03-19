namespace TomorrowComesToday.Infrastructure.Implementations.Repositories
{
    using System.Collections;
    using System.Collections.Generic;

    using SharpArch.NHibernate;

    using TomorrowComesToday.Domain.Entities;
    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;

    /// <summary>
    /// The card repository.
    /// </summary>
    public class CardRepository : LinqRepository<Card>, ICardRepository
    {
        /// <summary>
        /// Get a number of cards from the deck
        /// </summary> 
        /// <param name="numberRequired">The number Required</param>
        /// <param name="cardsToExclude">The cards to exclude, so already dealt</param>
        /// <returns>The <see cref="IList"/> of cards</returns>
        public IList<Card> GetBlackFromDeck(int numberRequired, IList<Card> cardsToExclude)
        {
            throw new System.NotImplementedException();
        }
    }
}
