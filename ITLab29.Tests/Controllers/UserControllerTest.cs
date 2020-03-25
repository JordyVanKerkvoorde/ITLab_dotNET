using ITLab29.Controllers;
using ITLab29.Models.Domain;
using ITLab29.Tests.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ITLab29.Tests.Controllers
{
    public class UserControllerTest
    {

        private DummyApplicationDbContext _dummyContext;
        private UserController _userController;
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IMediaRepository> _mockMediaRepository;
        private User user1 = new User("1234", "Peter", "Jansens", UserType.USER, UserStatus.ACTIVE)
        { UserName = "peter.jansens@student.hogent.be", Email = "peter.jansens@student.hogent.be", EmailConfirmed = true };

        public UserControllerTest()
        {
            _dummyContext = new DummyApplicationDbContext();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockMediaRepository = new Mock<IMediaRepository>();

            _userController = new UserController(_mockUserRepository.Object, _mockMediaRepository.Object);
        }


        //[Fact]
        //public void Index()
        //{
        //    var result = Assert.IsType<ViewResult>(_userController.Index(user1));
        //}

    }

}
