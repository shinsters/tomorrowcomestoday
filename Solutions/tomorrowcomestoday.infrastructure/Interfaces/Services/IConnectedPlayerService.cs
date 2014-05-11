namespace TomorrowComesToday.Infrastructure.Interfaces.Services
{
    using TomorrowComesToday.Domain.Entities;

    /// <summary>
    /// This creates user accounts and returns connected players
    /// </summary>
    public interface IConnectedPlayerService
    {
        /// <summary>
        /// Returns a Connected Player object from a connection ID
        /// </summary>
        /// <param name="connectionId">The Connection ID given by signalr</param>
        /// <param name="name">The display name of the player in game</param>
        /// <returns>A new or existing connected player object</returns>
        ConnectedPlayer GetConnectedPlayer(string connectionId, string name);
    }
}
