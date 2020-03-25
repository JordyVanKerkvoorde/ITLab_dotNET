using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ITLab29.Models;
using ITLab29.Models.Domain;

namespace ITLab29.Tests.Models.Domain
{
    public class SessionTest
    {

        private readonly Session _session;
        private readonly int _sessionId;
        private readonly string _title;
        private readonly string _description;
        private readonly User _responsible;
        private readonly DateTime _start;
        private readonly DateTime _end;
        private readonly int _capacity;
        private readonly Location _location;


        public SessionTest()
        {
            _sessionId = 14;
            _title = "Welcome to testing.";
            _description = "Today we will test the Event class.";
            _responsible = new User("123456ab", "Jan", "Willem", UserType.ADMIN, UserStatus.ACTIVE);
            _start = new DateTime(2020, 2, 19, 18, 00, 00);
            _end = new DateTime(2020, 2, 19, 19, 00, 00);
            _capacity = 100;
            _location = new Location("GSCHB.4.013", CampusEnum.SCHOONMEERSEN, 120);

            _session = new Session(_title, _description, _responsible, _start, _end, _capacity, _location);
        }

       

        [Fact]
        public void AddMedia()
        {
            Media media = _session.AddMedia(MediaType.IMAGE, "/here/is/a/photo");
            Assert.Equal(1, _session.Media.Count);
            Assert.True(_session.Media.Contains(media));
        }

        [Fact]
        public void AddGuest()
        {
            Guest guest = _session.AddGuest("Super Mario", "super@mario.be", "123456789");
            Assert.Equal(1, _session.Guests.Count);
            Assert.True(_session.Guests.Contains(guest));
            Assert.Throws<ArgumentException>(
                () => _session.AddGuest("Super Mario", "super@mario.be", "123456789"));
            Assert.Equal(1, _session.Guests.Count);
        }

        [Fact]
        public void AddFeedback()
        {
            User dummyUser = new User("88888", "Yorick", "Van de Woestyne", UserType.ADMIN, UserStatus.ACTIVE)
            { UserName = "yorick.vandewoestyne@student.hogent.be", Email = "yorick.vandewoestyne@student.hogent.be", EmailConfirmed = true };
            Feedback feedback = new Feedback(5, "Almost done testing this class", dummyUser);
            _session.AddFeedback(feedback);
            Assert.Equal(1, _session.Feedback.Count);
            Assert.True(_session.Feedback.Contains(feedback));
        }

        [Fact]
        public void AddUserSession_RemoveUserSession()
        {
            User dummyUser = new User("88888", "Yorick", "Van de Woestyne", UserType.ADMIN, UserStatus.ACTIVE)
            { UserName = "yorick.vandewoestyne@student.hogent.be", Email = "yorick.vandewoestyne@student.hogent.be", EmailConfirmed = true };
            User dummyUser2 = new User("11111", "Jan", "Willem", UserType.USER, UserStatus.ACTIVE)
            { UserName = "jan.willem@student.hogent.be", Email = "jan.willem@student.hogent.be", EmailConfirmed = true };
            User dummyUser3 = new User("12345", "Sander", "Machado", UserType.RESPONSIBLE, UserStatus.BLOCKED)
            { UserName = "sander.castanheiramachado@student.hogent.be", Email = "sander.castanheiramachado@student.hogent.be", EmailConfirmed = true };
            User dummyUser4 = new User("00200", "Jordy", "Van Kerkvoorde", UserType.RESPONSIBLE, UserStatus.ACTIVE)
            { UserName = "jordy.vankerkvoorde@student.hogent.be", Email = "jordy.vankerkvoorde@student.hogent.be", EmailConfirmed = true };
            Session session = new Session("What I Wish I Had Known Before Scaling Uber to 1000 Services",
                    "Matt Ranney, Senior Staff Engineer bij Uber, vertelt over zijn ervaringen met microservices bij Uber: \"To Keep up with Uber's growth, we've embraced microservices in a big way.This has led to an explosion of new services, crossing over 1, 000 production services in early March 2016.Along the way we've learned a lot, and if we had to do it all over again, we'd do some things differently.If you are earlier along on your personal microservices journey than we are, then this talk may save you from having to learn some things learn the hard way. \"",
                        dummyUser, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-5).AddHours(2), 2, new Location("GSCHB1.420", CampusEnum.SCHOONMEERSEN, 100));
            Assert.Throws<Exception>(() => session.AddUserSession(dummyUser3));
            session.AddUserSession(dummyUser);
            Assert.Equal(1, session.GetUsers().Count);
            session.AddUserSession(dummyUser2);
            Assert.Equal(2, session.GetUsers().Count);
            Assert.Throws<Exception>(() => session.AddUserSession(dummyUser4));
            session.RemoveUserSession(dummyUser);
            Assert.Equal(1, session.GetUsers().Count);
            Assert.Throws<Exception>(() => session.RemoveUserSession(dummyUser));
            Assert.Equal(1, session.GetUsers().Count);


        }

    }
}
