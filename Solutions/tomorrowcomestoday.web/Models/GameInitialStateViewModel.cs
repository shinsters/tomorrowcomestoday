namespace TomorrowComesToday.Web.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The initial state of a game sent to users when a game starts
    /// </summary>
    public class GameInitialStateViewModel
    {
        /// <summary>
        /// All the player names, including the active player
        /// </summary>
        public IList<GameInitialPlayerViewModel> PlayerNames { get; set; }

        /// <summary>
        /// Guid of the current card tsar
        /// </summary>
        public string ActivePlayerGuid { get; set; }

        /// <summary>
        /// The cards being delt to the player at the beginning of the game
        /// </summary>
        public IList<GameCardDealtViewModel> DealtCards { get; set; }

        /// <summary>
        /// The guid of the player in the current game
        /// </summary>
        public string PlayerInGameGuid { get; set; }

        /// <summary>
        /// The text of the active black card
        /// </summary>
        public string BlackCardText { get; set; }

        /// <summary>
        /// The private token of the player in the game
        /// </summary>
        public string Token { get; set; }
    }
}