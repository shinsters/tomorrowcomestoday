namespace TomorrowComesToday.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;

    using TomorrowComesToday.Domain;
    using TomorrowComesToday.Domain.Entities;
    using TomorrowComesToday.Domain.Enums;
    using TomorrowComesToday.Infrastructure.Enums;
    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;
    using TomorrowComesToday.Infrastructure.Interfaces.Services;
    using TomorrowComesToday.Web.Models;

    /// <summary>
    /// The hub that handles communications to and from clients 
    /// </summary>
    [HubName("gameHub")]
    public class GameHub : Hub
    {
        /// <summary>
        /// The user context service
        /// </summary>
        private readonly IUserContextService userContextService;

        /// <summary>
        /// Contains active application state
        /// </summary>
        private readonly IGameLobbyService gameLobbyService;

        /// <summary>
        /// The connected player service
        /// </summary>
        private readonly IConnectedPlayerService connectedPlayerService;

        /// <summary>
        /// The game service
        /// </summary>
        private readonly IGameService gameService;

        /// <summary>
        /// The game repository
        /// </summary>
        private readonly IGameRepository gameRepository;

        public GameHub(IUserContextService userContextService,
            IGameLobbyService gameLobbyService,
            IConnectedPlayerService connectedPlayerService,
            IGameService gameService,
            IGameRepository gameRepository)
        {
            this.userContextService = userContextService;
            this.gameLobbyService = gameLobbyService;
            this.connectedPlayerService = connectedPlayerService;
            this.gameService = gameService;
            this.gameRepository = gameRepository;
        }

        /// <summary>
        /// First called method on joining the application's hub
        /// Todo: this totally wants how it's called being changed to the correct join hub events, not a manual call back
        /// </summary>
        /// <param name="name"></param>
        public void JoinServer(string name)
        {
            this.SetUserIdInContext(name);
            Clients.All.broadcastMessage(string.Format("{0} has joined the server", name));

            // if we have enough people in the lobby then start a game
            var amountOfWaitingUsers = gameLobbyService.ConnectedPlayers.Count(o => o.ConnectedPlayerState == ConnectedPlayerState.IsWaitingInLobby);
            var hasEnoughPlayersToStartGame = amountOfWaitingUsers == CommonConcepts.GAME_PLAYER_LIMIT;

            if (!hasEnoughPlayersToStartGame)
            {
                return;
            }

            // gets a player to view model dictionary
            var viewModels = this.StartGame();

            foreach (var playerAndModel in viewModels)
            {
                var connectedPlayer = playerAndModel.Key;
                var viewModel = playerAndModel.Value;
                this.Clients.Client(connectedPlayer.ConnectionId).sendInitialState(viewModel);
            }
        }

        /// <summary>
        /// Send message from client
        /// </summary>
        public void SendChatMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            var chatViewModel = this.GenerateChatViewModel(message);
            var currentGame = this.userContextService.CurrentGame;
            var connectedPlayers = this.gameLobbyService.GetPlayersInGame(currentGame);

            foreach (var connectedPlayer in connectedPlayers)
            {
                this.Clients.Client(connectedPlayer.ConnectionId).getChatMessage(chatViewModel);
            }
        }

        /// <summary>
        /// Send a card to play from client
        /// </summary>
        /// <param name="cardGuid"></param>
        public void SendWhiteCard(string cardGuid)
        {
            // first check card is in this game
            var gameCard = this.userContextService.CurrentGame.WhiteCardsInDeck.FirstOrDefault(o => o.GameCardGuid.ToString() == cardGuid);
            if (gameCard == null)
            {
                return;
            }

            // otherwise attempt to play it
            var cardPlayState = gameService.PlayWhiteCard(
                this.userContextService.CurrentGame.GameGuid,
                this.userContextService.ConnectedPlayer.ActiveGamePlayerGuid,
                gameCard.GameCardGuid);

            switch (cardPlayState)
            {
                // move onto next state
                case CardPlayStateEnum.AllPlayed:
                    this.ShowAllCards();
                            break;

                // update all clients, but don't progress
                case CardPlayStateEnum.CardPlayed:
                    this.ShowPlayedCard();
                    break;
            }
        }

        /// <summary>
        /// Call back from the game to set up local context from joined new game
        /// </summary>
        public void AckGame()
        {
            var connectedPlayer = this.userContextService.ConnectedPlayer;

            // just set the game property in the user context of this player
            var game = gameRepository.GetByGuid(connectedPlayer.ActiveGameGuid);
            this.userContextService.CurrentGame = game;
        }

        public void Send(string name, string message)
        {
            Clients.All.broadcastMessage(message);
        }

        #region private methods

        /// <summary>
        /// All cards have been played in a game, let everyone see
        /// </summary>
        private void ShowAllCards()
        {
            // we want to put them in a random order
            var currentGame = this.userContextService.CurrentGame;
            var connectedPlayers = this.gameLobbyService.GetPlayersInGame(currentGame);

            foreach (var connectedPlayer in connectedPlayers)
            {
                var viewModel = this.GenerateAllChosenViewModel(connectedPlayer);

                if (viewModel != null)
                {
                    this.Clients.Client(connectedPlayer.ConnectionId).showAllCards(viewModel);
                }
            }
        }

        /// <summary>
        /// A single card has been played, but no one can see it 
        /// </summary>
        private void ShowPlayedCard()
        {
            var currentGame = this.userContextService.CurrentGame;
            var connectedPlayers = this.gameLobbyService.GetPlayersInGame(currentGame);

            foreach (var connectedPlayer in connectedPlayers)
            {
                this.Clients.Client(connectedPlayer.ConnectionId).showGameCard();
            }
        }

        /// <summary>
        /// Generate an all chosen view model
        /// </summary>
        /// <param name="connectedPlayer">The player</param>
        /// <returns>The view model</returns>
        private GameAllChosenViewModel GenerateAllChosenViewModel(ConnectedPlayer connectedPlayer)
        {
            var activeGame = this.userContextService.CurrentGame;
            var activeGamePlayer = activeGame.GamePlayers.FirstOrDefault(o => o.GamePlayerGuid == connectedPlayer.ActiveGamePlayerGuid);

            if (activeGamePlayer == null)
            {
                return null;
            }

            var playedWhiteCards = new List<GameCard>();
            
            foreach (var gamePlayer in activeGame.GamePlayers)
            {
                playedWhiteCards.AddRange(gamePlayer.WhiteCardsInHand.Where(o => o.GameCardState == GameCardState.IsInPlay));
            }

            var viewModel = new GameAllChosenViewModel
                       {
                           CanSelectWinner = activeGamePlayer.PlayerState == PlayerState.IsActivePlayerSelecting,
                           AnswerCards = playedWhiteCards.Select(GenerateInitialCardDealtViewModel).ToList()
                       };

            return viewModel;
        }

        /// <summary>
        /// Generates a player to chat view model to send to players
        /// </summary>
        /// <param name="message"></param>
        /// <returns>A chat view model</returns>
        private ChatViewModel GenerateChatViewModel(string message)
        {
            return new ChatViewModel
                       {
                           UserName = userContextService.ConnectedPlayer.Player.Name,
                           Image = "http://lorempixel.com/50/50/",
                           Message = message,
                           TimeStamp = DateTime.Now.ToShortTimeString()
                       };
        }

        /// <summary>
        /// Set the Id of the user in the user context service
        /// </summary>
        private void SetUserIdInContext(string playerName)
        {
            // create a connected player with their signalr connection id
            var connectionId = Context.ConnectionId;

            var connectedPlayer = this.connectedPlayerService.GetConnectedPlayer(connectionId, playerName);
            if (connectedPlayer == null)
            {
                return;
            }

            // now set the connected player in the local context
            this.userContextService.ConnectedPlayer = connectedPlayer;

            // add the user to the lobby
            gameLobbyService.ConnectedPlayers.Add(connectedPlayer);
        }

        /// <summary>
        /// Start a game 
        /// </summary>
        private Dictionary<ConnectedPlayer, GameInitialStateViewModel> StartGame()
        {
            // grab the first n players who're ready to join the game
            var connectedPlayers = this.gameLobbyService.ConnectedPlayers
                .Where(o => o.ConnectedPlayerState == ConnectedPlayerState.IsWaitingInLobby)
                .Take(CommonConcepts.GAME_PLAYER_LIMIT).ToList();

            if (connectedPlayers.Count() < 2)
            {
                return new Dictionary<ConnectedPlayer, GameInitialStateViewModel>();
            }

            var game = this.gameLobbyService.StartGame(connectedPlayers);

            // generate view model to send back to users
            var modelsToSendToClients = new Dictionary<ConnectedPlayer, GameInitialStateViewModel>();

            foreach (var connectedPlayer in connectedPlayers)
            {
                var playerInGame = game.GamePlayers.First(o => o.Player.Guid == connectedPlayer.Player.Guid);

                var model = new GameInitialStateViewModel
                                {
                                    DealtCards = playerInGame.WhiteCardsInHand.Select(this.GenerateInitialCardDealtViewModel).ToList(),
                                    ActivePlayerGuid = game.GamePlayers.First(o => o.PlayerState == PlayerState.IsActivePlayerWaiting).GamePlayerGuid.ToString(),
                                    PlayerInGameGuid = playerInGame.GamePlayerGuid.ToString(),
                                    PlayerNames = game.GamePlayers.Select(this.GameInitialPlayerViewModel).ToList(),
                                    BlackCardText = game.BlackCardsInDeck.First(o => o.GameCardState == GameCardState.IsInPlay).Card.Text
                                };
                modelsToSendToClients.Add(connectedPlayer, model);
                connectedPlayer.ActiveGameGuid = game.GameGuid;
                connectedPlayer.ActiveGamePlayerGuid = playerInGame.GamePlayerGuid;
                connectedPlayer.ConnectedPlayerState = ConnectedPlayerState.IsPlayingGame;
            }

            return modelsToSendToClients;
        }

        /// <summary>
        /// Generate a initial player view model
        /// </summary>
        /// <param name="gamePlayer"></param>
        /// <returns></returns>
        private GameInitialPlayerViewModel GameInitialPlayerViewModel(GamePlayer gamePlayer)
        {
           return new GameInitialPlayerViewModel
                      {
                          Guid = gamePlayer.GamePlayerGuid.ToString(),
                          Name = gamePlayer.Player.Name
                      };
        }

        /// <summary>
        /// Generate an initial card dealt view model 
        /// </summary>
        /// <param name="gameCard"></param>
        /// <returns></returns>
        private GameCardDealtViewModel GenerateInitialCardDealtViewModel(GameCard gameCard)
        {
            return new GameCardDealtViewModel
                                                    {
                                                        Guid = gameCard.GameCardGuid.ToString(),
                                                        Text = gameCard.Card.Text
                                                    };
        }
        #endregion
    }
}