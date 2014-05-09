namespace TomorrowComesToday.Domain.Enums
{

    /// <summary>
    /// What is the current state of the game being played
    /// </summary>
    public enum GameState
    {
        /// <summary>
        /// Game has just begun, so things need setting up
        /// </summary>
        Beginning,

        /// <summary>
        /// The game is active and turns need to take place
        /// </summary>
        BeingPlayed,

        /// <summary>
        /// Game is ended, no further actions
        /// </summary>
        Ended
    }
}
