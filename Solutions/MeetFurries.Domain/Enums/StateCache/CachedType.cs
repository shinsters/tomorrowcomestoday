namespace TomorrowComesToday.Domain.Enums.StateCache
{
    /// <summary>
    /// Types of cached object we're saving in the session singleton
    /// </summary>
    public enum CachedType
    {
        /// <summary>
        /// User profiles
        /// </summary>
        User,

        /// <summary>
        /// Events that are happening, or haven't happened yet
        /// </summary>
        Event,

        /// <summary>
        /// Events that have happened in the past
        /// </summary>
        HistoricalEvents,

        /// <summary>
        /// The town or city where the event was held.  This is not the same as a location.
        /// </summary>
        City
    }
}
