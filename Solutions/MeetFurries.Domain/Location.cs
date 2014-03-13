namespace TomorrowComesToday.Domain
{
    using SharpArch.Domain.DomainModel;

    public class Location : Entity
    {
        public virtual Meet Meet { get; set; }

        public virtual string Name { get; set; } 

        public virtual string Longitude { get; set; }

        public virtual string Latitude { get; set; }

        public virtual string Description { get; set; }

        public virtual bool IsVenue { get; set; }
    }
}