using System;
using System.Collections.Generic;
using System.Linq;

namespace ITLab29.Models.Domain
{
    public class User
    {

        public string UserId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public UserType UserType { get; set; }
        public UserStatus UserStatus { get; set; }
        public ICollection<Session> Sessions { get; }
        public string Email { get; set; }

        public User(string userId, string firstName, string lastName, UserType userType, UserStatus userStatus, string email)
        {
            if(userId == null || firstName == null || lastName == null || email == null)
            {
                throw new ArgumentNullException();
            }

            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            UserType = userType;
            UserStatus = userStatus;
            Email = email;
            Sessions = new List<Session>();
        }

        public Session AddSession(int id, string title, string description, User responsible, DateTime start, DateTime end, int capacity, Location location) {
            if (Sessions.Any(e => e.SessionId == id))
            {
                throw new ArgumentException($"User {FirstName} {LastName} has already an event with title: {title}");
            }
            Session newSession = new Session(id, title, description, responsible, start, end, capacity, location);
            Sessions.Add(newSession);
            return newSession;
        }

        public Boolean IsActive()
        {
            return UserStatus == UserStatus.ACTIVE;

        }



    }
}
