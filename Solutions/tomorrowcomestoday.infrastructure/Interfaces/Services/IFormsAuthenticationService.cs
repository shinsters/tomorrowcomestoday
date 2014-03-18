namespace TomorrowComesToday.Infrastructure.Interfaces.Services
{
    /// <summary>
    /// This integrates with the built in .net forms auth services
    /// </summary>
    public interface IFormsAuthenticationService
    {
        /// <summary>
        /// The sign in action
        /// </summary>
        /// <param name="userGUID">The userGUID</param>
        /// <param name="createPersistentCookie">Create persistent cookie</param>
        void SignIn(string userGUID, bool createPersistentCookie);

        /// <summary>
        /// The log out action
        /// </summary>
        void LogOut();
    }
}
