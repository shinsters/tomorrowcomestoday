namespace TomorrowComesToday.Domain
{
    using System;
    using System.Collections.Generic;

    using SharpArch.Domain.DomainModel;

    public class Meet : Entity
    {
        public virtual DateTime StartTime { get; set; }

        public virtual DateTime? EndTime { get; set; }

        /// <summary>
        /// Unique identifier for clean URLs
        /// </summary>
        public virtual string Slug { get; set; }

        public virtual string Description { get; set; }

        public virtual User Administrator { get; set; }

        public virtual string Name { get; set; }

        public virtual IList<User> Attendees { get; set; } 

        public virtual IList<User> MaybeAttendees { get; set; } 

        public virtual IList<User> NotAttending { get; set; } 

        public virtual IList<Location> Locations { get; set; } 

        public virtual IList<Comment> Comments { get; set; }
    } 
}
