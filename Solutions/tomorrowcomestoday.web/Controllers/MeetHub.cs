namespace TomorrowComesToday.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Linq;

    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;

    using TomorrowComesToday.Domain;

    [HubName("MeetHub")]
    public class MeetHub : Hub
    {

        public MeetHub()
        {
        }

        //    public void Send(ChatMessageViewModel clientMessage)
        //    {
        //        Guid token;
        //        if (!Guid.TryParse(clientMessage.Token, out token))
        //        {
        //            return;
        //        }

        //        var user = this.userContextService.GetActiveUser();

        //        if (user == null)
        //        {
        //            Debug.WriteLine("Unknown user sent message {0}, this wont be sent to clients", clientMessage.Message);
        //            return;
        //        }

        //        Debug.WriteLine("User {0} sent message {1}", user.Username, clientMessage.Message);

        //        // rebuild with all properties
        //        var message = new ChatMessageViewModel
        //                          {
        //                              Avatar = user.EmailAddress,
        //                              Message = clientMessage.Message,
        //                              Username = user.Username,
        //                              Timestamp = DateTime.Now,
        //                              Id = 0
        //                          };

        //        var channelSlug = this.meetHubStateService.GetJoinedChannelSlug(token);

        //        this.Clients.Group(channelSlug).addMessage(message);
        //    }

        //    /// <summary>
        //    /// User joins a specific channel's group
        //    /// </summary>
        //    /// <param name="token">Token of the waiting connection</param>
        //    public void Join(string token)
        //    {
        //        var parsedToken = this.ParsedToken(token);
        //        if (!parsedToken.HasValue)
        //        {
        //            return;
        //        }

        //        var channelSlug = this.meetHubStateService.AddToChannel(parsedToken.Value);

        //        this.Groups.Add(this.Context.ConnectionId, channelSlug);

        //        var user = this.userContextService.GetActiveUser();

        //        // send all data
        //        this.SendAttendance(channelSlug, user);
        //    }

        //    /// <summary>
        //    /// Users sets their attendance status
        //    /// </summary>
        //    /// <param name="token"></param>
        //    /// <param name="status"></param>
        //    public void SetAttendance(string token, string status)
        //    {
        //        var parsedToken = this.ParsedToken(token);
        //        if (!parsedToken.HasValue)
        //        {
        //            return;
        //        }

        //        // grab user
        //        var user = this.userContextService.GetActiveUser();
        //        if (user == null)
        //        {
        //            return;
        //        }

        //        var slug = this.meetHubStateService.GetJoinedChannelSlug(parsedToken.Value);

        //        MeetAttendanceStatus parsedStatus;

        //        switch (status)
        //        {
        //            case "attending":
        //                parsedStatus = MeetAttendanceStatus.Attending;
        //                break;

        //            case "notattending":
        //                parsedStatus = MeetAttendanceStatus.NotAttending;
        //                break;

        //            case "maybeattending":
        //                parsedStatus = MeetAttendanceStatus.MaybeAttending;
        //                break;

        //            default:
        //                return;
        //        }

        //        // update service with this
        //        this.meetService.SetAttendance(user, slug, parsedStatus);

        //        // inform all other clients of new set up
        //        // todo, this
        //    }

        //    /// <summary>
        //    /// Update all connected users on a channel with better attendance statuses
        //    /// </summary>
        //    /// <param name="slug">Channel Slug</param>
        //    /// <param name="user">Specific user, null for all</param>
        //    private void SendAttendance(string slug, User user)
        //    {
        //        var meet = this.meetRepository.GetEventFromSlug(slug);

        //        // we need to put all three types of meet into a single list of the view model
        //        var attendees =
        //            meet.Attendees.Select(
        //                attendee =>
        //                new AttendanceViewModel
        //                    {
        //                        Avatar = attendee.EmailAddress,
        //                        Username = attendee.Username,
        //                        Status = "attending"
        //                    }).ToList();

        //        attendees.AddRange(
        //            meet.MaybeAttendees.Select(
        //                attendee =>
        //                new AttendanceViewModel
        //                    {
        //                        Avatar = attendee.EmailAddress,
        //                        Username = attendee.Username,
        //                        Status = "maybeattending"
        //                    }));

        //        attendees.AddRange(
        //            meet.MaybeAttendees.Select(
        //                attendee =>
        //                new AttendanceViewModel
        //                {
        //                    Avatar = attendee.EmailAddress,
        //                    Username = attendee.Username,
        //                    Status = "maybeattending"
        //                }));


        //        attendees.Add(new AttendanceViewModel
        //                            {
        //                                Username = "Hank Testman",
        //                                Status = "attending"
        //                            });

        //        attendees.Add(new AttendanceViewModel
        //        {
        //            Username = "Bob Never turns up",
        //            Status = "notattending"
        //        });


        //        foreach (var attendanceViewModel in attendees)
        //        {
        //            this.Clients.Group(slug).addAttending(attendanceViewModel);
        //        }

        //    }

        //    /// <summary>
        //    /// Get a GUID from a token
        //    /// </summary>
        //    /// <param name="token">The string token</param>
        //    /// <returns>The GUID formatted token</returns>
        //    private Guid? ParsedToken(string token)
        //    {
        //        Guid parsedToken;

        //        if (!Guid.TryParse(token, out parsedToken))
        //        {
        //            return null;
        //        }

        //        return parsedToken;
        //    }
        //}

    }
}


