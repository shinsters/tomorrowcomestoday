namespace TomorrowComesToday.Domain.Enums
{
     /// <summary>
     /// What is the current state of a player
     /// </summary>
    public enum PlayerState
    {
        /// <summary>
        /// This player is the card tsar and is waiting for everyone to select their cards
        /// </summary>
        IsActivePlayerWaiting,

        /// <summary>
        /// This player is the card tsar and is selecting a card to win
        /// </summary>
        IsActivePlayerSelecting,

        /// <summary>
        /// This player is just a normal player and will play a white card
        /// </summary>
        IsNormalPlayerSelecting,

        /// <summary>
        /// This player is just a normal player who has played a white card and is waiting for the round to complete.
        /// </summary>
        IsNormalPlayerWaiting
    }
}
