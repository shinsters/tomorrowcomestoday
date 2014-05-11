namespace TomorrowComesToday.Web.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// A view model informing clients about played cards and enabling selection
    /// </summary>
    public class GameAllChosenViewModel
    {
        /// <summary>
        /// Can the recieving user select the winning card
        /// </summary>
        public bool CanSelectWinner { get; set; }

        /// <summary>
        /// The answer cards being sent for everyone to see
        /// </summary>
        public IList<GameCardDealtViewModel> AnswerCards { get; set; }
    }
}