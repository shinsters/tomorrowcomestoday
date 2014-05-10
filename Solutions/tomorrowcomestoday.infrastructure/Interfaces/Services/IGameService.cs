namespace TomorrowComesToday.Infrastructure.Interfaces.Services
{
    using System;

    using NHibernate.Id;

    /// <summary>
    /// This handles changes in the game state
    /// </summary>
    public interface IGameService
    {
        /// <summary>
        /// Deal a round
        /// </summary>
        /// <param name="gameGuid">The game GUID</param>
        void DealRound(Guid gameGuid);

        /// <summary>
        /// Plays a white card from a player
        /// </summary>
        /// <param name="gameGuid">The GUID of the game</param>
        /// <param name="gamePlayerGuid">The GUID of the in game player attempting to play the card</param>
        /// <param name="gameCardGuid">The GUID of the in game card attempting to be played</param>
        void PlayWhiteCard(Guid gameGuid, Guid gamePlayerGuid, Guid gameCardGuid);

        /// <summary>
        /// Selects a white card as the winner of a round
        /// </summary>
        /// <param name="gameGuid">The GUID of the game</param>
        /// <param name="gamePlayerGuid">The GUID of the in game player attempting to play the card</param>
        /// <param name="gameCardGuid">The GUID of the in game card attempting to be played</param>
        void SelectWhiteCardAsWinner(Guid gameGuid, Guid gamePlayerGuid, Guid gameCardGuid);
    }
}
