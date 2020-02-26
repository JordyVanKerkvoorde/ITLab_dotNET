using ITLab29.Models.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ITLab29.Tests.Data
{
    class DummyApplicationDbContext
    {

        private UserManager<User> _userManager;

        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Session> Sessions { get; set; }
        public IEnumerable<UserSession> UserSessions { get; set; }
        public IEnumerable<Media> Media { get; set; }
        public IEnumerable<Location> Locations { get; set; }
        public IEnumerable<Feedback> Feedback { get; set; }
        public IEnumerable<Guest> Guests { get; set; }


        public DummyApplicationDbContext(UserManager<User> userManager)
        {
            _userManager = userManager;

            User dummyUser = new User("88888", "Yorick", "VdW", UserType.ADMIN, UserStatus.ACTIVE)
            { UserName = "yvdwoest@gmail.com", Email = "yvdwoest@gmail.com", EmailConfirmed = true };
            User dummyUser2 = new User("11111", "Jan", "Willem", UserType.USER, UserStatus.INACTIVE)
            { UserName = "janw@gmail.com", Email = "janw@gmail.com", EmailConfirmed = true };
            User dummyUser3 = new User("12345", "Sander", "Machado", UserType.RESPONSIBLE, UserStatus.BLOCKED)
            { UserName = "sandercm@gmail.com", Email = "sandercm@gmail.com", EmailConfirmed = true };
            Media avatar = new Media(MediaType.IMAGE, "/photo/stock.png");
            Media = new List<Media> { avatar };
            dummyUser.Avatar = avatar;
            dummyUser2.Avatar = avatar;
            dummyUser3.Avatar = avatar;

            _userManager.CreateAsync(dummyUser, "P@ssword1");
            _userManager.CreateAsync(dummyUser2, "P@ssword1");
            _userManager.CreateAsync(dummyUser3, "P@ssword1");

            Users = new List<User> { dummyUser, dummyUser2, dummyUser3 };

            Location schoonmeersen = new Location("GSCHB1.420", CampusEnum.SCHOONMEERSEN, 500);
            Location aalst = new Location("GSCHA6.099", CampusEnum.AALST, 420);
            Location mercator = new Location("GSCHM4.012", CampusEnum.MERCATOR, 200);
            Location schoonmeersen2 = new Location("GSCHB3.220", CampusEnum.SCHOONMEERSEN, 100);

            Locations = new List<Location> { schoonmeersen, schoonmeersen2, mercator, aalst };
            

            Session session1 = new Session("My first event ever", "Dit is een dummy description",
                    dummyUser, DateTime.Now.AddDays(2), DateTime.Now.AddDays(2).AddHours(2), 20, schoonmeersen);
            Session session2 =
                new Session("My second event", "Dit is nog een dummy description",
                    dummyUser, DateTime.Now.AddDays(3), DateTime.Now.AddDays(3).AddHours(3), 69, aalst);

            Sessions = new List<Session>
                {
                    session1,
                    session2,
                    new Session("My third event", "Dit is nog een dummy description",
                        dummyUser2, DateTime.Now.AddDays(7), DateTime.Now.AddDays(7).AddHours(2), 30, mercator),
                    new Session("My fourth event", "Dit is nog een dummy description",
                        dummyUser2, DateTime.Now.AddDays(14), DateTime.Now.AddDays(14).AddHours(1), 123, schoonmeersen2),
                    new Session("My fifth event", "Dit is misschien een dummy description",
                        dummyUser3, DateTime.Now.AddDays(8), DateTime.Now.AddDays(8).AddHours(1), 30, schoonmeersen)
                };


            Feedback feedback1 = new Feedback(5, "Yeet");
            Feedback feedback2 = new Feedback(3, "foobar");

            Feedback = new List<Feedback> { feedback1, feedback2 };


            Guest guest1 = new Guest("De heer Meneer", "deheermeneer@mail.com", "0412 12 12 12");
            Guest guest2 = new Guest("De heer Mevrouw", "deheermevrouw@mail.com", "0413 13 13 13");
            Guest guest3 = new Guest("De heer Madame", "deheermadame@mail.com", "0414 14 14 14");
            Guest guest4 = new Guest("De heer Vrouwke", "deheervrouwke@mail.com", "0415 15 15 15");

            Guests = new List<Guest> { guest1, guest2, guest3, guest4 };
            

            Media = new List<Media>
                {
                    avatar,
                    new Media(MediaType.FILE, "/path/mamamia.pdf"),
                    new Media(MediaType.IMAGE, "/path/file.jpg"),
                    new Media(MediaType.VIDEO, "/path/videos/18+.mov")
                };


        }

    }
}
