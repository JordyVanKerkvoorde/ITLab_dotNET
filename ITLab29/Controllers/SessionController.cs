using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITLab29.Models.Domain;
using ITLab29.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ITLab29.Models.ViewModels;

namespace ITLab29.Controllers
{
    [ServiceFilter(typeof(LoggedOnUserFilter))]
    public class SessionController : Controller
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUserRepository _userRepository;

        public SessionController(ISessionRepository sessionRepository, IUserRepository userRepository) {
            _sessionRepository = sessionRepository;
            _userRepository = userRepository;
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
        [ServiceFilter(typeof(LoggedOnUserFilter))]
        public IActionResult Details(int id, User user) {
            Session session = _sessionRepository.GetById(id);
            if (session == null) {
                return NotFound();
            }

            ViewData["user"] = user;
            ViewData["session"] = session;
            return View(new FeedBackViewModel(new Feedback()));
        }

        [HttpPost]
        [ServiceFilter(typeof(LoggedOnUserFilter))]
        public IActionResult Add(int id, User user) {
            Session session = _sessionRepository.GetById(id);
            session.AddUserSession(user);
            _userRepository.SaveChanges();
            _sessionRepository.SaveChanges();

            ViewData["sessionId"] = id;
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ServiceFilter(typeof(LoggedOnUserFilter))]
        public IActionResult Delete(int id, User user) {
            Session session = _sessionRepository.GetById(id);
            user.RemoveUserSession(session);
            session.RemoveUserSession(user);
            _userRepository.SaveChanges();
            _sessionRepository.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddFeedback(int id, FeedBackViewModel fbvm) {
            Feedback fb = new Feedback(fbvm.Score, fbvm.Description);
            Session session = _sessionRepository.GetById(id);
            session.AddFeedback(fb);
            _sessionRepository.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}