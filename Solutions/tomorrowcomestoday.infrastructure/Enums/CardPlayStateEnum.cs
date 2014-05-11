namespace TomorrowComesToday.Infrastructure.Enums
{
    /// <summary>
    /// The state of the game after a card has been played
    /// </summary>
    public enum CardPlayStateEnum
    {
        /// <summary>
        /// Card was successfully played, but no big change to state
        /// </summary>
        CardPlayed,

        /// <summary>
        /// Card request was invalid
        /// </summary>
        WasNotPlayed,

        /// <summary>
        /// All have been played for round, game is in next state
        /// </summary>
        AllPlayed,
    }
}
