using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ITLab29.Models;
using ITLab29.Models.Domain;

namespace ITLab29.Tests.Models.Domain {
    public class SessionTest {

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
            _responsible = new User("123456ab", "Jan", "Willem", UserType.ADMIN, UserStatus.ACTIVE, "jan@hogent.be");
            _start = new DateTime(2020, 2, 19, 18, 00, 00);
            _end = new DateTime(2020, 2, 19, 19, 00, 00);
            _capacity = 100;
            _location = new Location("GSCHB.4.013", CampusEnum.SCHOONMEERSEN, 120);

            _session = new Session(_sessionId, _title, _description, _responsible, _start, _end, _capacity, _location);
        }

        [Fact]
        public void NewEvent_SetProperties()
        {
            Assert.Equal(_sessionId, _session.SessionId);
            Assert.Equal(_title, _session.Title);
            Assert.Equal(_description, _session.Description);
            Assert.Equal(_responsible, _session.Responsible);
            Assert.Equal(_start, _session.Start);
            Assert.Equal(_end, _session.End);
            Assert.Equal(_capacity, _session.Capacity);
            Assert.Equal(_location, _session.Location);
            Assert.Equal(0, _session.Attendees.Count);
            Assert.Equal(0, _session.Guests.Count);
            Assert.Equal(0, _session.Media.Count);
            Assert.Equal(0, _session.Feedback.Count);
        }

        [Fact]
        public void NewEvent_Null()
        {
            Assert.Throws<ArgumentException>(
                () => new Session(_sessionId, null, _description, _responsible, _start, _end, _capacity, _location));
            Assert.Throws<ArgumentException>(
                () => new Session(_sessionId, _title, null, _responsible, _start, _end, _capacity, _location));
            Assert.Throws<ArgumentException>(
                () => new Session(_sessionId, _title, _description, null, _start, _end, _capacity, _location));
            Assert.Throws<ArgumentException>(
                () => new Session(_sessionId, _title, _description, _responsible, _start, _end, _capacity, null));
            Assert.Throws<ArgumentException>(
                () => new Session(_sessionId, _title, _description, _responsible, _start, _end, 0, _location));
        }

        [Fact]
        public void AddAttendee()
        {
            User attendee = _session.AddAttendee("123456cd", "Jordy", "Van Kerkvoorde", UserType.USER, UserStatus.ACTIVE, "jordy@hogent.be");
            Assert.Equal(1, _session.Attendees.Count);
            Assert.True(_session.Attendees.Contains(attendee));
            Assert.Throws<ArgumentException>(
                () => _session.AddAttendee("123456cd", "Jordy", "Van Kerkvoorde", UserType.USER, UserStatus.ACTIVE, "jordy@hogent.be"));
            Assert.Equal(1, _session.Attendees.Count);
        }

        [Fact]
        public void AddMedia()
        {
            Media media = _session.AddMedia(123, MediaType.IMAGE, "/here/is/a/photo");
            Assert.Equal(1, _session.Media.Count);
            Assert.True(_session.Media.Contains(media));
            Assert.Throws<ArgumentException>(
                () => _session.AddMedia(123, MediaType.IMAGE, "/here/is/a/photo"));
            Assert.Equal(1, _session.Media.Count);
        }

        [Fact]
        public void AddGuest()
        {
            Guest guest = _session.AddGuest(1234, "Super Mario", "super@mario.be", "123456789");
            Assert.Equal(1, _session.Guests.Count);
            Assert.True(_session.Guests.Contains(guest));
            Assert.Throws<ArgumentException>(
                () => _session.AddGuest(1234, "Super Mario", "super@mario.be", "123456789"));
            Assert.Equal(1, _session.Guests.Count);
        }

        [Fact]
        public void AddFeedback()
        {
            Feedback feedback = _session.AddFeedback(12, 5, 
                new User("123456cd", "Jordy", "Van Kerkvoorde", UserType.USER, UserStatus.ACTIVE, "jordy@hogent.be"), 
                "Almost done testing this class");
            Assert.Equal(1, _session.Feedback.Count);
            Assert.True(_session.Feedback.Contains(feedback));
            Assert.Throws<ArgumentException>(
                () => _session.AddFeedback(12, 5,
                new User("123456cd", "Jordy", "Van Kerkvoorde", UserType.USER, UserStatus.ACTIVE, "jordy@hogent.be"),
                "Almost done testing this class"));
            Assert.Equal(1, _session.Feedback.Count);
        }
    }
}
