﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITLab29.Models.Domain;
using ITLab29.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ITLab29.Models.ViewModels;
using ITLab29.Exceptions;
using System.Collections;

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
            try
            {
                Console.WriteLine("path test");
                Console.WriteLine(sessions.First().Media.Count());
                ViewData["preview"] = sessions.First().Media.Where(t => t.Type == MediaType.IMAGE).First().Path;
            }
            catch
            {
                ViewData["preview"] = "/photo/stock.png";
            }
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
            Console.WriteLine("@@@@@@@@@ session media count @@@@@@@@@@");
            Console.WriteLine(session.Media.Where(t => t.Type==MediaType.IMAGE).Count());
            ViewData["user"] = user;
            ViewData["session"] = session;
            ViewData["images"] = session.Media.Where(t => t.Type == MediaType.IMAGE).ToList();
            ViewData["files"] = session.Media.Where(t => t.Type == MediaType.FILE).ToList();
            ViewData["videos"] = session.Media.Where(t => t.Type == MediaType.VIDEO).ToList();
            return View(new FeedBackViewModel() { Session = session });
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
            return RedirectToAction("Details", "Session", new { id });
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

            return RedirectToAction("Details", "Session", new { id });
        }

        [HttpPost]
        public IActionResult AddFeedback(FeedBackViewModel feedback ) {
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
            return RedirectToAction("Details", "Session", new { feedback.id });
        }
        [ServiceFilter(typeof(LoggedOnUserFilter))]
        public IActionResult OpenSessions(User user)
        {
            //IEnumerable<Session> sessions = _sessionRepository.GetByResponsibleId(user.Id);
            IEnumerable<Session> sessions;
            if (user.UserType == UserType.ADMIN)
            {
                sessions = _sessionRepository.GetOpenableSessionsAsAdmin();
                ViewData["opensessions"] = _sessionRepository.GetOpenedSessionsAsAdmin();
            }
            else { 
                sessions = _sessionRepository.GetOpenableSessions(user.Id);
                ViewData["opensessions"] = _sessionRepository.GetOpenedSessions(user.Id);
            }

            sessions.OrderBy(s => s.Start);
            return View(sessions);
        }

        [HttpPost]
        public IActionResult OpenSession(int id)
        {
            try
            {
                Session session = _sessionRepository.GetById(id);
                session.OpenSession();
                _sessionRepository.SaveChanges();
                TempData["message"] = "Sessie succesvol opengezet";

            }
            catch (Exception e)
            {
                TempData["error"] = "Er is iets misgegaan bij het openzetten van de sessie";
            }
            ViewData["message"] = "bajldakl;sdkljfa;sd succesvol opengezet";

            return RedirectToAction("OpenSessions");
        }

        public IActionResult Aanwezigen(int sessionid)
        {
            //IEnumerable<User> users = _sessionRepository.GetRegisteredUsersBySessionId(sessionid);
            IEnumerable<User> users = _userRepository.GetRegisteredBySessionId(sessionid);
            return View(users);           
        }
    }
}