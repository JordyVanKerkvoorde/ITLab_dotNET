using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Models.Domain
{
    public class Event
    {

        public int EventID { get;}
        public String Title { get; set; }
        public String Description { get; set; }
        public User Responsible { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Capacity { get; set; }
        public Guest Guest { get; set; }
        public Location Location { get; set; }
        public ICollection<User> Attendees { get; set; }
        public IEnumerable<Media> Media { get; set; }

        public Event(string title, string description,
            User responsible, DateTime start, DateTime end, int capacity,
            Location location) {
            if (title == null || description == null || responsible == null || start == null || end == null || location == null) { 
                throw new ArgumentException("Values cannot be NULL");
            }
            Title = title;
            Description = description;
            Responsible = responsible;
            Start = start;
            End = end;
            Capacity = capacity;
            Location = location;
        }

        public User AddAttendee(User user) {
            if (Attendees.Any(a => a.UserId == user.UserId)) {
                throw new ArgumentException("User cannot be added more than once.");
            }
            if (user.UserStatus == UserStatus.BLOCKED) {
                throw new ArgumentException("User cannot be added more than once.");
            }
            Attendees.Add(user);
            return user;
        }


        

    }
}
