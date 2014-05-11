namespace TomorrowComesToday.Infrastructure.Implementations.Services
{
    using TomorrowComesToday.Domain.Entities;
    using TomorrowComesToday.Infrastructure.Interfaces.Services;

    /// <summary>
    /// Contains per session based properties 
    /// </summary>
    public class UserContextService : IUserContextService
    {
        /// <summary>
        /// Active id of user in the context of the active application
        /// </summary>
        public ConnectedPlayer ConnectedPlayer { get; set; }

        /// <summary>
        /// The game the player is in
        /// </summary>
        public Game CurrentGame { get; set; }
    }
}