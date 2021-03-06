﻿namespace TomorrowComesToday.Infrastructure.Implementations.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using TomorrowComesToday.Domain;
    using TomorrowComesToday.Domain.Entities;
    using TomorrowComesToday.Domain.Enums;
    using TomorrowComesToday.Infrastructure.Enums;
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
        /// <param name="gameRepository">The game repository.</param>
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
            if (game.GameState == GameState.Beginning)
            {
                this.CreateDeck(game);
                game.GameState = GameState.BeingPlayed;
            }
            else
            {
                // assign the next active player if it's not the first turn  
                var activePlayer = game.GamePlayers.First(o => o.PlayerState == PlayerState.IsActivePlayerSelecting);
                var nextActivePlayer = game.GamePlayers.FirstOrDefault(o => o.GamePlayerId == activePlayer.GamePlayerId + 1)
                                       ?? game.GamePlayers.OrderBy(o => o.GamePlayerId).First();

                activePlayer.PlayerState = PlayerState.IsNormalPlayerSelecting;
                nextActivePlayer.PlayerState = PlayerState.IsActivePlayerWaiting;
            }
            
            // mark all played cards as such
            foreach (var gamePlayer in game.GamePlayers)
            {
                foreach (var gameCard in gamePlayer.WhiteCardsInHand.Where(o => o.GameCardState == GameCardState.IsInPlay))
                {
                    gameCard.GameCardState = GameCardState.HasBeenPlayed;
                }
            }

            // then assign a selection to a user
            this.DealWhiteTurn(game);
            this.DealBlackTurn(game);
        }

        /// <summary>
        /// Plays a white card from a player
        /// </summary>
        /// <param name="gameGuid">The GUID of the game</param>
        /// <param name="gamePlayerGuid">The GUID of the in game player attempting to play the card</param>
        /// <param name="gameCardGuid">The GUID of the in game card attempting to be played</param>
        /// <returns>The <see cref="CardPlayStateEnum"/>.</returns>
        public CardPlayStateEnum PlayWhiteCard(Guid gameGuid, Guid gamePlayerGuid, Guid gameCardGuid)
        {
            // I guess technically we don't need the guid of the game, but doesn't seem nice 
            // to get that out of lists. Maybe smarten this up later.
            var game = this.gameRepository.GetByGuid(gameGuid);

            // first check to see the card exists in the game
            var cardInGame = game.WhiteCardsInDeck.FirstOrDefault(o => o.GameCardGuid == gameCardGuid);

            if (cardInGame == null)
            {
                return CardPlayStateEnum.WasNotPlayed;
            }

            // now check card is currently in the game
            var playerOwningCard = game.GamePlayers.FirstOrDefault(o => o.WhiteCardsInHand.Any(c => c.GameCardGuid == gameCardGuid));

            if (playerOwningCard == null)
            {
                return CardPlayStateEnum.WasNotPlayed;
            }

            // and check the owner is as expected
            var correctPlayerPlayingCard = playerOwningCard.GamePlayerGuid == gamePlayerGuid;

            if (!correctPlayerPlayingCard)
            {
                return CardPlayStateEnum.WasNotPlayed;
            }

            // and check that the player is allowed to play currently
            if (playerOwningCard.PlayerState != PlayerState.IsNormalPlayerSelecting)
            {
                return CardPlayStateEnum.WasNotPlayed;
            }

            // otherwise mark the card as played, and the player as having played it
            cardInGame.GameCardState = GameCardState.IsInPlay;
            playerOwningCard.PlayerState = PlayerState.IsNormalPlayerWaiting;

            // now check if all players have played their card
            var playersExceptActivePlayer = game.GamePlayers.Where(o => o.PlayerState != PlayerState.IsActivePlayerWaiting);
            if (playersExceptActivePlayer.All(o => o.PlayerState == PlayerState.IsNormalPlayerWaiting))
            {
                // so change the card tsars game state
                var activePlayer = game.GamePlayers.First(o => o.PlayerState == PlayerState.IsActivePlayerWaiting);
                activePlayer.PlayerState = PlayerState.IsActivePlayerSelecting;
                return CardPlayStateEnum.AllPlayed;
            }

            // no state changes, so just return false
            return CardPlayStateEnum.CardPlayed;
        }

        /// <summary>
        /// Selects a white card as the winner of a round
        /// </summary>
        /// <param name="gameGuid">The GUID of the game</param>
        /// <param name="gamePlayerGuid">The GUID of the in game player attempting to play the card</param>
        /// <param name="gameCardGuid">The GUID of the in game card attempting to be played</param>
        /// <returns>The <see cref="GamePlayer"/>.</returns>
        public GamePlayer SelectWhiteCardAsWinner(Guid gameGuid, Guid gamePlayerGuid, Guid gameCardGuid)
        {   
            var game = this.gameRepository.GetByGuid(gameGuid);

            // first check to see the card exists in the game
            var cardInGame = game.WhiteCardsInDeck.FirstOrDefault(o => o.GameCardGuid == gameCardGuid);

            if (cardInGame == null)
            {
                return null;
            }

            // now check card is currently in the game
            var playerOwningCard = game.GamePlayers.FirstOrDefault(o => o.WhiteCardsInHand.Any(c => c.GameCardGuid == gameCardGuid));

            if (playerOwningCard == null)
            {
                return null;
            }

            // and check the active player is not the person who owns the card
            var correctPlayerPlayingCard = playerOwningCard.GamePlayerGuid != gamePlayerGuid;

            if (!correctPlayerPlayingCard)
            {
                return null;
            }

            // otherwise give the player a point
            playerOwningCard.Points++;

            // and mark the cards as having been played
            var playedCardsThisTurn = new List<GameCard>();

            foreach (var gamePlayer in game.GamePlayers)
            {
                playedCardsThisTurn.AddRange(gamePlayer.WhiteCardsInHand.Where(o => o.GameCardState == GameCardState.IsInPlay));
            }

            foreach (var gameCard in playedCardsThisTurn)
            {
                gameCard.GameCardState = GameCardState.HasBeenPlayed;
            }

            // now mark any old cards as inactive
            foreach (var blackCard in game.BlackCardsInDeck.Where(o => o.GameCardState == GameCardState.IsInPlay))
            {
                blackCard.GameCardState = GameCardState.HasBeenPlayed;
            }

            return playerOwningCard;
        }

        /// <summary>
        /// Create the decks at the beginning of the game
        /// </summary>
        /// <param name="game">The active game</param>
        private void CreateDeck(Game game)
        {
            // just get them all for the moment
            var whiteCards = this.cardRepository.GetCardFromDeck(CardType.White);
            var whiteCardsInDeck = this.GenerateCardsInDeck(whiteCards);
            game.WhiteCardsInDeck = whiteCardsInDeck;

            var blackCards = this.cardRepository.GetCardFromDeck(CardType.Black);
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
            var numberOfCardsRequired = game.GamePlayers.Sum(gamePlayerState => CommonConcepts.HAND_SIZE - gamePlayerState.WhiteCardsInHand.Count(o => o.GameCardState == GameCardState.IsInHand));

            // get either the number of required cards, of if there aren't enough - every card that's left
            var gameCardsToDeal = game.WhiteCardsInDeck.Count(o => !o.HasBeenDealt)
                             > numberOfCardsRequired
                                 ? game.WhiteCardsInDeck.Where(o => !o.HasBeenDealt).Take(numberOfCardsRequired).ToList()
                                 : game.WhiteCardsInDeck.Where(o => !o.HasBeenDealt);

            // this whole thing can be made much more efficient, but trying not to prematurely optimise
            foreach (var gameCard in gameCardsToDeal)
            {
                foreach (var gamePlayer in game.GamePlayers)
                {
                    if (gamePlayer.WhiteCardsInHand.Count(o => o.GameCardState == GameCardState.IsAwaitingPlay) < CommonConcepts.HAND_SIZE && !gameCard.HasBeenDealt)
                    {
                        gamePlayer.WhiteCardsInHand.Add(gameCard);
                        gameCard.HasBeenSentToClient = false;
                        gameCard.HasBeenDealt = true;
                    }
                }
            }
        }

        /// <summary>
        /// Generate an in game card type, containing game specific data
        /// </summary>
        /// <param name="cardsInDeck">Collection of cards to use</param>
        /// <returns>A shuffled deck of cards</returns>
        private IList<GameCard> GenerateCardsInDeck(IEnumerable<Card> cardsInDeck)
        {
            var deckCards = cardsInDeck
                .Select(card => new GameCard
                                    {
                                        Card = card, 
                                        GameCardGuid = Guid.NewGuid(), 
                                        GameCardState = GameCardState.IsAwaitingPlay,
                                        HasBeenSentToClient = false
                                    })
                .ToList();

            // shuffling by the card guid should give us some kind of shuffle for the deck.
            // if it's crap then i'll make a better shuffle.
            return deckCards.OrderBy(o => o.GameCardGuid).ToList();
        }

        /// <summary>
        /// Deal a new black card
        /// </summary>
        /// <param name="game">The active game</param> 
        private void DealBlackTurn(Game game)
        {
            // now play the next one
            var gameCard = game.BlackCardsInDeck.FirstOrDefault(o => o.GameCardState == GameCardState.IsAwaitingPlay);

            // we'll run out of cards eventually
            if (gameCard != null)
            {
                gameCard.GameCardState = GameCardState.IsInPlay;
            }
        }
    }
}
