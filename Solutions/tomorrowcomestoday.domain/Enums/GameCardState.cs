namespace TomorrowComesToday.Domain.Enums
{
    /// <summary>
    /// What is the current state of this game card
    /// </summary>
    public enum GameCardState
    {
        /// <summary>
        /// Card is okay to be played
        /// </summary>
        IsAwaitingPlay,

        /// <summary>
        /// The card is currently in the hand of a player
        /// </summary>
        IsInHand,

        /// <summary>
        /// The card the player has just played in the active round
        /// </summary>
        IsInPlay,

        /// <summary>
        /// Card has been played and should be ignored from the players hand
        /// </summary>
        HasBeenPlayed
    }
}
