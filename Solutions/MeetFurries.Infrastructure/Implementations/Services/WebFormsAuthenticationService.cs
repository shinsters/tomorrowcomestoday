namespace TomorrowComesToday.Infrastructure.Implementations.Services
{
    using System;
    using System.Web.Security;

    using TomorrowComesToday.Infrastructure.Interfaces.Services;

    /// <summary>
    /// This integrates with the built in .net forms auth services
    /// </summary>
    public class WebFormsAuthenticationService : IFormsAuthenticationService
    {
        /// <summary>
        /// The sign in action
        /// </summary>
        /// <param name="userGUID">The userGUID</param>
        /// <param name="createPersistentCookie">Create persistent cookie</param>
        public void SignIn(string userGUID, bool createPersistentCookie)
        {
            if (string.IsNullOrEmpty(userGUID))
            {
                throw new ArgumentException("Value cannot be null or empty.", "userGUID");
            }

            FormsAuthentication.SetAuthCookie(userGUID, createPersistentCookie);
        }

        /// <summary>
        /// The log out action
        /// </summary>
        public void LogOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}
