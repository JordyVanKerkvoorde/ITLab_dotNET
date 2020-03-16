using ITLab29.Models.Domain;
using System;
using Xunit;

namespace ITLab29.Tests.Models.Domain
{
    public class UserTest
    {
        private User _user;
        private User _user2;
        private Session _evt;

        private string Id = "0s5";
        private string FName = "Jordy";
        private string LName = "VK";
        private string Mail = "test@test.be";

        public UserTest()
        {
            _user = new User("01e", "Jan", "Willem", UserType.USER, UserStatus.ACTIVE);
            _user2 = new User("01e", "Jan", "Willem", UserType.USER, UserStatus.BLOCKED);
            // _evt = new Event("title", "desc", _user, new DateTime(), new DateTime(), 25, new Location("loc1", CampusEnum.SCHOONMEERSEN, 30));
        }


        [Fact]
        public void IsActiveTest()
        {
            Assert.True(_user.IsActive());
            Assert.False(_user2.IsActive());
        }


        [Fact]
        public void ConstructorExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(() => new User(null, FName, LName, UserType.ADMIN, UserStatus.ACTIVE));
            Assert.Throws<ArgumentNullException>(() => new User(Id, null, LName, UserType.ADMIN, UserStatus.ACTIVE));
            Assert.Throws<ArgumentNullException>(() => new User(Id, FName, null, UserType.ADMIN, UserStatus.ACTIVE));
        }

        [Fact]
        public void isRegistered()
        {

        }
    }
}

