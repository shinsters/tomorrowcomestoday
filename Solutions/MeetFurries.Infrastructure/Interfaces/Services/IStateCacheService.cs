namespace TomorrowComesToday.Infrastructure.Interfaces.Services
{
    /// <summary>
    /// Caches the state of the current application in memory. Designed to reduce the amount of 
    /// data pulled from storage.
    /// </summary>
    public interface IStateCacheService
    {
        /// <summary>
        /// Build a cache of the items.  This should only happen on the first request.
        /// </summary>
        void BuildCache();
    }
}

