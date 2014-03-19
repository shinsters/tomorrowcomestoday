﻿namespace TomorrowComesToday.Infrastructure.Interfaces.Repositories
{
    using System.Collections;
    using System.Collections.Generic;

    using SharpArch.Domain.PersistenceSupport;

    using TomorrowComesToday.Domain.Entities;

    /// <summary>
    /// The card repository.
    /// </summary>
    public interface ICardRepository : IRepository<Card>
    {
        /// <summary>
        /// Get a number of cards from the deck
        /// </summary> 
        /// <param name="numberRequired">The number Required</param>
        /// <param name="cardsToExclude">The cards to exclude, so already dealt</param>
        /// <returns>The <see cref="IList"/> of cards</returns>
        IList<Card> GetBlackFromDeck(int numberRequired, IList<Card> cardsToExclude);

        ///// <summary>
        ///// Get user from identifier
        ///// </summary>
        ///// <param name="userFormId">Form Id from h</param>
        ///// <returns></returns>
        //  User GetFromFormId(string userFormId);
    }
}
