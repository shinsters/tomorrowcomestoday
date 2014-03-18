namespace TomorrowComesToday.Infrastructure.Interfaces.Services
{
    using TomorrowComesToday.Infrastructure.Enums.Authentication;

    /// <summary>
    /// The WebAuthenticationService interface.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Return user based on login credentials. Might be a new user, might be an existing one
        /// </summary>
        /// <param name="authenticationProvider">Provider of the account</param>
        /// <param name="response">Response from the service, could be anything - it's provider specific. It needs manually parsing.</param>
        /// <returns>User, or null if error</returns>
        AuthenticationState Login(AuthenticationProvider authenticationProvider, string response);

        /// <summary>
        /// Log the active user out
        /// </summary>
        void LogOut();
    }
}
