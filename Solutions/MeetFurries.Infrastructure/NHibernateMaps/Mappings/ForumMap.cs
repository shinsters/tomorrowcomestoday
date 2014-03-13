namespace TomorrowComesToday.Infrastructure.NHibernateMaps.Mappings
{
    using FluentNHibernate.Mapping;

    using TomorrowComesToday.Domain;

    /// <summary>
    /// The user map.
    /// </summary>
    public sealed class ForumMap : ClassMap<Forum>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ForumMap"/> class.
        /// </summary>
        public ForumMap()
        {
            this.Table("Forums");
            this.Id(o => o.Id);
            this.Map(o => o.Slug);
            this.Map(o => o.Accessibility);
            this.HasManyToMany(o => o.Moderators).Table("ForumModerators").ParentKeyColumn("UserId").ChildKeyColumn("ForumId").Inverse();
        }
    }
}
