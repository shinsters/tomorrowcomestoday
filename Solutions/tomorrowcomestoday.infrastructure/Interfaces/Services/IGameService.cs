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
        /// <param name="deckSize">The size of the white deck. Null means any size</param>
        /// <param name="gameGuid">The game guid</param>
        void DealRound(int deckSize, Guid gameGuid);
    }
}
