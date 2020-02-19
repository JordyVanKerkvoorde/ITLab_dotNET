using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ITLab29.Models;
using ITLab29.Models.Domain;

namespace ITLab29.Tests.Models.Domain {
    public class UserTest {
        private User _user;
        private Event _evt;

        public UserTest() {
            _user = new User("01e", "Jan", "Willem", UserType.USER, UserStatus.ACTIVE, "mail@mail.be");
           // _evt = new Event("title", "desc", _user, new DateTime(), new DateTime(), 25, new Location("loc1", CampusEnum.SCHOONMEERSEN, 30));
        }

        [Fact]
        public void TestAddAttendee() {
            _user.AddEvent(1, "title", "desc", _user, new DateTime(), new DateTime(), 25, new Location("loc1", CampusEnum.SCHOONMEERSEN, 30));
            Assert.Equal(1, _user.Events.Count);
        }

    }
}
