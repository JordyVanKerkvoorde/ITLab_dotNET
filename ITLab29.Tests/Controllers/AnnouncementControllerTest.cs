using ITLab29.Controllers;
using ITLab29.Exceptions;
using ITLab29.Models.Domain;
using ITLab29.Tests.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ITLab29.Tests.Controllers
{
    public class AnnouncementControllerTest
    {
        private DummyApplicationDbContext _dummyContext;
        private AnnouncementController _announcementController;
        private Mock<IAnnouncementRepository> _mockAnnouncementRepository;

        public AnnouncementControllerTest()
        {

            _dummyContext = new DummyApplicationDbContext();
            _mockAnnouncementRepository = new Mock<IAnnouncementRepository>();

            _announcementController = new AnnouncementController(_mockAnnouncementRepository.Object);

        }

        [Fact]
        public void Index_PassesAllAnnouncementsInOrder()
        {
            _mockAnnouncementRepository.Setup(a => a.GetAll()).Returns(_dummyContext.Announcements);
            var result = Assert.IsType<ViewResult>(_announcementController.Index());
            List<Announcement> announcements = Assert.IsType<List<Announcement>>(result.Model);
            Assert.Equal(3, announcements.Count);
            Assert.Equal("Geen fysieke lessen tot 18 mei.", announcements.ElementAt(0).Message);
            Assert.Equal("Lessen gaan online door.", announcements.ElementAt(1).Message);
            Assert.Equal("Sessies worden voorlopig uitgesteld.", announcements.ElementAt(2).Message);
        }

        [Fact]
        public void Index_NoAnnouncements()
        {
            _mockAnnouncementRepository.Setup(a => a.GetAll()).Throws(new EmptyListException(""));
            var result = Assert.IsType<ViewResult>(_announcementController.Index());
            List<Announcement> announcements = Assert.IsType<List<Announcement>>(result.Model);
            Assert.Empty(announcements);
        }


    }
}
