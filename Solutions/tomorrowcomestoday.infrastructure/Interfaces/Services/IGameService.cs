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

        /// <summary>
        /// Deal hand to players of a set game
        /// </summary>
        /// <param name="deckSize">The size of the white deck. Null means any size</param>
        /// <param name="gameGuid">The game guid</param>
        void DealGame(int deckSize, Guid gameGuid);
    }
}
