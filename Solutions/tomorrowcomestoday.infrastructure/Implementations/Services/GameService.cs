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
            var gameState = this.gameStateRepository.GetByGuid(gameGuid);

            if (gameState == null)
            {
                return;
            }

            // first generate a deck
            var cardsAlreadyPlayedInGame = this.CardsAlreadyPlayedInGame(gameState);
            var deck = this.cardRepository.GetCardFromDeck(CardType.White, cardsAlreadyPlayedInGame);
            gameState.CardsInTheDeck = this.GenerateCardsInDeck(deck);

            // then assign a selection to a user
            this.DealToPlayers(gameState);
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
        /// Deal from deck to players
        /// </summary>
        /// <param name="gameState"></param>
        private void DealToPlayers(GameState gameState)
        {
            foreach (var gamePlayerState in gameState.GamePlayerStates)
            {
                // this condition might happen at any point, so we'll need to keep checking for it
                if (gameState.CardsInTheDeck.All(o => o.HasBeenDealt))
                {
                    return;
                }

                var cardsNeeded = CommonConcepts.HandSize - gamePlayerState.CardsInHand.Count;

                for (var i = 0; i < cardsNeeded; i++)
                {
                    var card = gameState.CardsInTheDeck.FirstOrDefault(o => !o.HasBeenDealt);
                    if (card == null)
                    {
                        // then we've dealt the last card, fall out of this whole method
                        return;
                    }

                    gamePlayerState.CardsInHand.Add(card.Card);

                    card.HasBeenDealt = true;
                }
            }
        }

        /// <summary>
        /// Returns a list of cards already been dealt in a game
        /// </summary>
        /// <param name="gameState">The game</param>
        /// <returns>A list of cards already dealt in game</returns>
        private IList<Card> CardsAlreadyPlayedInGame(GameState gameState)
        {
            return gameState.CardsInTheDeck.Select(o => o.Card).ToList();
        }

        /// <summary>
        /// Generate a list of entity 
        /// </summary>
        /// <param name="cardsInDeck"></param>
        /// <returns></returns>
        private IList<DeckCard> GenerateCardsInDeck(IEnumerable<Card> cardsInDeck)
        {
            var deckCards = new List<DeckCard>();

            foreach (var card in cardsInDeck)
            {
                deckCards.Add(new DeckCard
                                  {
                                      Card = card,
                                      CardGuid = Guid.NewGuid(),
                                      HasBeenDealt = false
                                  });
            }

            // und shuffle bitte
            return deckCards.OrderBy(o => o.CardGuid).ToList();
        }
    }
}
