namespace TomorrowComesToday.Web.Models
{
    /// <summary>
    /// View model of a card being handed to a player
    /// </summary>
    public class GameCardDealtViewModel
    {
        /// <summary>
        /// The text of the card
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The game specific GUID of a card
        /// </summary>
        public string Guid { get; set; }
    }
}