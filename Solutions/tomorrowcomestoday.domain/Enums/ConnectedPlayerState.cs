namespace TomorrowComesToday.Domain.Enums
{
    /// <summary>
    /// The state of a connected player
    /// </summary>
    public enum ConnectedPlayerState
    {
        /// <summary>
        /// The player is waiting in the lobby to join a new game
        /// </summary>
        IsWaitingInLobby,

        /// <summary>
        /// The player is in a game
        /// </summary>
        IsPlayingGame
    }
}
