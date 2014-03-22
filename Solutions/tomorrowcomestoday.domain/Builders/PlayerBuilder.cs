namespace TomorrowComesToday.Domain.Builders
{
    using System;

    using TomorrowComesToday.Domain.Entities;

    /// <summary>
    /// Creates a player
    /// </summary>
    public class PlayerBuilder
    {
        private readonly Player entity = new Player();

        public PlayerBuilder Named(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("No name set while creating player");
            }

            this.entity.Name = name;

            return this;
        }

        public Player Create()
        {
            // sets a globally unique identifer, because the id from sharparch is a little funny
            // with when it gets assigned. It needs to get it from nhibernate, which might not
            // be the case with things in memory.
            this.entity.Guid = Guid.NewGuid();
            return this.entity;
        }
    }
}
