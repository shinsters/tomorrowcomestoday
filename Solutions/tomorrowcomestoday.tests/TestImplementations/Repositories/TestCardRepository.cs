﻿namespace TomorrowComesToday.Tests.TestImplementations.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using SharpArch.Domain.PersistenceSupport;

    using TomorrowComesToday.Domain.Entities;
    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;

    /// <summary>
    /// An in memory dumb version of the test card repository
    /// </summary>
    public class TestCardRepository : ICardRepository
    {
        /// <summary>
        /// Constructs 
        /// </summary>
        public TestCardRepository()
        {
            this.Cards = new List<Card>();
        }

        public IDbContext DbContext { get; set; }

        /// <summary>
        /// Domain object
        /// </summary>
        private IList<Card> Cards { get; set; }

        public Card Get(int id)
        {
            return this.Cards.FirstOrDefault(o => o.Id == id);
        }

        public IList<Card> GetAll()
        {
            return this.Cards;
        }

        public Card SaveOrUpdate(Card entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Card entity)
        {
            throw new System.NotImplementedException();
        }
    }
}