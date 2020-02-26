using ITLab29.Controllers;
using ITLab29.Models.Domain;
using ITLab29.Tests.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ITLab29.Tests.Controllers
{
    public class SessionControllerTest
    {
        private UserManager<User> _userManager;
        private DummyApplicationDbContext _dummyContext;
        private SessionController _sessionController;
        private Mock<ISessionRepository> _mockSessionRepository;
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IUserSessionRepository> _mockUserSessionRepository;

        public SessionControllerTest()
        {
            _userManager = new Mock<UserManager<User>>(
                    new Mock<IUserStore<User>>().Object,
                    new Mock<IOptions<IdentityOptions>>().Object,
                    new Mock<IPasswordHasher<User>>().Object,
                    new IUserValidator<User>[0],
                    new IPasswordValidator<User>[0],
                    new Mock<ILookupNormalizer>().Object,
                    new Mock<IdentityErrorDescriber>().Object,
                    new Mock<IServiceProvider>().Object,
                    new Mock<ILogger<User>>().Object).Object;

            _dummyContext = new DummyApplicationDbContext(_userManager);
            _mockSessionRepository = new Mock<ISessionRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockUserSessionRepository = new Mock<IUserSessionRepository>();

            _sessionController = new SessionController(_mockSessionRepository.Object, _mockUserRepository.Object, _userManager, _mockUserSessionRepository.Object);

        }


        [Fact]
        public void Index_PassesAllSessionInOrder()
        {
            _mockSessionRepository.Setup(s => s.GetAll()).Returns(_dummyContext.Sessions);
            var result = Assert.IsType<ViewResult>(_sessionController.Index(DateTime.Now));
            List<Session> sessions = Assert.IsType<List<Session>>(result.Model);
            Assert.Equal(5, sessions.Count);
        }

    }
}
