using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Models.Domain
{
    public class UserSession
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public int SessionId { get; set; }
        public Session Session { get; set; }
    }
}
