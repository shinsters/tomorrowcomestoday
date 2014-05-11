namespace TomorrowComesToday.Web.Models
{
    /// <summary>
    /// View model for a chat message sent to the client
    /// </summary>
    public class ChatViewModel
    {
        /// <summary>
        /// Name of user sending message
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Time Stamp when the message was recieved by the server
        /// </summary>
        public string TimeStamp { get; set; }

        /// <summary>
        /// The message sent
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The image for the user
        /// </summary>
        public string Image { get; set; }
    }
}