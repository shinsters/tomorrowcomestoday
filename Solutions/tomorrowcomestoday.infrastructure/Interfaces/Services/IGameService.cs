namespace TomorrowComesToday.Infrastructure.Interfaces.Services
{
    using System;

    /// <summary>
    /// This handles changes in the game state
    /// </summary>
    public interface IGameService
    {
        /// <summary>
        /// Deal a round
        /// </summary>
        /// <param name="gameGuid">The game guid</param>
        void DealRound(Guid gameGuid);
    }
}
