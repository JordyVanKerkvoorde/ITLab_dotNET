using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITLab29.Exceptions;
using ITLab29.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ITLab29.Controllers
{
    public class AnnouncementController : Controller
    {
        private readonly IAnnouncementRepository _announcementRepository;

        public AnnouncementController(IAnnouncementRepository announcementRepository) {
            _announcementRepository = announcementRepository;
        }

        public IActionResult Index()
        {
            List<Announcement> announcements;
            try
            {
                announcements = _announcementRepository.GetAll().OrderByDescending(a => a.PostTime).ToList();
            }
            catch (EmptyListException e)
            {
                Console.Error.WriteLine(e.StackTrace);
                announcements = new List<Announcement>();
            }
            return View(announcements);
        }
    }
}