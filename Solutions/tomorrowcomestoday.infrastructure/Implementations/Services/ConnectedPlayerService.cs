using System;

namespace TomorrowComesToday.Infrastructure.Implementations.Services
{
    using TomorrowComesToday.Domain.Builders;
    using TomorrowComesToday.Domain.Entities;
    using TomorrowComesToday.Domain.Enums;
    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;
    using TomorrowComesToday.Infrastructure.Interfaces.Services;

    /// <summary>
    /// This creates user accounts and returns connected players
    /// </summary>
    public class ConnectedPlayerService : IConnectedPlayerService
    {
        /// <summary>
        /// The player repository
        /// </summary>
        private readonly IPlayerRepository playerRepository;

        public ConnectedPlayerService(IPlayerRepository playerRepository)
        {
            this.playerRepository = playerRepository;
        }

        /// <summary>
        /// Returns a Connected Player object from a connection ID
        /// </summary>
        /// <param name="connectionId">The Connection ID given by signalr</param>
        /// <param name="name">The display name of the player in game</param>
        /// <returns>A new or existing connected player object</returns>
        public ConnectedPlayer GetConnectedPlayer(string connectionId, string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(connectionId))
            {
                return null;
            }

            // first build a player object
            var player = new PlayerBuilder()
                .Named(name)
                .Create();

            this.playerRepository.SaveOrUpdate(player);

            // now create an active connection object for the user
            var connectedPlayer = new ConnectedPlayer
            {
                Player = player,
                ConnectedPlayerState = ConnectedPlayerState.IsWaitingInLobby,
                ConnectionId = connectionId
            };

            return connectedPlayer;
        }
    }
}
