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
        /// Active player for this context
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        /// Active id of user in the context of the active application
        /// </summary>
        public string CurrentActiveSessionId { get; set; }
    }
}