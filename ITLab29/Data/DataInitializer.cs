﻿using ITLab29.Models.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Data
{
    public class DataInitializer
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public DataInitializer(ApplicationDbContext dbcontext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbcontext;
            _userManager = userManager;
        }

        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();

            if (_dbContext.Database.EnsureCreated())
            {

                User dummyUser = new User("88888", "Yorick", "VdW", UserType.ADMIN, UserStatus.ACTIVE)
                { UserName = "yvdwoest@gmail.com", Email = "yvdwoest@gmail.com", EmailConfirmed = true };
                User dummyUser2 = new User("11111", "Jan", "Willem", UserType.USER, UserStatus.INACTIVE)
                { UserName = "janw@gmail.com", Email = "janw@gmail.com", EmailConfirmed = true };
                User dummyUser3 = new User("12345", "Sander", "Machado", UserType.RESPONSIBLE, UserStatus.BLOCKED)
                { UserName = "sandercm@gmail.com", Email = "sandercm@gmail.com", EmailConfirmed = true };


                await _userManager.CreateAsync(dummyUser, "P@ssword1");
                await _userManager.CreateAsync(dummyUser2, "P@ssword1");
                await _userManager.CreateAsync(dummyUser3, "P@ssword1");
                //await InitializeUsers();
                //User dummyUser = new User("864460yv", "Yorick", "Van de Woestyne", UserType.ADMIN, UserStatus.ACTIVE, "yvdwoest@gmail.com");
                //User dummyUser2 = new User("12345wfd", "Eric", "De Man", UserType.RESPONSIBLE, UserStatus.ACTIVE, "ericdeman@man.com");
                //User dummyUser3 = new User("596074kkk", "Jan", "Willem", UserType.RESPONSIBLE, UserStatus.ACTIVE, "janwillem@mail.com");

                //IList<User> users = new List<User> { dummyUser, dummyUser2, dummyUser3 };
                //_dbContext.User.AddRange(users);

                Location schoonmeersen = new Location("GSCHB1.420", CampusEnum.SCHOONMEERSEN, 500);
                Location aalst = new Location("GSCHA6.099", CampusEnum.AALST, 420);
                Location mercator = new Location("GSCHM4.012", CampusEnum.MERCATOR, 200);
                Location schoonmeersen2 = new Location("GSCHB3.220", CampusEnum.SCHOONMEERSEN, 100);

                IList<Location> locations = new List<Location> { schoonmeersen, schoonmeersen2, mercator, aalst };
                _dbContext.Locations.AddRange(locations);

                Session session1 = new Session("My first event ever", "Dit is een dummy description",
                        dummyUser, DateTime.Now.AddDays(2), DateTime.Now.AddDays(2).AddHours(2), 20, schoonmeersen);
                Session session2 =
                    new Session("My second event", "Dit is nog een dummy description",
                        dummyUser, DateTime.Now.AddDays(3), DateTime.Now.AddDays(3).AddHours(3), 69, aalst);

                IList<Session> sessions = new List<Session>
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

                _dbContext.Sessions.AddRange(sessions);

                Feedback feedback1 = new Feedback(5, "Yeet");
                Feedback feedback2 = new Feedback(3, "foobar");

                _dbContext.Feedback.AddRange(new List<Feedback> { feedback1, feedback2 });


                Guest guest1 = new Guest("De heer Meneer", "deheermeneer@mail.com", "0412 12 12 12");
                Guest guest2 = new Guest("De heer Mevrouw", "deheermevrouw@mail.com", "0413 13 13 13");
                Guest guest3 = new Guest("De heer Madame", "deheermadame@mail.com", "0414 14 14 14");
                Guest guest4 = new Guest("De heer Vrouwke", "deheervrouwke@mail.com", "0415 15 15 15");

                IList<Guest> guests = new List<Guest> { guest1, guest2, guest3, guest4 };
                foreach (var item in guests)
                {
                    _dbContext.Guests.Add(item);
                }

                IList<Media> medias = new List<Media>
                {
                    new Media(MediaType.FILE, "/path/mamamia.pdf"),
                    new Media(MediaType.IMAGE, "/path/file.jpg"),
                    new Media(MediaType.VIDEO, "/path/videos/18+.mov")
                };

                _dbContext.Media.AddRange(medias);
            }
            _dbContext.SaveChanges();
        }

        private async Task InitializeUsers(User user, string password)
        {
            //IdentityUser user1 = new IdentityUser { UserName = "user1@gmail.com", Email = "user1@gmail.com", EmailConfirmed=true };
            //IdentityUser user2 = new IdentityUser { UserName = "user2@gmail.com", Email = "user2@gmail.com", EmailConfirmed=true };
            //await _userManager.CreateAsync(user1, "P@ssword1");
            //await _userManager.CreateAsync(user2, "P@ssword1");
            //User dummyUser = new User("864460yv", "Yorick", "Van de Woestyne", UserType.ADMIN, UserStatus.ACTIVE, "yvdwoest@gmail.com")
            //{ UserName = "yvdwoest@gmail.com", EmailConfirmed = true };
            await _userManager.CreateAsync(user, password);

            User dummyUser = new User("88888", "Yorick", "VdW", UserType.ADMIN, UserStatus.ACTIVE)
            { UserName = "yvdwoest@gmail.com", Email = "yvdwoest@gmail.com", EmailConfirmed = true };
            User user2 = new User("11111", "Jan", "Willem", UserType.USER, UserStatus.INACTIVE)
            { UserName = "janw@gmail.com", Email = "janw@gmail.com", EmailConfirmed = true };

            await _userManager.CreateAsync(dummyUser, "P@ssword1");
            await _userManager.CreateAsync(user2, "P@ssword1");
        }
    }
}
