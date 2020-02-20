using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Models.Domain
{
    public class Guest
    {

        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }


        public Guest(string name, string email, string phoneNumber)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }

    }
}
