using System;
using Xunit;
using ITLab29.Models.Domain;

namespace ITLab29.Tests.Models.Domain {
    public class GuestTest {

        private readonly Guest _guest;
        private readonly int _guestId;
        private readonly string _name;
        private readonly string _email;
        private readonly string _phonenumber;
        
        public GuestTest()
        {
            _guestId = 12345;
            _name = "Donald Duck";
            _email = "donald@duck.com";
            _phonenumber = "007005002";
            _guest = new Guest(_guestId, _name, _email, _phonenumber);
        }

        [Fact]
        public void NewGuest_SetProperties()
        {
            Assert.Equal(_guestId, _guest.GuestId);
            Assert.Equal(_name, _guest.Name);
            Assert.Equal(_email, _guest.Email);
            Assert.Equal(_phonenumber, _guest.PhoneNumber);
        }

        [Fact]
        public void AddEvent()
        {
            Event ev = _guest.AddEvent(1, "title", "desc", new User("01e", "Jan", "Willem", UserType.USER, UserStatus.ACTIVE, "mail@mail.be"), 
                new DateTime(), new DateTime(), 25, new Location("loc1", CampusEnum.SCHOONMEERSEN, 30));
            Assert.Equal(1, _guest.Events.Count);
            Assert.True(_guest.Events.Contains(ev));
            Assert.Throws<ArgumentException>(
                () => _guest.AddEvent(1, "title", "desc", new User("01e", "Jan", "Willem", UserType.USER, UserStatus.ACTIVE, "mail@mail.be"),
                new DateTime(), new DateTime(), 25, new Location("loc1", CampusEnum.SCHOONMEERSEN, 30)));
            Assert.Equal(1, _guest.Events.Count);
        }
          
    }
}
