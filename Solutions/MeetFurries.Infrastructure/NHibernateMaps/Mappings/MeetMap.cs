namespace TomorrowComesToday.Infrastructure.NHibernateMaps.Mappings
{
    using FluentNHibernate.Mapping;

    using TomorrowComesToday.Domain;

    /// <summary>
    /// The user map.
    /// </summary>
    public sealed class MeetMap : ClassMap<Meet>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="MeetMap"/> class.
        /// </summary>
        public MeetMap()
        {
            this.Table("Meets");
            this.Id(o => o.Id);
            this.Map(o => o.StartTime);
            this.Map(o => o.EndTime);
            this.Map(o => o.Name);
            this.Map(o => o.Description);
            this.Map(o => o.Slug);
            this.References(o => o.Administrator, "AdministratorId").Cascade.None();
            this.HasManyToMany(o => o.Attendees).Table("MeetAttendence").ParentKeyColumn("MeetId").ChildKeyColumn("UserId");
            this.HasManyToMany(o => o.MaybeAttendees).Table("MeetMaybeAttendence").ParentKeyColumn("MeetId").ChildKeyColumn("UserId");
            this.HasManyToMany(o => o.NotAttending).Table("MeetNotAttendance").ParentKeyColumn("MeetId").ChildKeyColumn("UserId");
            this.HasMany(o => o.Locations).KeyColumn("MeetId").Inverse().Cascade.All();
            this.HasMany(o => o.Comments).KeyColumn("MeetId").Inverse().Cascade.All();
        }
    }
}
