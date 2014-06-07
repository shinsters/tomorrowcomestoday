namespace TomorrowComesToday.Infrastructure.Implementations.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using TomorrowComesToday.Domain.Builders;
    using TomorrowComesToday.Domain.Entities;
    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;
    using TomorrowComesToday.Infrastructure.Interfaces.Services;

    /// <summary>
    /// Singleton service to hold active application states
    /// </summary>
    public class GameLobbyService : IGameLobbyService
    {
        /// <summary>
        /// The game repository
        /// </summary>
        private readonly IGameRepository gameRepository;

        /// <summary>
        /// The game service
        /// </summary>
        private readonly IGameService gameService;

        /// <summary>
        /// Constructs a game lobby service
        /// </summary>
        /// <param name="gameRepository">The game Repository.</param>
        /// <param name="gameService">The game Service.</param>
        public GameLobbyService(
            IGameRepository gameRepository,
            IGameService gameService)
        {
            this.gameRepository = gameRepository;
            this.gameService = gameService;
            this.ConnectedPlayers = new List<ConnectedPlayer>();
        }

        /// <summary>
        /// The players connected to the server
        /// </summary>
        public IList<ConnectedPlayer> ConnectedPlayers { get; set; }

        /// <summary>
        /// Starts a new game
        /// </summary>
        /// <param name="connectedPlayers">A list of connected players</param>
        /// <returns>A started game</returns>
        public Game StartGame(IList<ConnectedPlayer> connectedPlayers)
        {
            // create the game
            var game = new GameBuilder()
                .AddPlayers(connectedPlayers.Select(o => o.Player)
                .ToList())
                .Create();

            this.gameRepository.SaveOrUpdate(game);

            // start the first turn
            this.gameService.DealRound(game.GameGuid);

            // send the game back to the hub to make a view model
            return game;
        }

        /// <summary>
        /// Get a collection of connected player objects from a specific game
        /// </summary>
        /// <param name="game">The game</param>
        /// <returns>Connected players</returns>
        public IList<ConnectedPlayer> GetPlayersInGame(Game game)
        {
            // I guess this is more like a repository method, but I prefer to not have repos exposed to controllers...
            return this.ConnectedPlayers.Where(o => o.ActiveGameGuid == game.GameGuid).ToList();
        }
    }
}
