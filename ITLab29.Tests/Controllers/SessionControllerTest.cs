﻿using ITLab29.Controllers;
using ITLab29.Exceptions;
using ITLab29.Models.Domain;
using ITLab29.Models.ViewModels;
using ITLab29.Tests.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ITLab29.Tests.Controllers
{
    public class SessionControllerTest
    {
        private DummyApplicationDbContext _dummyContext;
        private SessionController _sessionController;
        private Mock<ISessionRepository> _mockSessionRepository;
        private Mock<IUserRepository> _mockUserRepository;
        private readonly  User user1;
        private readonly User user2;
        private readonly User user3;

        public SessionControllerTest()
        {

            _dummyContext = new DummyApplicationDbContext();
            _mockSessionRepository = new Mock<ISessionRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            user1 = _dummyContext.Users.ElementAt(0); //Admin
            user2 = _dummyContext.Users.ElementAt(2); //Responsible
            user3 = _dummyContext.Users.ElementAt(3); //User

            _sessionController = new SessionController(_mockSessionRepository.Object, _mockUserRepository.Object);

        }


        [Fact]
        public void Index_PassesAllSessionInOrderTest()
        {
            _mockSessionRepository.Setup(s => s.GetAll()).Returns(_dummyContext.Sessions);
            var result = Assert.IsType<ViewResult>(_sessionController.Index());
            List<Session> sessions = Assert.IsType<List<Session>>(result.Model);
            Assert.Equal(5, sessions.Count);
            Assert.Equal("What I Wish I Had Known Before Scaling Uber to 1000 Services", sessions.ElementAt(0).Title);
            Assert.Equal("Life is Terrible: Let’s Talk About the Web", sessions.ElementAt(1).Title);
            Assert.Equal("De weg naar de Cloud, hoe doen bedrijven dat nu eigenlijk?", sessions.ElementAt(2).Title);
            Assert.Equal("Power Use of UNIX - Dan North", sessions.ElementAt(3).Title);
            Assert.Equal("TDD, Where Did It All Go Wrong", sessions.ElementAt(4).Title);
        }

        [Fact]
        public void Index_NoSessions()
        {
            _mockSessionRepository.Setup(s => s.GetAll()).Throws(new EmptyListException(""));
            var result = Assert.IsType<ViewResult>(_sessionController.Index());
            List<Session> sessions = Assert.IsType<List<Session>>(result.Model);
            Assert.Empty(sessions);
        }

        [Fact]
        public void Get_PassesAllSessionInOrderTest()
        {
            _mockSessionRepository.Setup(s => s.GetAll()).Returns(_dummyContext.Sessions);
            var result = Assert.IsType<OkObjectResult>(_sessionController.Get());
            List<Session> sessions = Assert.IsType<List<Session>>(result.Value);
            Assert.Equal(5, sessions.Count);
            Assert.Equal("What I Wish I Had Known Before Scaling Uber to 1000 Services", sessions.ElementAt(0).Title);
            Assert.Equal("Life is Terrible: Let’s Talk About the Web", sessions.ElementAt(1).Title);
            Assert.Equal("De weg naar de Cloud, hoe doen bedrijven dat nu eigenlijk?", sessions.ElementAt(2).Title);
            Assert.Equal("Power Use of UNIX - Dan North", sessions.ElementAt(3).Title);
            Assert.Equal("TDD, Where Did It All Go Wrong", sessions.ElementAt(4).Title);
        }

        [Fact]
        public void Get_NoSessions()
        {
            _mockSessionRepository.Setup(s => s.GetAll()).Throws(new EmptyListException(""));
            var result = Assert.IsType<OkObjectResult>(_sessionController.Get());
            List<Session> sessions = Assert.IsType<List<Session>>(result.Value);
            Assert.Empty(sessions);
        }

        [Theory]
        [InlineData(1, "Power Use of UNIX - Dan North")]
        [InlineData(2, "TDD, Where Did It All Go Wrong")]
        [InlineData(3, "De weg naar de Cloud, hoe doen bedrijven dat nu eigenlijk?")]
        [InlineData(4, "Life is Terrible: Let’s Talk About the Web")]
        [InlineData(5, "What I Wish I Had Known Before Scaling Uber to 1000 Services")]
        public void Details_ShowsCorrectSession(int id, string title)
        {
            _mockSessionRepository.Setup(s => s.GetById(id)).Returns(_dummyContext.Sessions.First(s => s.SessionId == id));
            //Session session = _dummyContext.Sessions.Where(s => s.SessionId == id).First();
            var result = Assert.IsType<ViewResult>(_sessionController.Details(id, user1));
            Tuple<FeedBackViewModel, EventDetailsViewModel> result2 = result.Model as Tuple<FeedBackViewModel, EventDetailsViewModel>;
            EventDetailsViewModel eventDetailsViewModel = Assert.IsType<EventDetailsViewModel>(result2.Item2);
            Session session = eventDetailsViewModel.Session;
            Assert.Equal(title, session.Title);
            Assert.Equal(id, session.SessionId);
        }


        [Theory]
        [InlineData(1234)]
        [InlineData(12345)]
        public void Details_SessionNotFound_ReturnsNotFound(int id)
        {
            _mockSessionRepository.Setup(s => s.GetById(id)).Throws(new SessionNotFoundException(""));
            Assert.IsType<NotFoundResult>(_sessionController.Details(id, user1));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Add_SessionFound_AddsUserToSession(int id)
        {
            _mockSessionRepository.Setup(s => s.GetById(id)).Returns(_dummyContext.Sessions.First(s => s.SessionId == id));
            var result = Assert.IsType<RedirectToActionResult>(_sessionController.Add(id, user1));
            Assert.Equal("Details", result?.ActionName);
            _mockUserRepository.Verify(m => m.SaveChanges(), Times.Once);
            Session session = _dummyContext.Sessions.First(s => s.SessionId == id);
            Assert.Contains(user1, session.UserSessions.Select(us => us.User));

        }

        [Theory]
        [InlineData(1234)]
        [InlineData(12345)]
        public void Add_SessionNotFound_ReturnsNotFound(int id)
        {
            _mockSessionRepository.Setup(s => s.GetById(id)).Throws(new SessionNotFoundException(""));
            Assert.IsType<NotFoundResult>(_sessionController.Add(id, user1));

        }



        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Delete_SessionFound_DeletesUserFromSession(int id)
        {
            Session session = _dummyContext.Sessions.First(s => s.SessionId == id);
            _mockSessionRepository.Setup(s => s.GetById(id)).Returns(session);
            // user1 is dummyUser uit dummyDBContext en is op einde daar al toegevoegd aan sessie5 (met id 1)
           _sessionController.Add(id, user1);
            Assert.Contains(user1, session.GetUsers());
            _sessionController.Delete(id, user1);
            Assert.DoesNotContain(user1, session.GetUsers());
            _mockUserRepository.Verify(m => m.SaveChanges(), Times.Exactly(2));

        }

        [Theory]
        [InlineData(1234)]
        [InlineData(12345)]
        public void Delete_SessionNotFound_ReturnsNotFound(int id)
        {
            _mockSessionRepository.Setup(s => s.GetById(id)).Throws(new SessionNotFoundException(""));
            Assert.IsType<NotFoundResult>(_sessionController.Delete(id, user1));

        }

        [Fact]
        public void AddFeedback_SessionFound_AddsFeedbacktoSession()
        {
            var feedback = new FeedBackViewModel() { id = 1, Score = 4, Description = "testet" };
            _mockSessionRepository.Setup(s => s.GetById(feedback.id)).Returns(_dummyContext.Sessions.First(s => s.SessionId == feedback.id));
            var result = Assert.IsType<RedirectToActionResult>(_sessionController.AddFeedback(feedback));
            Assert.Equal("Details", result?.ActionName);
            Session session = _dummyContext.Sessions.First(s => s.SessionId == feedback.id);
            session.Feedback.Any(fb => fb.Description == feedback.Description && fb.Score == feedback.Score);
            _mockSessionRepository.Verify(m => m.SaveChanges(), Times.Once);
        }


        [Fact]
        public void AddFeedback_SessionNotFound_ReturnsNotFound()
        {
            var feedback = new FeedBackViewModel() { id = 1234, Score = 4, Description = "testet" };
            _mockSessionRepository.Setup(s => s.GetById(feedback.id)).Throws(new SessionNotFoundException(""));
            Assert.IsType<NotFoundResult>(_sessionController.AddFeedback(feedback));
        }

        [Fact]
        public void AddFeedback_UserNotFound_ReturnsNotFound()
        {
            var feedback = new FeedBackViewModel() { id = 1234, Score = 4, Description = "testet" };
            _mockUserRepository.Setup(u => u.GetById(feedback.UserId)).Throws(new UserNotFoundException(""));
            Assert.IsType<NotFoundResult>(_sessionController.AddFeedback(feedback));
        }


        [Fact]
        public void OpenSessionsAsAdmin_ReturnsAllOpenableSessionsInOrder()
        {
            _mockSessionRepository.Setup(s => s.GetOpenableSessionsAsAdmin()).Returns(_dummyContext.AdminSessions);
            var result = Assert.IsType<ViewResult>(_sessionController.OpenSessions(user1));
            List<Session> adminsessions = Assert.IsType<List<Session>>(result.Model);
            Assert.Equal(2, adminsessions.Count);
            Assert.Equal("Life is Terrible: Let’s Talk About the Web", adminsessions.ElementAt(0).Title);
            Assert.Equal("De weg naar de Cloud, hoe doen bedrijven dat nu eigenlijk?", adminsessions.ElementAt(1).Title);
        }

        [Fact]
        public void OpenSessionsAsResponsible_ReturnsResponsibleOpenableSessions()
        {
            _mockSessionRepository.Setup(s => s.GetOpenableSessions(user2.Id)).Returns(_dummyContext.ResponsibleSessions);
            var result = Assert.IsType<ViewResult>(_sessionController.OpenSessions(user2));
            List<Session> responsiblesessions = Assert.IsType<List<Session>>(result.Model);
            Assert.Equal(1, responsiblesessions.Count);
            Assert.Equal("De weg naar de Cloud, hoe doen bedrijven dat nu eigenlijk?", responsiblesessions.ElementAt(0).Title);
        }

        [Fact]
        public void OpenSessionsAsUser_ReturnsNoOpenableSessions()
        {
            _mockSessionRepository.Setup(s => s.GetOpenableSessions(user3.Id)).Throws(new EmptyListException(""));
            var result = Assert.IsType<ViewResult>(_sessionController.OpenSessions(user3));
            List<Session> sessions = Assert.IsType<List<Session>>(result.Model);
            Assert.Empty(sessions);
        }


        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        public void OpenSession(int id)
        {
            Session session = _dummyContext.Sessions.First(s => s.SessionId == id);
            _mockSessionRepository.Setup(s => s.GetById(id)).Returns(session);
            var result = Assert.IsType<RedirectToActionResult>(_sessionController.OpenSession(id));
            Assert.True(session.IsOpened);
            _mockSessionRepository.Verify(m => m.SaveChanges(), Times.Once);

        }

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        public void CloseSession(int id)
        {
            Session session = _dummyContext.Sessions.First(s => s.SessionId == id);
            _mockSessionRepository.Setup(s => s.GetById(id)).Returns(session);
            _sessionController.OpenSession(id);
            Assert.True(session.IsOpened);
            var result = Assert.IsType<RedirectToActionResult>(_sessionController.CloseSession(id));
            Assert.False(session.IsOpened);
            _mockSessionRepository.Verify(m => m.SaveChanges(), Times.Exactly(2));
        }

        [Fact]
        public void Aanwezigen_ReturnsAllAlphabeticallyOnLastName()
        {
            Session session = _dummyContext.Sessions.First(s => s.SessionId == 3);
            _mockSessionRepository.Setup(s => s.GetById(3)).Returns(session);
            var result = Assert.IsType<ViewResult>(_sessionController.Aanwezigen(3));
            List<User> users = Assert.IsType<List<User>>(result.Model);
            Assert.Equal(3, users.Count);
            Assert.Equal("Van De Woestyne", users.ElementAt(0).LastName);
            Assert.Equal("Van Kerkvoorde", users.ElementAt(1).LastName);
            Assert.Equal("Willem", users.ElementAt(2).LastName);
        }

        [Fact]
        public void Aanwezigen_SessionNotFound()
        {
            _mockSessionRepository.Setup(s => s.GetById(3)).Throws(new SessionNotFoundException(""));
            Assert.IsType<NotFoundResult>(_sessionController.Aanwezigen(3));
        }

        [Fact]
        public void SetUserPresent()
        {
            Session session = _dummyContext.Sessions.First(s => s.SessionId == 3);
            _mockSessionRepository.Setup(s => s.GetById(3)).Returns(session);
            User user = _dummyContext.Users.ElementAt(0);
            _mockUserRepository.Setup(u => u.GetById("123213jw")).Returns(user);
            var result = Assert.IsType<RedirectToActionResult>(_sessionController.SetUserPresent("123213jw", 3));
            Assert.Contains(user, session.PresentUsers);
        }

        [Fact]
        public void RemoveUserPresent()
        {
            Session session = _dummyContext.Sessions.First(s => s.SessionId == 3);
            _mockSessionRepository.Setup(s => s.GetById(3)).Returns(session);
            User user = _dummyContext.Users.ElementAt(0);
            _mockUserRepository.Setup(u => u.GetById("123213jw")).Returns(user);
            _sessionController.SetUserPresent("123213jw", 3);
            Assert.Contains(user, session.PresentUsers);
            var result = Assert.IsType<RedirectToActionResult>(_sessionController.RemoveUserPresent("123213jw", 3));
            Assert.DoesNotContain(user, session.PresentUsers);
            
        }




    }
}
