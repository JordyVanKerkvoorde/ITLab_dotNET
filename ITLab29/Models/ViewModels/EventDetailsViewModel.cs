using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITLab29.Models.Domain;
using Microsoft.AspNetCore.Mvc;


namespace ITLab29.Models.ViewModels
{
    public class EventDetailsViewModel
    {
        public User User { get; set; }
        public Session Session { get; set; }
        public IList<Media> Images { get; set; }
        public IList<Media> Files { get; set; }
        public IList<Media> Videos { get; set; }
    }
}
