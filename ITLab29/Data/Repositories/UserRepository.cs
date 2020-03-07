using ITLab29.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<User> _users;
        private readonly DbSet<Session> _sessions;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
            _users = context.Users;
            _sessions = context.Sessions;
        }

        public IEnumerable<User> GetAll()
        {
            return _users.AsNoTracking().ToList();
        }

        public User GetByName(string name)
        {
            return _users.Where(u => u.UserName == name).Include(u => u.UserSessions).FirstOrDefault();
        }

        public User GetById(string userId)
        {
            return _users
                .Include(u => u.UserSessions) 
                //.Include(u => u.UserStatus)
                //.Include(u => u.UserType)
                .Include(u => u.Avatar)
                .SingleOrDefault(u => u.Id == userId);
        }

        public IEnumerable<User> GetByLastName(string lastName)
        {
            return _users.Where(u => u.LastName == lastName).ToList();
        }

        public IEnumerable<Session> GetRegisteredSessions(string userId)
        {
            User user = _users.Include(u => u.UserSessions).Where(p => p.UserId == userId).FirstOrDefault();
            // this part is needed to fix a bug in the framework
            // before the loop the Session field of UserSession will always be null
            // after the loop the fields are populated
            IList<Session> sessions = new List<Session>();
            foreach (UserSession us in user.UserSessions)
            {
                sessions.Add(_sessions.Where(s => s.SessionId == us.SessionId).FirstOrDefault());
            }
            return sessions;
        }


        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
