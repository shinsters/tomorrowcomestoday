namespace TomorrowComesToday.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;

    using TomorrowComesToday.Domain;
    using TomorrowComesToday.Domain.Entities;
    using TomorrowComesToday.Domain.Enums;
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

        public GameHub(IUserContextService userContextService,
            IGameLobbyService gameLobbyService,
            IConnectedPlayerService connectedPlayerService)
        {
            this.userContextService = userContextService;
            this.gameLobbyService = gameLobbyService;
            this.connectedPlayerService = connectedPlayerService;
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



        public void Send(string name, string message)
        {
            Clients.All.broadcastMessage(message);
        }

        #region private methods

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
            this.userContextService.CurrentActiveSessionId = connectionId;
            this.userContextService.Player = connectedPlayer.Player;

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
                                    IsActivePlayer = playerInGame.PlayerState == PlayerState.IsActivePlayerWaiting,
                                    PlayerInGameGuid = playerInGame.GamePlayerGuid.ToString(),
                                    PlayerNames = game.GamePlayers.Select(this.GameInitialPlayerViewModel).ToList(),
                                    BlackCardText = game.BlackCardsInDeck.First(o => o.GameCardState == GameCardState.IsInPlay).Card.Text
                                };
                modelsToSendToClients.Add(connectedPlayer, model);

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
        private GameInitialCardDealtViewModel GenerateInitialCardDealtViewModel(GameCard gameCard)
        {
            return new GameInitialCardDealtViewModel
                                                    {
                                                        Guid = gameCard.GameCardGuid.ToString(),
                                                        Text = gameCard.Card.Text
                                                    };
        }
        #endregion
    }
}