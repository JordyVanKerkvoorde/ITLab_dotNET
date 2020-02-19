using ITLab29.Models.Domain;
using System;
using Xunit;

namespace ITLab29.Tests.Models.Domain {
    public class UserTest {
        private User _user;
        private User _user2;
        private Event _evt;

        private string Id = "0s5";
        private string FName = "Jordy";
        private string LName = "VK";
        private string Mail = "test@test.be";

        public UserTest() {
            _user = new User("01e", "Jan", "Willem", UserType.USER, UserStatus.ACTIVE, "mail@mail.be");
            _user2 = new User("01e", "Jan", "Willem", UserType.USER, UserStatus.BLOCKED, "mail@mail.be");
            // _evt = new Event("title", "desc", _user, new DateTime(), new DateTime(), 25, new Location("loc1", CampusEnum.SCHOONMEERSEN, 30));
        }

        [Fact]
        public void AddEventTest() {
            _user.AddEvent(1, "title", "desc", _user, new DateTime(), new DateTime(), 25, new Location("loc1", CampusEnum.SCHOONMEERSEN, 30));
            Assert.Equal(1, _user.Events.Count);
            Event evtTest = _user2.AddEvent(10, "title", "desc", _user, new DateTime(), new DateTime(), 25, new Location("loc1", CampusEnum.SCHOONMEERSEN, 30));
            Assert.True(_user2.Events.Contains(evtTest));
            Assert.Throws<ArgumentException>(() => _user.AddEvent(1, "title", "desc", _user, new DateTime(), new DateTime(), 25, new Location("loc1", CampusEnum.SCHOONMEERSEN, 30)));
        }

        [Fact]
        public void IsActiveTest() {
            Assert.True(_user.IsActive());
            Assert.False(_user2.IsActive());
        }

        [Fact]
        public void SetProperties() {
            User testUsr = new User(Id, FName, LName, UserType.ADMIN, UserStatus.ACTIVE, Mail);
            Assert.Equal(Id, testUsr.UserId);
            Assert.Equal(FName, testUsr.FirstName);
            Assert.Equal(LName, testUsr.LastName);
            Assert.Equal(Mail, testUsr.Email);
        }

        [Fact]
        public void ConstructorExceptionTest() {
            Assert.Throws<ArgumentNullException>(() => new User(null, FName, LName, UserType.ADMIN, UserStatus.ACTIVE, Mail));
            Assert.Throws<ArgumentNullException>(() => new User(Id, null, LName, UserType.ADMIN, UserStatus.ACTIVE, Mail));
            Assert.Throws<ArgumentNullException>(() => new User(Id, FName, null, UserType.ADMIN, UserStatus.ACTIVE, Mail));
            Assert.Throws<ArgumentNullException>(() => new User(Id, FName, LName, UserType.ADMIN, UserStatus.ACTIVE, null));
        }
    }
}

