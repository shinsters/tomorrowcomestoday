namespace TomorrowComesToday.Infrastructure.Implementations.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using TomorrowComesToday.Infrastructure.Domain;
    using TomorrowComesToday.Infrastructure.Interfaces.Services;

    /// <summary>
    /// This defines the current state of a meet dashboard, it includes who is on what channel, or
    /// has just connected to what
    /// </summary>
    public class MeetHubStateService : IMeetHubStateService
    {
        /// <summary>
        /// The users waiting to join the hub
        /// </summary>
        private readonly IList<AwaitingJoinToHub> awaitingJoinToHubs;

        /// <summary>
        /// GUID and the slug its joined
        /// </summary>
        private readonly Dictionary<Guid, string> joinedChannels; 

        /// <summary>
        /// Initialises a new instance of the <see cref="MeetHubStateService"/> class.
        /// </summary>
        public MeetHubStateService()
        {
            this.awaitingJoinToHubs = new List<AwaitingJoinToHub>();
            this.joinedChannels = new Dictionary<Guid, string>();
        }

        /// <summary>
        /// Users are added to a waiting hub to store who's connected to what before they're added to a group hub. 
        /// </summary>
        /// <param name="token">Our internal token, which we expect back as verification that they've joined through the controller</param>
        /// <param name="slug">The channel they're joining</param>
        public void AddToWaitingHub(Guid token, string slug)
        {
            // todo, add a means of removing users after so long
            if (!string.IsNullOrEmpty(slug))
            {
                this.awaitingJoinToHubs.Add(new AwaitingJoinToHub
                                           {
                                               Slug = slug,
                                               Token = token,
                                               TimeStamp = DateTime.Now
                                           });
            }

            Debug.WriteLine("Token {0} for slug {1} added to waiting list", token, slug);
        }

        /// <summary>
        /// Once connected, join to a channel
        /// </summary>
        /// <param name="token">
        /// The token we expect passing back
        /// </param>
        /// <returns> The <see cref="KeyValuePair{TKey,TValue}"/> of user id and slug</returns>
        public string AddToChannel(Guid token)
        {
            // get the awaiting connection based on user's one time token
            var awaitingToJoin = this.awaitingJoinToHubs.FirstOrDefault(o => o.Token == token);
            if (awaitingToJoin == null)
            {
                return null;
            }
            
            // remove from waiting list
            this.awaitingJoinToHubs.Remove(awaitingToJoin);
            this.joinedChannels.Add(token, awaitingToJoin.Slug);

            return awaitingToJoin.Slug;
        }

        /// <summary>
        /// What slug a token is associated with
        /// </summary>
        /// <param name="token">The users token</param>
        /// <returns>The channel's slug name</returns>
        public string GetJoinedChannelSlug(Guid token)
        {
            return !this.joinedChannels.ContainsKey(token) ? string.Empty : this.joinedChannels[token];
        }
    }
}
