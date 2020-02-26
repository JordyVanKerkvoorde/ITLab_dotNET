using ITLab29.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Data.Repositories
{
    public class UserSessionRepository : IUserSessionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<UserSession> _usersessions;

        public UserSessionRepository(ApplicationDbContext context)
        {
            _context = context;
            _usersessions = context.UserSessions;
        }

        public void AddSessiontoUser(Session session, User user)
        {
            _usersessions.Add(new UserSession
            {
                UserId = user.UserId,
                User = user,
                Session = session,
                SessionId = session.SessionId
            });
        }

        public void AddUsertoSession(User user, Session session)
        {
            _usersessions.Add(new UserSession
            {
                UserId = user.UserId,
                User = user,
                Session = session,
                SessionId = session.SessionId
            });
        }

        public IEnumerable<Session> GetSessionsByUser(string userId)
        {
            return _usersessions.Where(us => us.UserId == userId).Select(us => us.Session).ToList();
        }

        public IEnumerable<User> GetUsersBySessionId(int sessionId)
        {
            return _usersessions.Where(us => us.SessionId == sessionId).Select(us => us.User).ToList();
        }

        public void RemoveUserSession(Session session, User user) {
            _usersessions.Remove(_usersessions.Where(us => us.Session == session && us.User == user).FirstOrDefault());
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
