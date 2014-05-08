namespace TomorrowComesToday.Infrastructure.Implementations.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json.Schema;

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
        private readonly IGameStateRepository gameStateRepository;

        /// <summary>
        /// The card repository.
        /// </summary>
        private readonly ICardRepository cardRepository;

        /// <summary>
        /// Initialises a new instance of the <see cref="GameService"/> class.
        /// </summary>
        /// <param name="gameStateRepository">The game state repository.</param>
        /// <param name="cardRepository">The card repository</param>
        public GameService(
            IGameStateRepository gameStateRepository,
            ICardRepository cardRepository)
        {
            this.gameStateRepository = gameStateRepository;
            this.cardRepository = cardRepository;
        }

        /// <summary>
        /// Get a deck then deal to players
        /// </summary>
        /// <param name="gameGuid">The game guid</param>
        public void DealGame(Guid gameGuid)
        {
            this.DealGame(gameGuid, null);
        }

        /// <summary>
        /// Get a deck then deal to players
        /// </summary>
        /// <param name="deckSize">The deck Size</param>
        /// <param name="gameGuid">The game guid</param>
        public void DealGame(int deckSize, Guid gameGuid)
        {
            this.DealGame(gameGuid, deckSize);
            this.StartRound(gameGuid);
        }

        /// <summary>
        /// Deal to players from existing deck
        /// </summary>
        /// <param name="gameGuid">The game Guid</param>
        public void DealToPlayers(Guid gameGuid)
        {
            var gameState = this.gameStateRepository.GetByGuid(gameGuid);

            if (gameState == null)
            {
                return;
            }

            this.DealToPlayers(gameState);
        }

        /// <summary>
        /// Get a deck then deal to players
        /// </summary>
        /// <param name="deckSize">The deck size</param>
        /// <param name="gameGuid">The game guid</param>
        private void DealGame(Guid gameGuid, int? deckSize)
        {
            var gameState = this.gameStateRepository.GetByGuid(gameGuid);

            if (gameState == null)
            {
                return;
            }

            // first generate a bunch of cards to deal
            var whiteCardsAlreadyPlayed = this.CardsAlreadyPlayedInGame(gameState, CardType.White);
            var whiteDeck = this.cardRepository.GetCardFromDeck(CardType.White, whiteCardsAlreadyPlayed);

            var cardsInDeck = this.GenerateCardsInDeck(whiteDeck);

            gameState.WhiteCardsInDeck = deckSize.HasValue ? cardsInDeck.Take(deckSize.Value).ToList() : cardsInDeck;

            // then assign a selection to a user
            this.DealToPlayers(gameState);

        }

        /// <summary>
        /// Deal from deck to players
        /// </summary>
        /// <param name="gameState"></param>
        private void DealToPlayers(GameState gameState)
        {
            // work out how many cards we need
            var numberOfCardsRequired =
                gameState.GamePlayerStates.Sum(
                    gamePlayerState => CommonConcepts.HandSize - gamePlayerState.CardsInHand.Count);

            // get either the number of required cards, of if there aren't enough - every card that's left
            var cardToDeal = gameState.WhiteCardsInDeck.Count(o => !o.HasBeenDealt) > numberOfCardsRequired 
                ? gameState.WhiteCardsInDeck.Take(numberOfCardsRequired).ToList() 
                : gameState.WhiteCardsInDeck.Where(o => !o.HasBeenDealt).ToList();

            var playerCounter = 0;

            foreach (var card in cardToDeal)
            {
                card.HasBeenDealt = true;

                if (playerCounter > gameState.GamePlayerStates.Count - 1)
                {
                    playerCounter = 0;
                }

                gameState.GamePlayerStates[playerCounter].CardsInHand.Add(card.Card);
                playerCounter++;
            }
        }

        /// <summary>
        /// Returns a list of cards already been dealt in a game
        /// </summary>
        /// <param name="gameState">The game</param>
        /// <param name="type">The type of card</param>
        /// <returns>A list of cards already dealt in game</returns>
        private IList<Card> CardsAlreadyPlayedInGame(GameState gameState, CardType type)
        {
            switch (type)
            {
                case CardType.Black:
                    return gameState.BlackCardsInDeck.Select(o => o.Card).ToList();

                default:
                    return gameState.WhiteCardsInDeck.Select(o => o.Card).ToList();
            }
        }

        /// <summary>
        /// Generate an in game card type, containing game specific data
        /// </summary>
        /// <param name="cardsInDeck">Collection of cards to use</param>
        /// <returns></returns>
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

            // und shuffle bitte
            return deckCards.OrderBy(o => o.CardGuid).ToList();
        }

        /// <summary>
        /// Begin a new round in a game
        /// </summary>
        /// <param name="gameGuid"></param>
        private void StartRound(Guid gameGuid)
        {
            var gameState = this.gameStateRepository.GetByGuid(gameGuid);

            // first mark any old cards as inactive
            foreach (var blackCard in gameState.BlackCardsInDeck.Where(o => o.IsCurrentCard))
            {
                blackCard.IsCurrentCard = false;
                blackCard.HasBeenDealt = true;
            }

            // now play the next one
            var gameCard = gameState.BlackCardsInDeck.FirstOrDefault(o => !o.HasBeenDealt);

            // we'll run out of cards eventually
            if (gameCard != null)
            {
                gameCard.IsCurrentCard = true;
            }

            // todo, deal with running out of cards
        }
    }
}
