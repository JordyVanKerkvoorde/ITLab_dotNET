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
                //aanpassen na database met data of dummy dates ^^^^ 
            }
            sessions = sessions.OrderBy(s => s.Start).ToList();
            return View(sessions);
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Session> sessions;
            sessions = _sessionRepository.GetAll();
            return Ok(sessions.OrderBy(s => s.Start).ToList());
        }
        [Route("session/{id}")]
        public IActionResult Details(int id) {
            Session session = _sessionRepository.GetById(id);
            if (session == null) {
                return NotFound();
            }
            return View(session);
        }

    }
}