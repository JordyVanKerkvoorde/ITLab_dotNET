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
            List<Session> result = _sessions.AsNoTracking().ToList();
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
                .SingleOrDefault(s => s.SessionId == sessionId);

            if (result == null) throw new ArgumentNullException($"SessionId: {sessionId} has no resulting session." );
            return result;
        }

        public void SaveChanges() {
            _context.SaveChanges();
        }
    }
}
