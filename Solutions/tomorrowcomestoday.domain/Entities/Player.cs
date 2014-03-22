namespace TomorrowComesToday.Domain.Entities
{
    using System.Collections;
    using System.Collections.Generic;

    using SharpArch.Domain.DomainModel;

    /// <summary>
    /// A game player
    /// </summary>
    public class Player : Entity
    {
        public string Name { get; set; }

        public IList<Card> InHandCards { get; set; }
    }
}
