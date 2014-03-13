namespace TomorrowComesToday.Infrastructure.Implementations.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using TomorrowComesToday.Domain;
    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;

    /// <summary>
    /// This doesn't talk to the database, but instead is a singleton of CachedTypes. 
    /// THIS NEEDS TO BE PER APPLICATION, NOT PER REQUEST
    /// THIS NEED TO BE RATE LIMITED, MAYBE?
    /// </summary>
    public class StateCacheRepository : IStateCacheRepository
    {
        /// <summary>
        /// Just initalise the collection. The service should populate this.
        /// </summary>
        public StateCacheRepository()
        {
            this.CacheItems = new List<StateCacheItem>();
        }

        /// <summary>
        /// The collection of cached state items
        /// </summary>
        private IList<StateCacheItem> CacheItems { get; set; }

        /// <summary>
        /// Really only for testing. 
        /// </summary>
        /// <returns>Every cache item</returns>
        public IList<StateCacheItem> GetAll()
        {
            return this.CacheItems;
        }

        /// <summary>
        /// Method called through the web socket
        /// </summary>
        /// <param name="start">Beginning string to search title fields with</param>
        /// <returns>Collection of relevant cache items</returns>
        public IList<StateCacheItem> GetBeginningWith(string start)
        {
            return this.CacheItems.Where(o => o.Title.Contains(start)).ToList();
        }

        /// <summary>
        /// Save collection, does no checking to see if exists.  This should only ever run if we've an empty collection.
        /// </summary>
        public void SaveCollection(IList<StateCacheItem> cacheItems)
        {
            if (cacheItems.Any())
            {
                throw new Exception("Hi. Something went wrong. If you see this, contact support with the message 'ermagerdcachefail'. We'll know what it means. 10 poins to Griffindor if you can work it out too.");
            }

            this.CacheItems = cacheItems;
        }

        /// <summary>
        /// Save a single cache item
        /// </summary>
        /// <param name="cacheItem"></param>
        public void SaveOrUpdateItem(StateCacheItem cacheItem)
        {
            // we probably do need the id in the state cache item
        }
    }
}
