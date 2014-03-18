namespace TomorrowComesToday.Infrastructure.Enums.Authentication
{
    /// <summary>
    /// The authentication state.
    /// </summary>
    public enum AuthenticationState
    {
        /// <summary>
        /// User credentials match an account, but are wrong
        /// </summary>
        Invalid,

        /// <summary>
        /// User credentials match an account, are valid, and the profile is complete
        /// </summary>
        Valid,

        /// <summary>
        /// New user, no profile information
        /// </summary>
        HasNotBuiltProfile
    }
}
