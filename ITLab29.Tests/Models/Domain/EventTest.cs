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
        public void SetProperties()
        {
            Assert.Equal(_event.EventId, _eventId);
        }
    }
}
