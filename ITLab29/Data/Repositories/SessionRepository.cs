using ITLab29.Exceptions;
using ITLab29.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Data.Repositories {
    public class SessionRepository : ISessionRepository {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Session> _sessions;
        public SessionRepository(ApplicationDbContext context) {
            _context = context;
            _sessions = context.Sessions;
        }

        public IEnumerable<Session> GetAll() {
            List<Session> result = _sessions.Include(s=>s.Media).AsNoTracking().ToList();
            // check if list is empty
            if (!result.Any())
            {
                throw new EmptyListException("List of sessions is empty.");
            }
            return result;
        }

        public IEnumerable<Session> GetByDate(DateTime date)
        { 
            List<Session> result = _sessions.Where(s => s.Start.Month == date.Month).ToList();
            // check if list is empty
            if (!result.Any())
            {
                throw new EmptyListException("List of sessions is empty.");
            }
            return result;
        }

        public Session GetById(int sessionId) {
            Session result = _sessions.Include(s => s.UserSessions)
                .Include(s => s.Location)
                .Include(s => s.Responsible)
                .Include(s => s.Media)
                .Include(s => s.UserSessions)
                .Include(s => s.Feedback)
                .Include(s => s.PresentUsers)
                .SingleOrDefault(s => s.SessionId == sessionId);

            if (result == null) throw new SessionNotFoundException($"SessionId: {sessionId} has no resulting session." );
            return result;
        }

        public IEnumerable<Session> GetByResponsibleId(string id)
        {
            return _sessions
                .Include(s => s.UserSessions)
                .Include(s => s.Location)
                .Include(s => s.Responsible)
                .Include(s => s.Media)
                .Where(s => s.Responsible.Id == id)
                .ToList();
        }

        public IEnumerable<Session> GetOpenableSessions(string id)
        {
            return GetByResponsibleId(id)
                .Where(s => (s.Start - DateTime.Now).TotalHours <= 1 && (s.Start - DateTime.Now).TotalHours > 0)
                .Where(s => !s.IsOpened)
                .ToList();
        }

        public IEnumerable<Session> GetOpenableSessionsAsAdmin()
        {
            return _sessions
                .Include(s => s.UserSessions)
                .Include(s => s.Location)
                .Include(s => s.Responsible)
                .Include(s => s.Media)
                .ToList()
                .Where(s => (s.Start - DateTime.Now).TotalHours <= 1 && !s.IsOpened && (s.Start - DateTime.Now).TotalHours > 0)
                .ToList();
        }

        public IEnumerable<Session> GetOpenedSessions(string id)
        {
            return GetByResponsibleId(id)
                .Where(s => s.IsOpened)
                .ToList();
        }

        public IEnumerable<Session> GetOpenedSessionsAsAdmin()
        {
            return _sessions
                .Include(s => s.UserSessions)
                .Include(s => s.Location)
                .Include(s => s.Responsible)
                .Include(s => s.Media)
                .ToList()
                .Where(s => s.IsOpened)
                .ToList();
        }

        public IEnumerable<User> GetRegisteredUsersBySessionId(int id)
        {
            Session session = _sessions
                .Include(s => s.UserSessions)
                .Where(s => s.SessionId == id)
                .FirstOrDefault();

            if (session != null)
            {
                return session.UserSessions
                    .Select(us => us.User)
                    .ToList();
            }
            else
                return null;

        }

        public void SaveChanges() {
            _context.SaveChanges();
        }
    }
}
