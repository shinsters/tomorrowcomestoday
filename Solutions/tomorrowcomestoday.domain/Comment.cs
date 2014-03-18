namespace TomorrowComesToday.Domain
{
    using System;

    using SharpArch.Domain.DomainModel;

    public class Comment : Entity
    {
        public virtual Meet Meet { get; set; }

        public virtual User User { get; set; }

        public virtual string Message { get; set; }

        public virtual DateTime TimeStamp { get; set; }
    }
}