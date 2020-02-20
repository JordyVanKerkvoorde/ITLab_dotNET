using System;
using System.Collections.Generic;
using System.Linq;

namespace ITLab29.Models.Domain
{
    public class User
    {

        #region Properties

        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserType UserType { get; set; }
        public UserStatus UserStatus { get; set; }
        public ICollection<Session> Sessions { get; set; }
        public string Email { get; set; }

        #endregion 

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
