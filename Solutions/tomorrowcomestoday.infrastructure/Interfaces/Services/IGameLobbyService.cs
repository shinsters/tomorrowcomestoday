namespace TomorrowComesToday.Infrastructure.Interfaces.Services
{
    using System.Collections.Generic;

    using TomorrowComesToday.Domain.Entities;

    /// <summary>
    /// Singleton service to hold active application states
    /// </summary>
    public interface IGameLobbyService
    {
        /// <summary>
        /// Collection of players waiting to be assigned game
        /// </summary>
        IList<ConnectedPlayer> ConnectedPlayers { get; set; }

        /// <summary>
        /// Starts a new game
        /// </summary>
        /// <param name="connectedPlayers">A list of connected players</param>
        /// <returns>A started game</returns>
        Game StartGame(IList<ConnectedPlayer> connectedPlayers);

        /// <summary>
        /// Get a collection of connected player objects from a specific game
        /// </summary>
        /// <param name="game">The game</param>
        /// <returns>Connected players</returns>
        IList<ConnectedPlayer> GetPlayersInGame(Game game);
    }
}
