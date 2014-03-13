namespace TomorrowComesToday.Infrastructure.NHibernateMaps.Mappings
{
    using FluentNHibernate.Mapping;

    using TomorrowComesToday.Domain;

    /// <summary>
    /// The user map.
    /// </summary>
    public sealed class CommentMap : ClassMap<Comment>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="MeetMap"/> class.
        /// </summary>
        public CommentMap()
        {
            this.Table("Comments");
            this.Id(o => o.Id);
            this.Map(o => o.Message);
            this.Map(o => o.TimeStamp);
            this.References(o => o.User, "UserId").Cascade.None();
            this.References(o => o.Meet, "MeetId").Cascade.None();
        }
    }
}
