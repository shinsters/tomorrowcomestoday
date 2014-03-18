namespace TomorrowComesToday.Infrastructure.Implementations.Services
{
    using TomorrowComesToday.Domain;
    using TomorrowComesToday.Infrastructure.Enums.Meets;
    using TomorrowComesToday.Infrastructure.Interfaces.Repositories;
    using TomorrowComesToday.Infrastructure.Interfaces.Services;

    /// <summary>
    /// Service managing the configuration of a meet
    /// </summary>
    public class MeetService : IMeetService
    {
        /// <summary>
        /// The meet repository.
        /// </summary>
        private readonly IMeetRepository meetRepository;

        /// <summary>
        /// Initialises a new instance of the <see cref="MeetService"/> class.
        /// </summary>
        /// <param name="meetRepository">The meet repository</param>
        public MeetService(IMeetRepository meetRepository)
        {
            this.meetRepository = meetRepository;
        }

        /// <summary>
        /// Set attendance for a meet
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="slug">The slug for the meet</param>
        /// <param name="attendanceStatus">The attendance status</param>
        public void SetAttendance(User user, string slug, MeetAttendanceStatus attendanceStatus)
        {
            var meet = this.meetRepository.GetEventFromSlug(slug);
            if (meet == null)
            {
                return;
            }

            SetAttendance(user, attendanceStatus, meet);

            this.meetRepository.SaveOrUpdate(meet);
        }

        /// <summary>
        /// Add/Remove attendance based on status
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="attendanceStatus">The attendance status</param>
        /// <param name="meet">The meet</param>
        private static void SetAttendance(User user, MeetAttendanceStatus attendanceStatus, Meet meet)
        {
            // remove from other lists first if needed, then add to correct
            switch (attendanceStatus)
            {
                case MeetAttendanceStatus.Attending:

                    if (meet.MaybeAttendees.Contains(user))
                    {
                        meet.MaybeAttendees.Remove(user);
                    }

                    if (meet.NotAttending.Contains(user))
                    {
                        meet.NotAttending.Remove(user);
                    }

                    if (!meet.Attendees.Contains(user))
                    {
                        meet.Attendees.Add(user);
                    }

                    break;

                case MeetAttendanceStatus.NotAttending:

                    if (meet.Attendees.Contains(user))
                    {
                        meet.MaybeAttendees.Remove(user);
                    }

                    if (meet.MaybeAttendees.Contains(user))
                    {
                        meet.NotAttending.Remove(user);
                    }

                    if (!meet.NotAttending.Contains(user))
                    {
                        meet.NotAttending.Add(user);
                    }

                    break;

                case MeetAttendanceStatus.MaybeAttending:

                    if (meet.Attendees.Contains(user))
                    {
                        meet.MaybeAttendees.Remove(user);
                    }

                    if (meet.NotAttending.Contains(user))
                    {
                        meet.NotAttending.Remove(user);
                    }

                    if (!meet.MaybeAttendees.Contains(user))
                    {
                        meet.NotAttending.Add(user);
                    }

                    break;
            }
        }
    }
}
