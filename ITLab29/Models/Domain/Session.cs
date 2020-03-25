using ITLab29.Exceptions;
using ITLab29.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Models.Domain
{
    public class Session
    {

        private string _shortDescription => StringModifier.TruncateAtWord(Description, 200);

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
        public bool IsOpened { get; set; }
        public ICollection<User> PresentUsers { get; set; }

        public Session() { }
        public Session(string title, string description,
            User responsible, DateTime start, DateTime end, int capacity,
            Location location) {
            Title = title;
            Description = description;
            Responsible = responsible;
            Start = start;
            End = end;
            Capacity = capacity;
            Location = location;
            IsOpened = false;

            UserSessions = new List<UserSession>();
            Media = new List<Media>();
            Guests = new List<Guest>();
            Feedback = new List<Feedback>();
            PresentUsers = new List<User>();
        }


        public Media AddMedia(MediaType type, string path) {
            Media media = new Media(type, path);
            Media.Add(media);
            return media;
        }
        public Media AddMedia(Media media)
        {
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
            if (!WroteFeedback(feedback.User)) {
                Feedback.Add(feedback);
            } else {
                throw new Exception("Je kan maar 1 keer feedback geven per sessie");
            }
            return feedback;
        }

        public string GetDateFormat() {
            return Start.ToString("d/M/yyyy");
        }

        public string GetTimeFormat() {
            return Start.ToString("HH:mm");
        }

        public void AddUserSession( User user) {
            if (!SessionFull() && user.UserStatus != UserStatus.BLOCKED && !UserSessions.Where(us => us.User == user).Any()) {
                UserSession us = new UserSession();
                us.User = user;
                us.UserId = user.UserId;
                us.SessionId = SessionId;
                us.Session = this;
                UserSessions.Add(us);
            } else {
                throw new Exception("Er moeten beschikbare plekken zijn en je mag geen blocked user zijn");
            }
            
        }

        public void RemoveUserSession(User user) {
            UserSession us = UserSessions.Where(u => u.Session == this && u.User == user).FirstOrDefault();
            if(us != null)
            {
                UserSessions.Remove(us);
            }
            else
            {
                throw new Exception($"User not found for session: {this}");
            }
        }

        public ICollection<User> GetUsers() {

            List<User> result = UserSessions.Where(u => u.SessionId == SessionId).Select(u => u.User).ToList();
            return result;
        }

        public string GetShortDescription()
        {
            return _shortDescription;
        }

        public void OpenSession()
        {
            if (IsOpened)
                throw new AlreadyOpenException("Session already open");
            else
                IsOpened = true;
        }

        public void CloseSession()
        {
            if (!IsOpened)
                throw new AlreadyOpenException("Session already closed");
            else
                IsOpened = false;
        }

        public bool WroteFeedback(User user) {
            return Feedback.Where(f => f.User == user).Any();
        }

        public bool SessionFull() {
            return UserSessions.Where(p => p.SessionId == SessionId).Count() >= Capacity;
        }

        public void RegisterUserPresent(User user)
        {
            if (PresentUsers.Contains(user))
            {
                throw new ArgumentException();
            }
            else
            {
                PresentUsers.Add(user);
            }
            
        }

        public void RegisterUsersPresent(IEnumerable<User> users)
        {
            PresentUsers.Concat(users);
        }

        public void RemoveUserPresent(User user)
        {
            if (!PresentUsers.Contains(user)) {
                throw new ArgumentException();
            }
            else
            {
                PresentUsers.Remove(user);
            }
            
        }
    }
}
