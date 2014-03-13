namespace TomorrowComesToday.Domain
{
    using System;

    /// <summary>
    /// The base implementation of an SMS object.
    /// These are passed to ISmsService implementations which can use them as needed
    /// to create custom objects for interaction with their respective service providers.
    /// </summary>
    public class Sms
    {
        /// <summary>
        /// The administrative user who created the object (recorded in audits.)
        /// </summary>
        public User AdminUser { get; set; }

        /// <summary>
        /// The date and time of creation.
        /// </summary>
        public DateTime CreationDateTime { get; set; }

        /// <summary>
        /// The event with which the SMS is associated.
        /// </summary>
        public Meet AssociatedMeet { get; set; }

        /// <summary>
        /// The number in the "From" field shown on the recipient's device.
        /// It is intended that replies to the message should go to the event creator.
        /// </summary>
        public User FromUser { get; set; }

        /// <summary>
        /// The intended recipient of the message.
        /// The User's PhoneNumber property is used for API calls to service providers.
        /// </summary>
        public User ToUser { get; set; }

        /// <summary>
        /// The body of the message. Whether or not to observe the length limit for a single message is
        /// up to the creator of the message. Not all SMS service providers may support multipart messages.
        /// </summary>
        public string MessageBody { get; set; }

    }
}