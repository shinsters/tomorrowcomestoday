namespace TomorrowComesToday.Infrastructure.Interfaces.Services
{

    using TomorrowComesToday.Domain;

    /// <summary>
    /// Information about the active users
    /// </summary>
    public interface IUserContextService
    {
        /// <summary>
        /// Get the active user logged into the system
        /// </summary>
        /// <returns>User object</returns>
        User GetActiveUser();

        /// <summary>
        /// Is the user authenticated
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        bool IsAuthenticated();

        /// <summary>
        /// The username of the authenticated user
        /// </summary><returns>The <see cref="string"/></returns>
        string Username();

        /// <summary>
        /// Email of authenticated user
        /// </summary>
        /// <returns></returns>
        string EmailAddress();
    }
}
