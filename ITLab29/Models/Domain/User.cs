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
        public ICollection<Event> Events { get; }
        public string Email { get; set; }

        public User(string userId, string firstName, string lastName, UserType userType, UserStatus userStatus, string email)
        {
            if(userId == null || FirstName == null || lastName == null || email == null)
            {
                throw new ArgumentNullException();
            }

            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            UserType = userType;
            UserStatus = userStatus;
            Email = email;
            Events = new List<Event>();
        }

        public Event AddEvent(int id, string title, string description, User responsible, DateTime start, DateTime end, int capacity, Location location) {
            if (Events.Any(e => e.EventID == id))
            {
                throw new ArgumentException($"User {FirstName} {LastName} has already an event with title: {title}");
            }
            Event newEvent = new Event(title, description, responsible, start, end, capacity, location);
            Events.Add(newEvent);
            return newEvent;
        }



    }
}
