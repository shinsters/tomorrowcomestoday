namespace TomorrowComesToday.Domain.Entities
{
    using System;

    using SharpArch.Domain.DomainModel;

    /// <summary>
    /// A game player
    /// </summary>
    public class Player : Entity
    {
        /// <summary>
        /// Display name for the player
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The guid for the player
        /// </summary>
        public Guid Guid { get; set; }
    }
}
