namespace TomorrowComesToday.Web.Models
{
    /// <summary>
    /// View model of a card being handed to a player
    /// </summary>
    public class GameInitialCardDealtViewModel
    {
        /// <summary>
        /// The text of the card
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The game specific guid of a card
        /// </summary>
        public string Guid { get; set; }
    }
}