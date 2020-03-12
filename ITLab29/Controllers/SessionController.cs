using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITLab29.Models.Domain;
using ITLab29.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ITLab29.Models.ViewModels;
using ITLab29.Exceptions;

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
                try
                {
                    sessions = _sessionRepository.GetAll();
                }
                catch (EmptyListException e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                    sessions = new List<Session>();
                }
            } else {
                try
                {
                    sessions = _sessionRepository.GetByDate(date ?? DateTime.Now);
                    //aanpassen na database met data of dummy dates ^^^^ 
                }
                catch (EmptyListException e)
                {
                    Console.Error.WriteLine(e.StackTrace);
                    sessions = new List<Session>();
                }
            }
            sessions = sessions.OrderBy(s => s.Start).ToList();
            return View(sessions);
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<Session> sessions;
                sessions = _sessionRepository.GetAll();
                return Ok(sessions.OrderBy(s => s.Start).ToList());
            }
            catch (EmptyListException e)
            {
                Console.Error.WriteLine(e.StackTrace);
                // Eventueel nog aanpassen en ViewData gebruiken?
                return Ok(new List<Session>());
            }
           
        }
        [Route("session/{id}")]
        [ServiceFilter(typeof(LoggedOnUserFilter))]
        public IActionResult Details(int id, User user) {
            Session session;
            try
            {
                session = _sessionRepository.GetById(id);
            }
            catch (ArgumentNullException e)
            {
                Console.Error.WriteLine(e.StackTrace);
                return NotFound();
            }
            
            ViewData["user"] = user;
            ViewData["session"] = session;
            return View(new FeedBackViewModel());
        }

        [HttpPost]
        [ServiceFilter(typeof(LoggedOnUserFilter))]
        public IActionResult Add(int id, User user) {
            Session session;
            try
            {
                session = _sessionRepository.GetById(id);
            }
            catch (ArgumentNullException e)
            {
                Console.Error.WriteLine(e.StackTrace);
                return NotFound();
            }
            session.AddUserSession(user);
            // maar 1 repository saven want die savet de context.
            _userRepository.SaveChanges();

            ViewData["sessionId"] = id;
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ServiceFilter(typeof(LoggedOnUserFilter))]
        public IActionResult Delete(int id, User user) {
            Session session;
            try
            {
                session = _sessionRepository.GetById(id);
            }
            catch (ArgumentNullException e)
            {
                Console.Error.WriteLine(e.StackTrace);
                return NotFound();
            }
            user.RemoveUserSession(session);
            _userRepository.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddFeedback(FeedBackViewModel feedback ) {
            Console.WriteLine(feedback.id);
            Console.WriteLine(feedback.Score);
            Console.WriteLine(feedback.Description);
            Session session;
            try
            {
                session = _sessionRepository.GetById(feedback.id);
            }
            catch (ArgumentNullException e)
            {
                Console.Error.WriteLine(e.StackTrace);
                return NotFound();
            }
            session.AddFeedback(new Feedback(feedback.Score, feedback.Description));
            _sessionRepository.SaveChanges();
            return RedirectToAction("Index");
        }
        [ServiceFilter(typeof(LoggedOnUserFilter))]
        public IActionResult OpenSessions(User user)
        {
            //IEnumerable<Session> sessions = _sessionRepository.GetByResponsibleId(user.Id);
            IEnumerable<Session> sessions;
            if (user.UserType == UserType.ADMIN)
                sessions = _sessionRepository.GetOpenableSessionsAsAdmin();
            else
                sessions = _sessionRepository.GetOpenableSessions(user.Id);

            sessions.OrderBy(s => s.Start);
            return View(sessions);
        }

        [HttpPost]
        [ServiceFilter(typeof(LoggedOnUserFilter))]
        public IActionResult OpenSession(User user, int id)
        {
            Session session = _sessionRepository.GetById(id);

            //session.



            return RedirectToAction("OpenSessions");
        }
    }
}