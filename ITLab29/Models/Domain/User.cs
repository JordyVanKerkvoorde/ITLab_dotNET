using System.Collections.Generic;

namespace ITLab29.Models.Domain
{
    public class User
    {

        public string UserId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public UserEnum.UserType UserType { get; set; }
        public UserEnum.UserStatus UserStatus { get; set; }
        public ICollection<Event> Events { get; }
        public string Email { get; set; }

        public User(string userId, string firstName, string lastName, UserEnum.UserType userType, UserEnum.UserStatus userStatus, string email)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            UserType = userType;
            UserStatus = userStatus;
            Email = email;
        }

        public void AddE(Event e) {

        }



    }
}
