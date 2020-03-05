using ITLab29.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Models.Domain
{
    public class Session
    {

        private string _shortDescription => DescriptionModifier.TruncateAtWord(Description, 200);

        public int SessionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public User Responsible { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Capacity { get; set; }
        public Location Location { get; set; }
        public ICollection<UserSession> UserSessions { get; set; }
        public ICollection<Media> Media { get; set; }
        public ICollection<Guest> Guests { get; set; }
        public ICollection<Feedback> Feedback { get; set; }

        public Session() { }
        public Session(string title, string description,
            User responsible, DateTime start, DateTime end, int capacity,
            Location location) {
            //if (title == null || description == null || responsible == null ||  location == null) { 
            //    throw new ArgumentException("Values cannot be NULL");
            //}
            //if (capacity > location.Capacity)
            //{
            //    throw new ArgumentException("Location has not enough places to host event.");
            //}
            //if (capacity <= 0)
            //{
            //    throw new ArgumentException("Capacity can't be negative or zero");
            //}
            //if (!responsible.IsActive())
            //{
            //    throw new ArgumentException("The responsible person is not an active user");
            //}
            //if (start > end)
            //{
            //    throw new ArgumentException("Endtime can't be before starttime.");
            //}
            Title = title;
            Description = description;
            Responsible = responsible;
            Start = start;
            End = end;
            Capacity = capacity;
            Location = location;

            UserSessions = new List<UserSession>();
            Media = new List<Media>();
            Guests = new List<Guest>();
            Feedback = new List<Feedback>();
        }

        //public User AddAttendee(string userId, string firstName, string lastName, UserType userType, UserStatus userStatus, string email) {
            //if (userId != null && Attendees.Any(a => a.UserId == userId)) {
            //    throw new ArgumentException("User cannot be added more than once.");
            //}
            //if (userStatus == UserStatus.BLOCKED) {
            //    throw new ArgumentException("Blocked user cannot be added.");
            //}
            //User user = new User(userId, firstName, lastName, userType, userStatus, email);
            //Attendees.Add(user);
            //return user;
        //}

        public Media AddMedia(int mediaId, MediaType type, string path) {
            if (Media.Any(m => m.MediaId == mediaId)) {
                throw new ArgumentException("Media exists.");
            }
            Media media = new Media(type, path);
            Media.Add(media);
            return media;
        }

        public Guest AddGuest(string name, string email, string phoneNumber) {
            if (Guests.Any(g => g.Name == name)) {
                throw new ArgumentException("Guest cannot be added twice.");
            }
            Guest guest = new Guest(name, email, phoneNumber);
            Guests.Add(guest);
            return guest;
        }

        public Feedback AddFeedback(Feedback feedback) {
            /*if (Feedback.Any(f => f.FeedbackId == feedbackId)) {
                throw new ArgumentException("Feedback cannot be added twice.");
            }
            Feedback feedback = new Feedback(score,  description);*/
            Feedback.Add(feedback);
            return feedback;
        }

        public string GetDateFormat() {
            return Start.ToString("d/M/yyyy");
        }

        public string GetTimeFormat() {
            return Start.ToString("HH:mm");
        }

        public void AddUserSession( User user) {
            if (UserSessions.Count() < Capacity || user.UserStatus != UserStatus.BLOCKED) {
                UserSessions.Add(new UserSession {
                    Session = this,
                    User = user,
                    UserId = user.UserId,
                    SessionId = SessionId
                });
            } else {
                throw new Exception("Er moeten beschikbare plekken zijn en je mag geen blocked user zijn");
            }
            
        }

        public void RemoveUserSession(User user) {
            UserSessions.Remove(UserSessions.Where(u => u.Session == this && u.User == user).FirstOrDefault());
        }

        public IEnumerable<User> GetUsers() {
            return UserSessions.Where(u => u.SessionId == SessionId).Select(u => u.User).ToList();
        }

        public string GetShortDescription()
        {
            return _shortDescription;
        }
    }
}
