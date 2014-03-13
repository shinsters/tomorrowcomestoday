namespace TomorrowComesToday.Infrastructure.Implementations.Repositories
{
    using System.Collections;
    using System.Collections.Generic;

    using TomorrowComesToday.Domain;
    using TomorrowComesToday.Infrastructure.Enums.Authentication;
    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;

    using SharpArch.NHibernate;

    /// <summary>
    /// The user repository.
    /// </summary>
    public class UserRepository : LinqRepository<User>, IUserRepository
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticationProvider">Which authentication provider has sent this user to us</param>
        /// <param name="token">The remote user id. Sort of like a GUID for that service.</param>
        /// <param name="secretToken">The secretToken, basically a temp password?</param> 
        /// <returns></returns>
        public User GetFromAuthenticationProviderTokenAndSecretToken(AuthenticationProvider authenticationProvider, string token, string secretToken)
        {
            return Session.QueryOver<User>()
                .Where(o => o.Token == token)
                .And(o => o.SecretToken == secretToken)
                .And(o => o.AuthenticationProvider == authenticationProvider)
                .SingleOrDefault();
        }

        /// <summary>
        /// Does this user already exist inside the system?
        /// </summary>
        /// <param name="authenticationProvider"></param>
        /// <param name="token"></param>
        /// <returns>If that user exists or not</returns>
        public bool ProviderAndTokenExists(AuthenticationProvider authenticationProvider, string token)
        {
            return Session.QueryOver<User>()
                    .And(o => o.AuthenticationProvider == authenticationProvider)
                    .And(o => o.Token == token)
                    .RowCount() != 0;
        }

        /// <summary>
        /// Get user from identifier
        /// </summary>
        /// <param name="userFormId">Form Id from h</param>
        /// <returns></returns>
        public User GetFromFormId(string userFormId)
        {
            return Session.QueryOver<User>()
                .Where(o => o.FormsId == userFormId)
                .SingleOrDefault();
        }
    }
}
