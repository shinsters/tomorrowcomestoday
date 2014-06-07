namespace TomorrowComesToday.Web.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Updates a client with their new state at the beginning of a turn
    /// </summary>
    public class GameNextRoundStateViewModel
    {
        /// <summary>
        /// The new cards for the player
        /// </summary>
        public IList<GameCardDealtViewModel> WhiteCards { get; set; }

        /// <summary>
        /// The GUID of the current card tsar
        /// </summary>
        public string ActivePlayerGuid { get; set; }

        /// <summary>
        /// The text of the active card in play
        /// </summary>
        public string BlackCardText { get; set; }
    }
}