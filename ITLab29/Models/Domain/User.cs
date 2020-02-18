using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Models.Domain
{
    public class User
    {

        public string UserId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public int MyProperty { get; set; }
    }
}
