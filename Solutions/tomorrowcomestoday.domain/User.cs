namespace TomorrowComesToday.Domain
{
    using System.Collections;
    using System.Collections.Generic;

    using TomorrowComesToday.Infrastructure.Enums.Authentication;

    using SharpArch.Domain.DomainModel;

    /// <summary>
    /// User of the system
    /// </summary>
    public class User : Entity
    {
        /// <summary>
        /// Local user name for user.
        /// </summary>
        public virtual string Username { get; set; }

        /// <summary>
        /// The user's mobile telephone number.
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string EmailAddress { get; set; }

         /// <summary>
         /// Which 'website' or provider has given us this user
         /// </summary>
        public virtual AuthenticationProvider AuthenticationProvider { get; set; }

        /// <summary>
        /// Unique ID for remote authentication provider
        /// </summary>
        public virtual string Token { get; set; }

        /// <summary>
        /// Remote 'password' from service to our website
        /// </summary>
        public virtual string SecretToken { get; set; } 

        /// <summary>
        /// This is the unique id for the user for sessions
        /// </summary>
        public virtual string FormsId { get; set; }

        /// <summary>
        /// Events organised by this user
        /// </summary>
        public virtual IList<Meet> OrganisedEvents { get; set; }

        /// <summary>
        /// Events this user is attending
        /// </summary>
        public virtual IList<Meet> AttendingEvents { get; set; }

        /// <summary>
        /// Events this user is maybe attending
        /// </summary>
        public virtual IList<Meet> MaybeAttendingEvents { get; set; }

        /// <summary>
        /// Comments the user has made
        /// </summary>
        public virtual IList<Comment> Comments { get; set; }
    }
}