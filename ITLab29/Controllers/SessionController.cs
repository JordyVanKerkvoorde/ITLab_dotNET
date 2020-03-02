using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITLab29.Models.Domain;
using ITLab29.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITLab29.Controllers
{
    [ServiceFilter(typeof(LoggedOnUserFilter))]
    public class SessionController : Controller
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserSessionRepository _userSessionRepository;

       // private readonly UserManager<User> _userManager;

        public SessionController(ISessionRepository sessionRepository, IUserRepository userRepository, UserManager<User> userManager, IUserSessionRepository userSessionRepository) {
            _sessionRepository = sessionRepository;
            _userRepository = userRepository;
            //_userManager = userManager;
            _userSessionRepository = userSessionRepository;
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
            return View(session);
        }

        [HttpPost]
        [ServiceFilter(typeof(LoggedOnUserFilter))]
        public IActionResult Add(int id, User user) {
            //User user = _userManager.FindByIdAsync(_userManager.GetUserId(User)).Result;
            Session session = _sessionRepository.GetById(id);
            if (session.UserSessions.Count() < session.Capacity || user.UserStatus != UserStatus.BLOCKED) {
                _userSessionRepository.AddSessiontoUser(session, user);
                _userSessionRepository.SaveChanges();
            } 

            ViewData["sessionId"] = id;
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ServiceFilter(typeof(LoggedOnUserFilter))]
        public IActionResult Delete(int id, User user) {
            //User user = _userManager.FindByIdAsync(_userManager.GetUserId(User)).Result;
            Session session = _sessionRepository.GetById(id);
            _userSessionRepository.RemoveUserSession(session, user);
            _userSessionRepository.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}