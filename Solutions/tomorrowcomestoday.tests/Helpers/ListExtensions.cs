namespace TomorrowComesToday.Tests.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Custom list extensions that're needed where random elements need to happen
    /// </summary>
    public static class ListExtensions
    {

        private static readonly Random Random = new Random();

        /// <summary>
        /// Get a random element from the collection
        /// </summary>
        /// <typeparam name="T">The list</typeparam>
        /// <param name="list">The other list</param>
        /// <returns>A single list item</returns>
        public static T GetRandomItem<T>(this List<T> list)
        {
            return !list.Any() ? default(T) : list.ElementAt(Random.Next(list.Count()));
        }
    }
}
