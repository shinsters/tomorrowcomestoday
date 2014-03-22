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
            return this.entity;
        }
    }
}
