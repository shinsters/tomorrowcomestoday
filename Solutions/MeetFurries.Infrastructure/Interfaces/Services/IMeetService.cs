namespace TomorrowComesToday.Infrastructure.Interfaces.Services
{
    using TomorrowComesToday.Domain;
    using TomorrowComesToday.Infrastructure.Enums.Meets;

    /// <summary>
    /// Service managing the configuration of a meet
    /// </summary>
    public interface IMeetService
    {
        /// <summary>
        /// Set attendance for a meet
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="slug">The slug for the meet</param>
        /// <param name="attendanceStatus">The attendance status</param>
        void SetAttendance(User user, string slug, MeetAttendanceStatus attendanceStatus);
    }
}
