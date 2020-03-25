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

        public IActionResult Index()
        {
            IEnumerable<Session> sessions;
            
            try
            {
                sessions = _sessionRepository.GetAll();
            }
            catch (EmptyListException e)
            {
                Console.Error.WriteLine(e.StackTrace);
                sessions = new List<Session>();
            }

            try
            {
                sessions = sessions.OrderBy(s => s.Start).ToList();
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
                return Ok(new List<Session>());
            }

        }
        [Route("session/{id}")]
        [ServiceFilter(typeof(LoggedOnUserFilter))]
        public IActionResult Details(int id, User user) {
            Session session;
            EventDetailsViewModel sessionDetailsViewModel;
            FeedBackViewModel feedBackViewModel;
            try
            {
                session = _sessionRepository.GetById(id);
                sessionDetailsViewModel = new EventDetailsViewModel
                {
                    User = user,
                    Session = session,
                    Images = session.Media.Where(t => t.Type == MediaType.IMAGE).ToList(),
                    Files = session.Media.Where(t => t.Type == MediaType.FILE).ToList(),
                    Videos = session.Media.Where(t => t.Type == MediaType.VIDEO).ToList()
                };
                feedBackViewModel = new FeedBackViewModel() { Session = session };
            }
            catch (SessionNotFoundException e)
            {
                Console.Error.WriteLine(e.StackTrace);
                return NotFound();
            }

            return View(new Tuple<FeedBackViewModel, EventDetailsViewModel>(feedBackViewModel, sessionDetailsViewModel));
        }

        [HttpPost]
        [ServiceFilter(typeof(LoggedOnUserFilter))]
        public IActionResult Add(int id, User user) {
            Session session;
            try
            {
                session = _sessionRepository.GetById(id);
                session.AddUserSession(user);
                // maar 1 repository saven want die savet de context.
                _userRepository.SaveChanges();
                ViewData["sessionId"] = id;
            }
            catch (SessionNotFoundException e)
            {
                Console.Error.WriteLine(e.StackTrace);
                return NotFound();
            }
            // catch exception uit session.AddUserSession
            catch (Exception)
            {
                _userRepository.SaveChanges();
                ViewData["sessionId"] = id;
            }
            return RedirectToAction("Details", "Session", new { id });
        }

        [HttpPost]
        [ServiceFilter(typeof(LoggedOnUserFilter))]
        public IActionResult Delete(int id, User user) {
            Session session;
            try
            {
                session = _sessionRepository.GetById(id);
                session.RemoveUserSession(user);
                _userRepository.SaveChanges();
            }
            catch (SessionNotFoundException e)
            {
                Console.Error.WriteLine(e.StackTrace);
                return NotFound();
            }

            return RedirectToAction("Details", "Session", new { id });
        }

        [HttpPost]
        public IActionResult AddFeedback(FeedBackViewModel feedback) {
            Session session;
            User user;
            try
            {
                session = _sessionRepository.GetById(feedback.id);
                user = _userRepository.GetById(feedback.UserId);
                session.AddFeedback(new Feedback(feedback.Score, feedback.Description, user));
                _sessionRepository.SaveChanges();
            }
            catch (SessionNotFoundException e)
            {
                Console.Error.WriteLine(e.StackTrace);
                return NotFound();
            }
            catch (UserNotFoundException e)
            {
                Console.Error.WriteLine(e.StackTrace);
                return NotFound();
            }

            return RedirectToAction("Details", "Session", new { feedback.id });
        }

        [ServiceFilter(typeof(LoggedOnUserFilter))]
        public IActionResult OpenSessions(User user)
        {
            IEnumerable<Session> sessions;
            try
            {
                if (user.UserType == UserType.ADMIN)
                {
                    sessions = _sessionRepository.GetOpenableSessionsAsAdmin();
                    ViewData["opensessions"] = _sessionRepository.GetOpenedSessionsAsAdmin();
                }
                else
                {
                    sessions = _sessionRepository.GetOpenableSessions(user.Id);
                    ViewData["opensessions"] = _sessionRepository.GetOpenedSessions(user.Id);
                }

                sessions.OrderBy(s => s.Start);

            }
            catch (Exception)
            {
                throw;
            }

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
            catch (Exception)
            {
                TempData["error"] = "Er is iets misgegaan bij het openzetten van de sessie";
            }

            return RedirectToAction("OpenSessions");
        }

        [HttpPost]
        public IActionResult CloseSession(int id)
        {
            try
            {
                Session session = _sessionRepository.GetById(id);
                session.CloseSession();
                _sessionRepository.SaveChanges();
                TempData["message"] = "Sessie succesvol gesloten";
            }
            catch (Exception)
            {
                TempData["error"] = "Er is iets misgegaan bij het sluiten van de sessie";
            }

            return RedirectToAction("OpenSessions");
        }

        public IActionResult Aanwezigen(int id)
        {
            IEnumerable<User> users = _userRepository.GetRegisteredBySessionId(id);
            ViewData["session"] = id;
            Session session = _sessionRepository.GetById(id);
            ViewData["presentusers"] = session == null ? new List<User>() : session.PresentUsers ;
            return View(users);
        }

        public IActionResult SetUserPresent(string id, int sessionid) 
        {
            try
            {
                Session session = _sessionRepository.GetById(sessionid);
                User user = _userRepository.GetById(id);
                if (user == null || session.PresentUsers == null)
                {
                    return NotFound();
                }
                ViewData["presentusers"] = session.PresentUsers;
                session.RegisterUserPresent(user);
                _sessionRepository.SaveChanges();

            } catch (Exception e)
            {
                throw e;
            }
            return RedirectToAction("Aanwezigen", "Session", new { id = sessionid });
        }

        public IActionResult RemoveUserPresent(string id, int sessionid)
        {
            try
            {
                Session session = _sessionRepository.GetById(sessionid);
                User user = _userRepository.GetById(id);
                if (user == null || session.PresentUsers == null)
                {
                    return NotFound();
                }
                ViewData["presentusers"] = session.PresentUsers;
                session.RegisterUserPresent(user);
                session.RemoveUserPresent(user);
                _sessionRepository.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
            return RedirectToAction("Aanwezigen", "Session", new { id = sessionid });
        }

    }
}