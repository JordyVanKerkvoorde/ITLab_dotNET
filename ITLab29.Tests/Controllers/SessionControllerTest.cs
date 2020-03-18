using ITLab29.Controllers;
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

        public SessionControllerTest()
        {

            _dummyContext = new DummyApplicationDbContext();
            _mockSessionRepository = new Mock<ISessionRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            user1 = _dummyContext.Users.ElementAt(0);

            _sessionController = new SessionController(_mockSessionRepository.Object, _mockUserRepository.Object);

        }


        [Fact]
        public void Index_PassesAllSessionInOrderTest()
        {
            _mockSessionRepository.Setup(s => s.GetAll()).Returns(_dummyContext.Sessions);
            var result = Assert.IsType<ViewResult>(_sessionController.Index(null));
            List<Session> sessions = Assert.IsType<List<Session>>(result.Model);
            Assert.Equal(5, sessions.Count);
            Assert.Equal("What I Wish I Had Known Before Scaling Uber to 1000 Services", sessions.ElementAt(0).Title);
            Assert.Equal("Life is Terrible: Let’s Talk About the Web", sessions.ElementAt(1).Title);
            Assert.Equal("De weg naar de Cloud, hoe doen bedrijven dat nu eigenlijk?", sessions.ElementAt(2).Title);
            Assert.Equal("Power Use of UNIX - Dan North", sessions.ElementAt(3).Title);
            Assert.Equal("TDD, Where Did It All Go Wrong", sessions.ElementAt(4).Title);
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
            FeedBackViewModel feedbackViewModel = Assert.IsType<FeedBackViewModel>(result.Model);
            Session session = feedbackViewModel.Session;
            Assert.Equal(title, session.Title);
        }


        [Theory]
        [InlineData(1234)]
        [InlineData(12345)]
        public void Details_SessionNotFound_ReturnsNotFound(int id)
        {
            _mockSessionRepository.Setup(s => s.GetById(id)).Throws(new ArgumentNullException());
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
            _mockSessionRepository.Setup(s => s.GetById(id)).Throws(new ArgumentNullException());
            Assert.IsType<NotFoundResult>(_sessionController.Add(id, user1));

        }



        //[Theory]
        //[InlineData(1)]
        //[InlineData(2)]
        //public void Delete_SessionFound_DeletesUserFromSession(int id)
        //{
        //    _mockSessionRepository.Setup(s => s.GetById(id)).Returns(_dummyContext.Sessions.First(s => s.SessionId == id));
        //    _sessionController.Add(id, user1);
        //    Session session = _dummyContext.Sessions.First(s => s.SessionId == id);
        //    Assert.Contains(user1, session.GetUsers());
        //    var result = _sessionController.Delete(id, user1) as ViewResult;
        //    Assert.DoesNotContain(user1, session.GetUsers());
        //    _mockUserRepository.Verify(m => m.SaveChanges(), Times.Exactly(2));

        //}

        [Theory]
        [InlineData(1234)]
        [InlineData(12345)]
        public void Delete_SessionNotFound_ReturnsNotFound(int id)
        {
            _mockSessionRepository.Setup(s => s.GetById(id)).Throws(new ArgumentNullException());
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
            _mockSessionRepository.Setup(s => s.GetById(feedback.id)).Throws(new ArgumentNullException());
            Assert.IsType<NotFoundResult>(_sessionController.AddFeedback(feedback));
        }


    }
}
