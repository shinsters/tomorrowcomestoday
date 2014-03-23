namespace TomorrowComesToday.Infrastructure.Implementations.Repositories
{
    using System.Collections.Generic;

    using SharpArch.NHibernate;

    using TomorrowComesToday.Domain.Entities;
    using TomorrowComesToday.Domain.Enums;
    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;

    /// <summary>
    /// The card repository.
    /// </summary>
    public class CardRepository : LinqRepository<Card>, ICardRepository
    {
        public IList<Card> GetCardFromDeck(int numberRequired, CardType cardType, IList<Card> cardsToExclude)
        {
            throw new System.NotImplementedException();
        }

        public IList<Card> GetCardFromDeck(CardType cardType, IList<Card> cardsToExclude)
        {
            throw new System.NotImplementedException();
        }
    }
}
