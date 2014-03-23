namespace TomorrowComesToday.Infrastructure.Interfaces.Services
{
    using System;

    /// <summary>
    /// This handles changes in the game state
    /// </summary>
    public interface IGameService
    {
        /// <summary>
        /// Deal hand to players of a set game
        /// </summary>
        /// <param name="gameGuid">The game guid</param>
        void DealGame(Guid gameGuid);
    }
}
