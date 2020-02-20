using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Models.Domain
{
    public class Session
    {

        public int SessionId { get;}
        public string Title { get; set; }
        public string Description { get; set; }
        public User Responsible { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Capacity { get; set; }
        public Location Location { get; set; }
        public ICollection<User> Attendees { get; set; }
        public ICollection<Media> Media { get; set; }
        public ICollection<Guest> Guests { get; set; }
        public ICollection<Feedback> Feedback { get; set; }


        public Session(int eventId, string title, string description,
            User responsible, DateTime start, DateTime end, int capacity,
            Location location) {
            if (title == null || description == null || responsible == null ||  location == null) { 
                throw new ArgumentException("Values cannot be NULL");
            }
            if (capacity > location.Capacity)
            {
                throw new ArgumentException("Location has not enough places to host event.");
            }
            if (capacity <= 0)
            {
                throw new ArgumentException("Capacity can't be negative or zero");
            }
            if (!responsible.IsActive())
            {
                throw new ArgumentException("The responsible person is not an active user");
            }
            if (start > end)
            {
                throw new ArgumentException("Endtime can't be before starttime.");
            }
            SessionId = eventId;
            Title = title;
            Description = description;
            Responsible = responsible;
            Start = start;
            End = end;
            Capacity = capacity;
            Location = location;

            Attendees = new List<User>();
            Media = new List<Media>();
            Guests = new List<Guest>();
            Feedback = new List<Feedback>();
        }

        public User AddAttendee(string userId, string firstName, string lastName, UserType userType, UserStatus userStatus, string email) {
            if (userId != null && Attendees.Any(a => a.UserId == userId)) {
                throw new ArgumentException("User cannot be added more than once.");
            }
            if (userStatus == UserStatus.BLOCKED) {
                throw new ArgumentException("Blocked user cannot be added.");
            }
            User user = new User(userId, firstName, lastName, userType, userStatus, email);
            Attendees.Add(user);
            return user;
        }

        public Media AddMedia(int mediaId, MediaType type, string path) {
            if (Media.Any(m => m.MediaId == mediaId)) {
                throw new ArgumentException("Media exists.");
            }
            Media media = new Media(mediaId, type, path);
            Media.Add(media);
            return media;
        }

        public Guest AddGuest(int guestId, string name, string email, string phoneNumber) {
            if (Guests.Any(g => g.GuestId == guestId)) {
                throw new ArgumentException("Guest cannot be added twice.");
            }
            Guest guest = new Guest(guestId, name, email, phoneNumber);
            Guests.Add(guest);
            return guest;
        }

        public Feedback AddFeedback(int feedbackId, int score, User user, string description) {
            if (Feedback.Any(f => f.FeedbackId == feedbackId)) {
                throw new ArgumentException("Feedback cannot be added twice.");
            }
            Feedback feedback = new Feedback(feedbackId, score, user, description);
            Feedback.Add(feedback);
            return feedback;
        }

    }
}
