namespace TomorrowComesToday.Infrastructure.Helpers
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The list helper.
    /// </summary>
    public static class ListHelper
    {
        /// <summary>
        /// Perform a Fisher-Yates shuffle
        /// </summary>
        /// <param name="list">The list to shuffle</param>
        /// <typeparam name="T">The type</typeparam>
        public static void Shuffle<T>(this IList<T> list)
        {
            var rng = new Random();
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = rng.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
