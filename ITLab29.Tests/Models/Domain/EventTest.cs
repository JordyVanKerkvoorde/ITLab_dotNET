using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ITLab29.Models;
using ITLab29.Models.Domain;

namespace ITLab29.Tests.Models.Domain {
    public class EventTest {

        private readonly Event _event;
        private readonly int _eventId;
        private readonly string _title;
        private readonly string _description;
        private readonly User _responsible;
        private readonly DateTime _start;
        private readonly DateTime _end;
        private readonly int _capacity;
        private readonly Location _location;


        public EventTest()
        {
            _eventId = 14;
            _title = "Welcome to testing.";
            _description = "Today we will test the Event class.";
            _responsible = new User("123456ab", "Jan", "Willem", UserType.ADMIN, UserStatus.ACTIVE, "jan@hogent.be");
            _start = new DateTime(2020, 2, 19, 18, 00, 00);
            _end = new DateTime(2020, 2, 19, 19, 00, 00);
            _capacity = 100;
            _location = new Location("GSCHB.4.013", CampusEnum.SCHOONMEERSEN, 120);

            _event = new Event(_eventId, _title, _description, _responsible, _start, _end, _capacity, _location);
        }

        [Fact]
        public void NewEvent_SetProperties()
        {
            Assert.Equal(_eventId, _event.EventId);
            Assert.Equal(_title, _event.Title);
            Assert.Equal(_description, _event.Description);
            Assert.Equal(_responsible, _event.Responsible);
            Assert.Equal(_start, _event.Start);
            Assert.Equal(_end, _event.End);
            Assert.Equal(_capacity, _event.Capacity);
            Assert.Equal(_location, _event.Location);
            Assert.Equal(0, _event.Attendees.Count);
            Assert.Equal(0, _event.Guests.Count);
            Assert.Equal(0, _event.Media.Count);
            Assert.Equal(0, _event.Feedback.Count);
        }

        [Fact]
        public void NewEvent_Null()
        {
            Assert.Throws<ArgumentException>(
                () => new Event(_eventId, null, _description, _responsible, _start, _end, _capacity, _location));
            Assert.Throws<ArgumentException>(
                () => new Event(_eventId, _title, null, _responsible, _start, _end, _capacity, _location));
            Assert.Throws<ArgumentException>(
                () => new Event(_eventId, _title, _description, null, _start, _end, _capacity, _location));
            Assert.Throws<ArgumentException>(
                () => new Event(_eventId, _title, _description, _responsible, _start, _end, _capacity, null));
            Assert.Throws<ArgumentException>(
                () => new Event(_eventId, _title, _description, _responsible, _start, _end, 0, _location));
        }

        [Fact]
        public void AddAttendee()
        {
            User attendee = _event.AddAttendee("123456cd", "Jordy", "Van Kerkvoorde", UserType.USER, UserStatus.ACTIVE, "jordy@hogent.be");
            Assert.Equal(1, _event.Attendees.Count);
            Assert.True(_event.Attendees.Contains(attendee));
            Assert.Throws<ArgumentException>(
                () => _event.AddAttendee("123456cd", "Jordy", "Van Kerkvoorde", UserType.USER, UserStatus.ACTIVE, "jordy@hogent.be"));
            Assert.Equal(1, _event.Attendees.Count);
        }

        [Fact]
        public void AddMedia()
        {
            Media media = _event.AddMedia(123, MediaType.IMAGE, "/here/is/a/photo");
            Assert.Equal(1, _event.Media.Count);
            Assert.True(_event.Media.Contains(media));
            Assert.Throws<ArgumentException>(
                () => _event.AddMedia(123, MediaType.IMAGE, "/here/is/a/photo"));
            Assert.Equal(1, _event.Media.Count);
        }

        [Fact]
        public void AddGuest()
        {
            Guest guest = _event.AddGuest(1234, "Super Mario", "super@mario.be", "123456789");
            Assert.Equal(1, _event.Guests.Count);
            Assert.True(_event.Guests.Contains(guest));
            Assert.Throws<ArgumentException>(
                () => _event.AddGuest(1234, "Super Mario", "super@mario.be", "123456789"));
            Assert.Equal(1, _event.Guests.Count);
        }

        [Fact]
        public void AddFeedback()
        {
            Feedback feedback = _event.AddFeedback(12, 5, 
                new User("123456cd", "Jordy", "Van Kerkvoorde", UserType.USER, UserStatus.ACTIVE, "jordy@hogent.be"), 
                "Almost done testing this class");
            Assert.Equal(1, _event.Feedback.Count);
            Assert.True(_event.Feedback.Contains(feedback));
            Assert.Throws<ArgumentException>(
                () => _event.AddFeedback(12, 5,
                new User("123456cd", "Jordy", "Van Kerkvoorde", UserType.USER, UserStatus.ACTIVE, "jordy@hogent.be"),
                "Almost done testing this class"));
            Assert.Equal(1, _event.Feedback.Count);
        }
    }
}
