namespace TomorrowComesToday.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;

    using NHibernate.Hql.Ast.ANTLR;

    using TomorrowComesToday.Domain;
    using TomorrowComesToday.Domain.Builders;
    using TomorrowComesToday.Domain.Entities;
    using TomorrowComesToday.Domain.Enums;
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
        private IList<GameInitialStateViewModel> StartGame()
        {
            // grab the first n players who're ready to join the game
            var players =
                this.gameLobbyService.ConnectedPlayers.Where(
                    o => o.ConnectedPlayerState == ConnectedPlayerState.IsWaitingInLobby)
                    .Take(CommonConcepts.GAME_PLAYER_LIMIT).ToList();

            if (players.Count() < 2)
            {
                return new List<GameInitialStateViewModel>();
            }

            var game = new GameBuilder()
                .AddPlayers(players.Select(o => o.Player)
                .ToList())
                .Create();


            gameRepository.SaveOrUpdate(game);

            gameService.DealRound(game.GameGuid);

            // generate view model to send back to users

            var modelsToSendToClients = new List<GameInitialStateViewModel>();

            foreach (var connectedPlayer in players)
            {
                var playerInGame = game.GamePlayers.First(o => o.Player.Guid == connectedPlayer.Player.Guid);

                var model = new GameInitialStateViewModel
                                {
                                    DealtCards = playerInGame.WhiteCardsInHand.Select(this.GenerateInitialCardDealtViewModel).ToList(),
                                    IsActivePlayer = playerInGame.PlayerState == PlayerState.IsActivePlayerWaiting,
                                    PlayerInGameGuid = playerInGame.GamePlayerGuid.ToString(),
                                    PlayerNames = game.GamePlayers.Select(this.GameInitialPlayerViewModel).ToList()
                                };

                modelsToSendToClients.Add(model);
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
                                                        Guid =
                                                            gameCard.GameCardGuid.ToString
                                                            (),
                                                        Text = gameCard.Card.Text
                                                    };
        }
        #endregion
    }
}