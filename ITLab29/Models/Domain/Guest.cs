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


        public Guest(int guestId, string name, string email, string phoneNumber)
        {
            GuestId = guestId;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }

    }
}
