﻿using System;
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
        public IActionResult Details(int id) {
            Session session = _sessionRepository.GetById(id);
            if (session == null) {
                return NotFound();
            }
            return View(session);
        }

        [HttpPost]
        public IActionResult Add(string userId, int sessionId) {
            
            User user = _userRepository.GetById(userId);
            Session session = _sessionRepository.GetById(sessionId);
            if (session.UserSessions.Count() < session.Capacity || user.UserStatus != UserStatus.BLOCKED) {
                //insert code
            } else { 
                //TODO implementation pop up/error
            }
            UserSession us = new UserSession {
                Session = session,
                User = user,
                SessionId = session.SessionId,
                UserId = user.Id
            };
            ViewData["userId"] = userId;
            ViewData["sessionId"] = sessionId;
            return RedirectToAction("Index");
        }
    }
}