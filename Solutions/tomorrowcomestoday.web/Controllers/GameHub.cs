namespace TomorrowComesToday.Web.Controllers
{
    using System;

    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;

    using TomorrowComesToday.Domain.Builders;
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

        public GameHub(
            IGameService gameService,
            IGameRepository gameRepository,
            IUserContextService userContextService,
            IPlayerRepository playerRepository)
        {
            this.gameService = gameService;
            this.gameRepository = gameRepository;
            this.userContextService = userContextService;
            this.playerRepository = playerRepository;
        }

        /// <summary>
        /// First called method on joining the application's hub
        /// </summary>
        /// <param name="name"></param>
        public void JoinServer(string name)
        {
            this.SetUserIdInContext(name);
            Clients.All.broadcastMessage(name);
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

            this.playerRepository.SaveOrUpdate(player);
        }

        #endregion
    }
}