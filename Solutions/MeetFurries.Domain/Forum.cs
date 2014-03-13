namespace TomorrowComesToday.Domain
{
    using System.Collections.Generic;

    using TomorrowComesToday.Domain.Enums.Forums;

    using SharpArch.Domain.DomainModel;

    /// <summary>
    /// Forum, contains threads
    /// </summary>
    public class Forum : Entity
    {
        public virtual string Name { get; set; } 

        public virtual string Slug { get; set; }

        public virtual IList<User> Moderators { get; set; } 

        public virtual ForumAccessibility Accessibility { get; set; }
    }
}