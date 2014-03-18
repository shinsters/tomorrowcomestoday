namespace TomorrowComesToday.Infrastructure.NHibernateMaps.Mappings
{
    using FluentNHibernate.Mapping;

    using TomorrowComesToday.Domain;

    /// <summary>
    /// The user map.
    /// </summary>
    public sealed class LocationMap : ClassMap<Location>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="UserMap"/> class.
        /// </summary>
        public LocationMap()
        {
            this.Table("Locations");
            this.Id(o => o.Id);
            this.Map(o => o.Name);
            this.Map(o => o.Latitude);
            this.Map(o => o.Longitude);
            this.References(o => o.Meet).Column("MeetId");
            this.Map(o => o.IsVenue);
            this.Map(o => o.Description);
        }
    }
}
