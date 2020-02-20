using ITLab29.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Data
{
    public class DataInitializer
    {
        private readonly ApplicationDbContext _dbContext;

        public DataInitializer(ApplicationDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();

            if (_dbContext.Database.EnsureCreated())
            {
                User dummyUser = new User("864460yv", "Yorick", "Van de Woestyne", UserType.ADMIN, UserStatus.ACTIVE, "yvdwoest@gmail.com");
                User dummyUser2 = new User("12345wfd", "Eric", "De Man", UserType.RESPONSIBLE, UserStatus.ACTIVE, "ericdeman@man.com");
                User dummyUser3 = new User("596074kkk", "Jan", "Willem", UserType.RESPONSIBLE, UserStatus.ACTIVE, "janwillem@mail.com");

                IList<User> users = new List<User> { dummyUser, dummyUser2, dummyUser3 };
                _dbContext.User.AddRange(users);

                Location schoonmeersen = new Location("GSCHB1.420", CampusEnum.SCHOONMEERSEN, 500);
                Location aalst = new Location("GSCHA6.099", CampusEnum.AALST, 420);
                Location mercator = new Location("GSCHM4.012", CampusEnum.MERCATOR, 200);
                Location schoonmeersen2 = new Location("GSCHB3.220", CampusEnum.SCHOONMEERSEN, 100);

                IList<Location> locations = new List<Location> { schoonmeersen, schoonmeersen2, mercator, aalst };
                _dbContext.Locations.AddRange(locations);

                IList<Session> sessions = new List<Session>
                {
                    new Session("My first event ever", "Dit is een dummy description",
                        dummyUser, DateTime.Now.AddDays(2), DateTime.Now.AddDays(2).AddHours(2), 20, schoonmeersen),
                    new Session("My second event", "Dit is nog een dummy description",
                        dummyUser, DateTime.Now.AddDays(3), DateTime.Now.AddDays(3).AddHours(3), 69, aalst),
                    new Session("My third event", "Dit is nog een dummy description",
                        dummyUser2, DateTime.Now.AddDays(7), DateTime.Now.AddDays(7).AddHours(2), 30, mercator),
                    new Session("My fourth event", "Dit is nog een dummy description",
                        dummyUser2, DateTime.Now.AddDays(14), DateTime.Now.AddDays(14).AddHours(1), 123, schoonmeersen2),
                    new Session("My fifth event", "Dit is misschien een dummy description",
                        dummyUser3, DateTime.Now.AddDays(8), DateTime.Now.AddDays(8).AddHours(1), 30, schoonmeersen)
                };

                _dbContext.Sessions.AddRange(sessions);

                Guest guest1 = new Guest("De heer Meneer", "deheermeneer@mail.com", "0412 12 12 12");
                Guest guest2 = new Guest("De heer Mevrouw", "deheermevrouw@mail.com", "0413 13 13 13");
                Guest guest3 = new Guest("De heer Madame", "deheermadame@mail.com", "0414 14 14 14");
                Guest guest4 = new Guest("De heer Vrouwke", "deheervrouwke@mail.com", "0415 15 15 15");

                IList<Guest> guests = new List<Guest> { guest1, guest2, guest3, guest4 };
                foreach (var item in guests)
                {
                    _dbContext.Guests.Add(item);
                }
            }
            _dbContext.SaveChanges();
    }
    }
}
