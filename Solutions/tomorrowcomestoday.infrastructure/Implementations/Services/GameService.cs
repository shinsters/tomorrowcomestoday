namespace TomorrowComesToday.Infrastructure.Implementations.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using TomorrowComesToday.Domain;
    using TomorrowComesToday.Domain.Entities;
    using TomorrowComesToday.Domain.Enums;
    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;
    using TomorrowComesToday.Infrastructure.Interfaces.Services;

    /// <summary>
    /// The game service, handles most of the logic of the game play
    /// </summary>
    public class GameService : IGameService
    {
        /// <summary>
        /// The game state repository.
        /// </summary>
        private readonly IGameRepository gameRepository;

        /// <summary>
        /// The card repository.
        /// </summary>
        private readonly ICardRepository cardRepository;

        /// <summary>
        /// Initialises a new instance of the <see cref="GameService"/> class.
        /// </summary>
        /// <param name="gameRepository">The game state repository.</param>
        /// <param name="cardRepository">The card repository</param>
        public GameService(
            IGameRepository gameRepository,
            ICardRepository cardRepository)
        {
            this.gameRepository = gameRepository;
            this.cardRepository = cardRepository;
        }

        /// <summary>
        /// Deal a round
        /// </summary>
        /// <param name="gameGuid">The game guid</param>
        public void DealRound(Guid gameGuid)
        {
            var game = this.gameRepository.GetByGuid(gameGuid);

            if (game == null)
            {
                return;
            }

            // if the game is just starting we need to create decks of cards
            if (game.GameState == GameState.Beginning){
                
                this.CreateDeck(game);
                game.GameState = GameState.BeingPlayed;
            }

            // then assign a selection to a user
            this.DealWhiteTurn(game);
            this.DealBlackTurn(game);
        }

        /// <summary>
        /// Create the decks at the beginning of the game
        /// </summary>
        /// <param name="game">The active game</param>
        private void CreateDeck(Game game)
        {
            // just get them all for the moment
            var whiteCards = cardRepository.GetCardFromDeck(CardType.White);
            var whiteCardsInDeck = this.GenerateCardsInDeck(whiteCards);
            game.WhiteCardsInDeck = whiteCardsInDeck;

            var blackCards = cardRepository.GetCardFromDeck(CardType.Black);
            var blackCardsInDeck = this.GenerateCardsInDeck(blackCards);
            game.BlackCardsInDeck = blackCardsInDeck;
        }

        /// <summary>
        /// Deal from deck to players
        /// </summary>
        /// <param name="game"></param>
        private void DealWhiteTurn(Game game)
        {
            // work out how many cards we need
            var numberOfCardsRequired = game.GamePlayerStates.Sum(gamePlayerState => CommonConcepts.HandSize - gamePlayerState.CardsInHand.Count);

            // get either the number of required cards, of if there aren't enough - every card that's left
            var cardToDeal = game.WhiteCardsInDeck.Count(o => !o.HasBeenDealt) > numberOfCardsRequired 
                ? game.WhiteCardsInDeck.Take(numberOfCardsRequired).ToList() 
                : game.WhiteCardsInDeck.Where(o => !o.HasBeenDealt).ToList();

            var playerCounter = 0;

            foreach (var card in cardToDeal)
            {
                card.HasBeenDealt = true;

                if (playerCounter > game.GamePlayerStates.Count - 1)
                {
                    playerCounter = 0;
                }

                game.GamePlayerStates[playerCounter].CardsInHand.Add(card.Card);
                playerCounter++;
            }
        }

        /// <summary>
        /// Generate an in game card type, containing game specific data
        /// </summary>
        /// <param name="cardsInDeck">Collection of cards to use</param>
        /// <returns>A shuffled deck of cards</returns>
        private IList<InGameCard> GenerateCardsInDeck(IEnumerable<Card> cardsInDeck)
        {
            var deckCards = new List<InGameCard>();

            foreach (var card in cardsInDeck)
            {
                deckCards.Add(new InGameCard
                {
                    Card = card,
                    CardGuid = Guid.NewGuid(),
                    HasBeenDealt = false

                });
            }

            // shuffling by the card guid should give us some kind of shuffle for the deck.
            // if it's crap then i'll make a better shuffle.
            return deckCards.OrderBy(o => o.CardGuid).ToList();
        }

        /// <summary>
        /// Deal a new black card
        /// </summary>
        /// <param name="game">The active game</param> 
        private void DealBlackTurn(Game game)
        {
            // first mark any old cards as inactive
            foreach (var blackCard in game.BlackCardsInDeck.Where(o => o.IsCurrentCard))
            {
                blackCard.IsCurrentCard = false;
                blackCard.HasBeenDealt = true;
            }

            // now play the next one
            var gameCard = game.BlackCardsInDeck.FirstOrDefault(o => !o.HasBeenDealt);

            // we'll run out of cards eventually
            if (gameCard != null)
            {
                gameCard.IsCurrentCard = true;
            }
        }
    }
}
