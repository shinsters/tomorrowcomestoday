namespace TomorrowComesToday.Infrastructure.Interfaces.Services
{
    using TomorrowComesToday.Domain.Entities;

    /// <summary>
    /// Contains per session based properties 
    /// </summary>
    public interface IUserContextService
    {
        /// <summary>
        /// Active id of user in the context of the active application
        /// </summary>
        ConnectedPlayer ConnectedPlayer { get; set; }

        /// <summary>
        /// The game the player is in
        /// </summary>
        Game CurrentGame { get; set; }
    }
}
