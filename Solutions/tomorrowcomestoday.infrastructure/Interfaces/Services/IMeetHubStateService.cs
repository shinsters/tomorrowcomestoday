namespace TomorrowComesToday.Infrastructure.Interfaces.Services
{
    using System;

    /// <summary>
    /// This defines the current state of a meet dashboard, it includes who is on what channel, or
    /// has just connected to what
    /// </summary>
    public interface IMeetHubStateService
    {
        /// <summary>
        /// Users are added to a waiting hub to store who's connected to what before they're added to a group hub. 
        /// </summary>
        /// <param name="token">Our internal token, which we expect back as verification that they've joined through the controller</param>
        /// <param name="slug">The channel they're joining</param>
        void AddToWaitingHub(Guid token, string slug);

        /// <summary>
        /// Once connected, join to a channel
        /// </summary>
        /// <param name="token">
        /// The token we expect passing back
        /// </param>
        /// <returns>The slug to join</returns>
        string AddToChannel(Guid token);

        /// <summary>
        /// What slug a token is associated with
        /// </summary>
        /// <param name="token">The users token</param>
        /// <returns>The channel's slug name</returns>
        string GetJoinedChannelSlug(Guid token);
    }
}
