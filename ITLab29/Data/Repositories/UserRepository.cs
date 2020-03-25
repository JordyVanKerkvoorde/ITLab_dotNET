using ITLab29.Exceptions;
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
            User result = _users.Where(u => u.UserName == name).Include(u => u.UserSessions).FirstOrDefault();

            if(result == null)
            {
                throw new UserNotFoundException($"No user found with the name : {name}.");
            }
            else
            {
                return result;
            }
        }

        public User GetById(string userId)
        {
            User result = _users
                .Include(u => u.UserSessions)
                .Include(u => u.Avatar)
                .SingleOrDefault(u => u.UserId == userId);

            if (result == null)
            {
                throw new UserNotFoundException($"No user found with the id : {userId}.");
            }
            else
            {
                return result;
            }
        }

        public IEnumerable<User> GetByLastName(string lastName)
        {
            List<User> result = _users.Where(u => u.LastName == lastName).ToList();

            if (!result.Any())
            {
                throw new EmptyListException("List of users is empty");
            }
            else
            {
                return result;
            }
            
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
