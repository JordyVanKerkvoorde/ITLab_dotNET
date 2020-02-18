using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Models.Domain
{
    public class Guest
    {

        public int GuestId { get; }
        public string Name { get; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Event> Events { get; set; }


        public Guest(string name, string email, string phoneNumber)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Events = new List<Event>();
        }



        public Event AddEvent(int id, string title, string description, User responsible, DateTime start, DateTime end, int capacity, Location location)
        {
            if (Events.Any(e => e.EventID == id))
            {
                throw new ArgumentException($"User {Name} has already an event with title: {title}");
            }
            Event newEvent = new Event(title, description, responsible, start, end, capacity, location);
            Events.Add(newEvent);
            return newEvent;
        }
    }
}
