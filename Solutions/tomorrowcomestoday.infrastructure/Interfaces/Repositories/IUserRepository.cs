namespace TomorrowComesToday.Infrastructure.Interfaces.Repositories
{
    using TomorrowComesToday.Domain;
    using TomorrowComesToday.Infrastructure.Enums.Authentication;

    using SharpArch.Domain.PersistenceSupport;

    /// <summary>
    /// The UserRepository.
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticationProvider">Which authenticationProvider has sent this user to us</param>
        /// <param name="token">The remote user id. Sort of like a GUID for that service.</param>
        /// <param name="secretToken">The secretToken, basically a temp password?</param>
        /// <returns></returns>
        User GetFromAuthenticationProviderTokenAndSecretToken(AuthenticationProvider authenticationProvider, string token, string secretToken);

        /// <summary>
        /// Does this user already exist inside the system?
        /// </summary>
        /// <param name="authenticationProvider"></param>
        /// <param name="token"></param>
        /// <returns>If that user exists or not</returns>
        bool ProviderAndTokenExists(AuthenticationProvider authenticationProvider, string token);

        /// <summary>
        /// Get user from identifier
        /// </summary>
        /// <param name="userFormId">Form Id from h</param>
        /// <returns></returns>
        User GetFromFormId(string userFormId);
    }
}
