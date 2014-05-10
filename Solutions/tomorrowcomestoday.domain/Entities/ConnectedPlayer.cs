namespace TomorrowComesToday.Domain.Entities
{
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
        /// The session id given by signalr
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        /// The state of the player in the current game
        /// </summary>
        public ConnectedPlayerState ConnectedPlayerState { get; set; }
    }
}
