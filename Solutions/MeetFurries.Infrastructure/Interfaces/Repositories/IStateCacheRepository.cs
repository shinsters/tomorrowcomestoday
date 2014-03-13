namespace TomorrowComesToday.Infrastructure.Interfaces.Repositories
{
    using System.Collections.Generic;

    using TomorrowComesToday.Domain;

    /// <summary>
    /// This doesn't talk to the database, but instead is a singleton of CachedTypes. 
    /// THIS NEEDS TO BE PER APPLICATION, NOT PER REQUEST
    /// THIS NEED TO BE RATE LIMITED, MAYBE?
    /// </summary>
    public interface IStateCacheRepository
    {
        /// <summary>
        /// Really only for testing. 
        /// </summary>
        /// <returns>Every cache item</returns>
        IList<StateCacheItem> GetAll();

        /// <summary>
        /// Method called through the web socket
        /// </summary>
        /// <param name="start">Beginning string to search title fields with</param>
        /// <returns>Collection of relevant cache items</returns>
        IList<StateCacheItem> GetBeginningWith(string start);

        /// <summary>
        /// Save collection, does no checking to see if exists.  This should only ever run if we've an empty collection.
        /// </summary>
        void SaveCollection(IList<StateCacheItem> cacheItems);

        /// <summary>
        /// Save a single cache item
        /// </summary>
        /// <param name="cacheItem"></param>
        void SaveOrUpdateItem(StateCacheItem cacheItem);
    }
}
