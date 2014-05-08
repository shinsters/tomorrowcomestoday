namespace TomorrowComesToday.Tests.TestImplementations.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using SharpArch.Domain.PersistenceSupport;

    using TomorrowComesToday.Domain.Builders;
    using TomorrowComesToday.Domain.Entities;
    using TomorrowComesToday.Domain.Enums;
    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;

    /// <summary>
    /// An in memory dumb version of the test card repository
    /// </summary>
    public class TestCardRepository : ICardRepository
    {
        /// <summary>
        /// Path to the black cards for loading resources
        /// </summary>
        private const string BlackCardsResourceLocation = "TomorrowComesToday.Domain.Resources.wcards.txt";

        /// <summary>
        /// Path to the white cards for loading resources
        /// </summary>
        private const string WhiteCardsResourceLocation = "";

        /// <summary>
        /// Constructs a test card repository
        /// </summary>
        public TestCardRepository()
        {
            this.Cards = this.GetCardsFromResource();
        }  

        public IDbContext DbContext { get; set; }

        /// <summary>
        /// Domain object
        /// </summary>
        private IList<Card> Cards { get; set; }

        /// <summary>
        /// Get a number of cards from the deck
        /// </summary> 
        /// <param name="numberRequired">The number Required</param>
        /// <param name="cardsToExclude">The cards to exclude, so already dealt</param>
        /// <returns>The <see cref="System.Collections.IList"/> of cards</returns>
        public IList<Card> GetCardFromDeck(int numberRequired, IList<Card> cardsToExclude)
        {
            throw new NotImplementedException();
        }

        public Card Get(int id)
        {
            return this.Cards.FirstOrDefault(o => o.Id == id);
        }

        public IList<Card> GetAll()
        {
            return this.Cards;
        }

        public Card SaveOrUpdate(Card entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Card entity)
        {
            throw new NotImplementedException();
        }

        public IList<Card> GetCardFromDeck(int numberRequired, CardType cardType, IList<Card> cardsToExclude)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a collection of cards from the repository
        /// </summary>
        /// <param name="cardType"></param>
        /// <param name="cardsToExclude"></param>
        /// <returns></returns>
        public IList<Card> GetCardFromDeck(CardType cardType, IList<Card> cardsToExclude)
        {
            return this.Cards
                .Where(o => o.CardType == cardType)
                .Where(card => cardsToExclude.All(o => o.CardGuid != card.CardGuid))
                .ToList();
        }

        private IList<Card> GetCardsFromResource()
        {
            var cards = new List<Card>();
            
            // we need to get the list of cards from a resource in this situation.
            // in the full game this'll persisted entities inside the database.
            // these are stored in the domain assembly
            var assembly = typeof(Card).Assembly;

            var resourceStream = assembly.GetManifestResourceStream(BlackCardsResourceLocation);

            if (resourceStream == null)
            {
                throw new Exception(string.Format("Unable to load resource {0}", BlackCardsResourceLocation));
            }

            string filesAsString;

            // the file being loaded was provided by cards against humanity's website. 
            // so the formatting is a little unusual for what we need. But rather than
            // modify their file, just deal with it so to retain compatability
            using (var reader = new StreamReader(resourceStream))
            {
                filesAsString = reader.ReadToEnd();
            }

            // first split the file on cards=
            // this is a bit crap, if a card has an = in it it'll break.
            // but good enough just for test data?
            filesAsString = filesAsString.Split('=')[1];

            foreach (var cardText in filesAsString.Split(new[] { "<>" }, StringSplitOptions.RemoveEmptyEntries))
            {
                var card = new CardBuilder()
                    .Text(cardText)
                    .Type(CardType.White)
                    .Create();

                cards.Add(card);
            }

            return cards;
        }
    }
}
