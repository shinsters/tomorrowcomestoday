namespace TomorrowComesToday.Infrastructure.Implementations.Services
{
    using System;
    using System.Linq;

    using TomorrowComesToday.Domain;
    using TomorrowComesToday.Infrastructure.Enums.Authentication;
    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;
    using TomorrowComesToday.Infrastructure.Interfaces.Services;

    /// <summary>
    /// Implementation of the authentication service
    /// </summary>
    public class WebAuthenticationService : IAuthenticationService
    {
        /// <summary>
        /// The user repository.
        /// </summary>
        private readonly IUserRepository userRepository;

        /// <summary>
        /// Random for username
        /// </summary>
        private Random random;

        /// <summary>
        /// The forms auth service
        /// </summary>
        private readonly IFormsAuthenticationService formsAuthenticationService;

        /// <summary>
        /// Initialises a new instance of the <see cref="WebAuthenticationService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository</param>
        /// <param name="formsAuthenticationService">The forms Authentication Service</param>
        public WebAuthenticationService(IUserRepository userRepository, IFormsAuthenticationService formsAuthenticationService)
        {
            this.userRepository = userRepository;
            this.formsAuthenticationService = formsAuthenticationService;
            random = new Random();
        }

        /// <summary>
        /// The login.
        /// </summary>
        /// <param name="authenticationProvider">The authenticationProvider</param>
        /// <param name="response">The response</param>
        /// <returns>The <see cref="bool"/></returns>
        public AuthenticationState Login(AuthenticationProvider authenticationProvider, string response)
        {
            // the remote identifier
            var token = string.Empty;

            // their 'password' for our internal system
            var secretToken = string.Empty;

            // is the response returned from the service validly formatted?
            var isValidResponse = this.IsValidResponse(authenticationProvider, response);

            if (isValidResponse)
            {
                token = this.GetToken(authenticationProvider, response);
                secretToken = this.GetSecretToken(authenticationProvider, response);
            }

            // fall out if we fail validation
            if (!isValidResponse || string.IsNullOrEmpty(token) || string.IsNullOrEmpty(secretToken))
            {
                return AuthenticationState.Invalid;
            }

            // get the user account
            var user = this.userRepository.GetFromAuthenticationProviderTokenAndSecretToken(authenticationProvider, token, secretToken);

            if (user != null)
            {
                // log them in
                this.formsAuthenticationService.SignIn(user.FormsId, true);
                // if we've got a valid user and they exist within the system
                return AuthenticationState.Valid;
            }

            // check to see if we've a user that exists in the system with this token, 
            // to see if someones trying to get in the account when they shouldn't be
            var userExistsInSystem = this.userRepository.ProviderAndTokenExists(authenticationProvider, token);

            if (userExistsInSystem)
            {
                // todo, looks like someone's being naughty, we should throttle the auth attempts from their IP
                return AuthenticationState.Invalid;
            }

            // otherwise we've a new user on our hands
            var newUser = this.CreateUser(authenticationProvider, token, secretToken);

            this.formsAuthenticationService.SignIn(newUser.FormsId, true);
            return AuthenticationState.HasNotBuiltProfile;
        }

        /// <summary>
        /// Log the user out
        /// </summary>
        public void LogOut()
        {
            this.formsAuthenticationService.LogOut();
        }

        /// <summary>
        /// Add user to the system
        /// </summary>
        /// <param name="authenticationProvider">Authentication provider we're using</param>
        /// <param name="token">Their unique token</param>
        /// <param name="secretToken">Their secret key</param>
        /// <returns>The <see cref="User"/></returns>
        private User CreateUser(AuthenticationProvider authenticationProvider, string token, string secretToken)
        {
            // todo, make the username a random adjective and species
            var user = new User
                           {
                               Username = string.Format("Newbie_{0}",  this.random.Next(1000, 10000)).Trim(),
                               AuthenticationProvider = authenticationProvider,
                               SecretToken = secretToken,
                               Token = token,
                               FormsId = Guid.NewGuid().ToString()
                           };

            this.userRepository.SaveOrUpdate(user);

            return user;
        }

        /// <summary>
        /// Validation to make sure the service's response is as expected
        /// </summary>
        /// <param name="authenticationProvider"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        private bool IsValidResponse(AuthenticationProvider authenticationProvider, string response)
        {
            // todo, holy crap validate this correctly
            switch (authenticationProvider)
            {
                case AuthenticationProvider.Twitter:
                    return response.Contains("oauth");
                default:
                    return false;
            }
        }

        /// <summary>
        /// Get the unique ID token from a response and auth service
        /// </summary>
        /// <param name="authenticationService">The Service</param>
        /// <param name="response">The response from the service</param>
        /// <returns></returns>
        private string GetToken(AuthenticationProvider authenticationService, string response)
        {
            switch (authenticationService)
            {
                case AuthenticationProvider.Twitter:
                    var oauthSplit = response.Split('=')[1];
                    return oauthSplit.Split('&')[0];
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Get the unique ID token from a response and auth service
        /// </summary>
        /// <param name="authenticationService">The Service</param>
        /// <param name="response">The response from the service</param>
        /// <returns></returns>
        private string GetSecretToken(AuthenticationProvider authenticationService, string response)
        {
            switch (authenticationService)
            {
                case AuthenticationProvider.Twitter:
                    return response.Split('=').Last();
                default:
                    return string.Empty;
            }
        }
    }
}
