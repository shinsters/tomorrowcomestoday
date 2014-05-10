using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TomorrowComesToday.Infrastructure.Implementations.Services
{
    using TomorrowComesToday.Domain.Entities;
    using TomorrowComesToday.Infrastructure.Interfaces.Services;

    /// <summary>
    /// Singleton service to hold active application states
    /// </summary>
    public class GameLobbyService : IGameLobbyService
    {
        /// <summary>
        /// Constructs a game lobby service
        /// </summary>
        public GameLobbyService()
        {
            this.ConnectedPlayers = new List<ConnectedPlayer>();   
        }

        /// <summary>
        /// The players connected to the server
        /// </summary>
        public IList<ConnectedPlayer> ConnectedPlayers { get; set; }
    }
}
