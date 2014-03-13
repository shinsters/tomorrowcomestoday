namespace TomorrowComesToday.Infrastructure.Implementations.Services
{
    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;
    using TomorrowComesToday.Infrastructure.Interfaces.Services;

    /// <summary>
    /// Caches the state of the current application in memory. Designed to reduce the amount of 
    /// data pulled from storage.
    /// </summary>
    public class StateCacheService : IStateCacheService
    {
        /// <summary>
        /// User repository for generating cache of user profiles
        /// </summary>
        private readonly IUserRepository userRepository;

        /// <summary>
        /// Meet repository, for generating cache of events, historical events and cities 
        /// </summary>
        private readonly IMeetRepository meetRepository;

        /// <summary>
        /// The state cache repository.
        /// </summary>
        private readonly IStateCacheRepository stateCacheRepository;

        /// <summary>
        /// Constructs the repository.
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="meetRepository"></param>
        /// <param name="stateCacheRepository"></param>
        public StateCacheService(IUserRepository userRepository, IMeetRepository meetRepository, IStateCacheRepository stateCacheRepository)
        {
            this.userRepository = userRepository;
            this.meetRepository = meetRepository;
            this.stateCacheRepository = stateCacheRepository;
        }

        /// <summary>
        /// Build a cache of the items.  This should only happen on the first request.
        /// </summary>
        public void BuildCache()
        {
            
        }
    }
}
