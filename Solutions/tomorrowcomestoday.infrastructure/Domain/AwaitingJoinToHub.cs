namespace TomorrowComesToday.Infrastructure.Domain
{
    using System;

    /// <summary>
    /// Users awaiting a join to a hub
    /// </summary>
    public class AwaitingJoinToHub
    {
        /// <summary>
        /// The token given to a user to verify their joining
        /// </summary>
        public Guid Token { get; set; }

        /// <summary>
        /// The slug of the channel requested to join
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// When the connection was requested
        /// </summary>
        public DateTime TimeStamp { get; set; }
    }
}
