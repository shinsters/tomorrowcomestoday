namespace TomorrowComesToday.Web.Controllers
{
    using System;
    using System.Linq;

    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;

    using TomorrowComesToday.Domain;
    using TomorrowComesToday.Domain.Builders;
    using TomorrowComesToday.Domain.Entities;
    using TomorrowComesToday.Domain.Enums;
    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;
    using TomorrowComesToday.Infrastructure.Interfaces.Services;

    /// <summary>
    /// The hub that handles communications to and from clients 
    /// </summary>
    [HubName("gameHub")]
    public class GameHub : Hub
    {
        /// <summary>
        /// Performs actions in games
        /// </summary>
        private readonly IGameService gameService;

        /// <summary>
        /// Gets games
        /// </summary>
        private readonly IGameRepository gameRepository;

        /// <summary>
        /// The user context service
        /// </summary>
        private readonly IUserContextService userContextService;

        /// <summary>
        /// The player repository
        /// </summary>
        private readonly IPlayerRepository playerRepository;

        /// <summary>
        /// Contains active application state
        /// </summary>
        private readonly IGameLobbyService gameLobbyService;

        public GameHub(
            IGameService gameService,
            IGameRepository gameRepository,
            IUserContextService userContextService,
            IPlayerRepository playerRepository,
            IGameLobbyService gameLobbyService)
        {
            this.gameService = gameService;
            this.gameRepository = gameRepository;
            this.userContextService = userContextService;
            this.playerRepository = playerRepository;
            this.gameLobbyService = gameLobbyService;
        }

        /// <summary>
        /// First called method on joining the application's hub
        /// </summary>
        /// <param name="name"></param>
        public void JoinServer(string name)
        {
            this.SetUserIdInContext(name);
            Clients.All.broadcastMessage(string.Format("{0} has joined the server", name));

            // if we have enough people in the lobby then start a game
            var amountOfWaitingUsers = gameLobbyService.ConnectedPlayers.Count(o => o.ConnectedPlayerState == ConnectedPlayerState.IsWaitingInLobby);
            var hasEnoughPlayersToStartGame = amountOfWaitingUsers == CommonConcepts.GAME_PLAYER_LIMIT;

            if (hasEnoughPlayersToStartGame)
            {
                this.StartGame();
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
            var currentActiveSessionId = Context.ConnectionId;
            this.userContextService.CurrentActiveSessionId = currentActiveSessionId;

            // just for now, make a new player
            var existingPlayer = playerRepository.GetByName(playerName);
            if (existingPlayer != null)
            {
                throw new Exception("Player handle already taken");
            }

            var player = new PlayerBuilder()
                .Named(playerName)
                .Create();

            // get signalr connection guid

            this.playerRepository.SaveOrUpdate(player);
            userContextService.Player = player;

            var connectedPlayer = new ConnectedPlayer
                                      {
                                          Player = player,
                                          ConnectedPlayerState = ConnectedPlayerState.IsWaitingInLobby,
                                          SessionId = currentActiveSessionId
                                      };

            gameLobbyService.ConnectedPlayers.Add(connectedPlayer);
        }

        /// <summary>
        /// Start a game 
        /// </summary>
        private void StartGame()
        {
            //foreach (var player in gameLobbyService)
            //{
            //    var 
            //}
        }

        #endregion
    }
}