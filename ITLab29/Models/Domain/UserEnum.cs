using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Models.Domain {
        public enum UserStatus { 
            ACTIVE,
            BLOCKED,
            INACTIVE
        }

        public enum UserType { 
            ADMIN,
            RESPONSIBLE,
            USER
        }
    
}
