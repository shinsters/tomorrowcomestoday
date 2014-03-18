namespace TomorrowComesToday.Domain
{
    using TomorrowComesToday.Domain.Enums.StateCache;

    /// <summary>
    /// Cached session items.  This isn't persisted.
    /// </summary>
    public class StateCacheItem
    {
        /// <summary>
        /// Id of the item, this might not be needed?
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Slug for the address to the view page of the item
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// Type that this item is
        /// </summary>
        public CachedType Type { get; set; }

        /// <summary>
        /// Location of any inline image we want to display. Must be small.
        /// </summary>
        public string ImageLocation { get; set; }

        /// <summary>
        /// What we should show in the drop down.
        /// </summary>
        public string Title { get; set; }
    }
}
