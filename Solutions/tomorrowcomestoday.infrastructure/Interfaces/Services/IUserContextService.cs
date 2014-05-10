namespace TomorrowComesToday.Infrastructure.Interfaces.Services
{
    using TomorrowComesToday.Domain.Entities;

    /// <summary>
    /// Contains per session based properties 
    /// </summary>
    public interface IUserContextService
    {
        /// <summary>
        /// Active player for this context
        /// </summary>
        Player Player { get; set; }

        /// <summary>
        /// Active id of user in the context of the active application
        /// </summary>
        string CurrentActiveSessionId { get; set; }
    }
}
