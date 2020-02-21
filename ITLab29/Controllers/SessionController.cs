using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITLab29.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ITLab29.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionRepository _sessionRepository;

        public SessionController(ISessionRepository sessionRepository) {
            _sessionRepository = sessionRepository;
        }

        public IActionResult Index(DateTime? date)
        {
            IEnumerable<Session> sessions;
            if (date == null) {
                sessions = _sessionRepository.GetAll();
            } else {
                sessions = _sessionRepository.GetByDate(date??DateTime.Now);
                //aanpassen ^^^^ 
            }
            sessions = sessions.OrderBy(s => s.Start).ToList();
            return View(sessions);
        }

        public IActionResult Details() {
            return View();
        }

    }
}