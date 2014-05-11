namespace TomorrowComesToday.Domain.Entities
{
    using System;

    using TomorrowComesToday.Domain.Enums;

    /// <summary>
    /// A player in the context of the live app, connected and active
    /// </summary>
    public class ConnectedPlayer
    {
        /// <summary>
        /// The player entity
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        /// The connection id given by signalr
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        /// The state of the player in the current game
        /// </summary>
        public ConnectedPlayerState ConnectedPlayerState { get; set; }

        /// <summary>
        /// Guid of the game this player is currently in
        /// </summary>
        public Guid ActiveGameGuid { get; set; }

        /// <summary>
        /// The Guid of the game player of this player in their current game
        /// </summary>
        public Guid ActiveGamePlayerGuid { get; set; }
    }
}
