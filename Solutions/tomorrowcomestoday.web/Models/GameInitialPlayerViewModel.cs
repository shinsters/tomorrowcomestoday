namespace TomorrowComesToday.Web.Models
{
    /// <summary>
    /// A player who is in a game and their properties
    /// </summary>
    public class GameInitialPlayerViewModel
    {
        /// <summary>
        /// The dispay name of the player
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Guid of the player in this game
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// The points of that player
        /// </summary>
        public int Points { get; set; }
    }
}