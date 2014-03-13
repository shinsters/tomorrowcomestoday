namespace TomorrowComesToday.Infrastructure.Implementations.Services
{
    using System.Web;

    using TomorrowComesToday.Domain;
    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;
    using TomorrowComesToday.Infrastructure.Interfaces.Services;

    /// <summary>
    /// Information about the active users
    /// </summary>
    public class WebUserContextService : IUserContextService
    {
        // todo, at the moment we're doing a db call every time we get the user stuff. This should be a singleton
        
        /// <summary>
        /// The User Repository
        /// </summary>
        private readonly IUserRepository userRepository;

        /// <summary>
        /// Initialises a new instance of the <see cref="WebUserContextService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository</param>
        public WebUserContextService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Get the active user logged into the system
        /// </summary>
        /// <returns>User object</returns>^
        public User GetActiveUser()
        {
            var userFormId = HttpContext.Current.User.Identity.Name;
            var user = this.userRepository.GetFromFormId(userFormId);
            return user;
        }

        /// <summary>
        /// Is the user authenticated
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>IF we've a user authenticated or not</returns>
        public bool IsAuthenticated()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }

        /// <summary>
        /// The username of the authenticated user
        /// </summary><returns>The <see cref="string"/></returns>
        public string Username()
        {
            var userFormId = HttpContext.Current.User.Identity.Name;
            var user = this.userRepository.GetFromFormId(userFormId);
            if (user == null || string.IsNullOrEmpty(user.Username))
            {
                return string.Empty;
            }

            return user.Username;
        }

        /// <summary>
        /// Email of authed user
        /// </summary>
        /// <returns></returns>
        public string EmailAddress()
        {
            var userFormId = HttpContext.Current.User.Identity.Name;
            var user = this.userRepository.GetFromFormId(userFormId);
            if (user == null || string.IsNullOrEmpty(user.EmailAddress))
            {
                return string.Empty;
            }

            return user.EmailAddress;
        }
    }
}
