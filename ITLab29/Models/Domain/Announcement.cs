using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Models.Domain {
    public class Announcement {

        public int Id { get; set; }
        public DateTime PostTime { get; set; }
        public string Message { get; set; }


        public Announcement(DateTime postTime, string message) {
            PostTime = postTime;
            Message = message;
        }

    }
}
