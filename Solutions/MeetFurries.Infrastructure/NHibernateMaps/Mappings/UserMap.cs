namespace TomorrowComesToday.Infrastructure.NHibernateMaps.Mappings
{
    using FluentNHibernate.Mapping;

    using TomorrowComesToday.Domain;
    using TomorrowComesToday.Infrastructure.Enums.Authentication;

    /// <summary>
    /// The user map.
    /// </summary>
    public sealed class UserMap : ClassMap<User>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="UserMap"/> class.
        /// </summary>
        public UserMap()
        {
            this.Table("Users");
            this.Id(o => o.Id);
            this.Map(o => o.Username);
            this.Map(o => o.PhoneNumber);
            this.Map(o => o.EmailAddress);
            this.Map(o => o.AuthenticationProvider).CustomType<AuthenticationProvider>();
            this.Map(o => o.SecretToken);
            this.Map(o => o.Token);
            this.Map(o => o.FormsId);
            this.HasMany(o => o.OrganisedEvents).KeyColumn("AdministratorId").Inverse().Cascade.All();
            this.HasMany(o => o.Comments).KeyColumn("UserId").Inverse().Cascade.All();
            this.HasManyToMany(o => o.AttendingEvents).Table("MeetAttendence").ParentKeyColumn("UserId").ChildKeyColumn("MeetId").Inverse();
            this.HasManyToMany(o => o.MaybeAttendingEvents).Table("MeetMaybeAttendence").ParentKeyColumn("UserId").ChildKeyColumn("MeetId").Inverse();
        }
    }
}
