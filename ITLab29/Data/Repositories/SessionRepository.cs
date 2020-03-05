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
            return _sessions.AsNoTracking().ToList();
        }

        public IEnumerable<Session> GetByDate(DateTime date) {
            return _sessions.Where(s => s.Start.Month == date.Month).ToList();
        }

        public Session GetById(int sessionId) {
            return _sessions.Include(s => s.UserSessions)
                .Include(s => s.Location)
                .Include(s => s.Responsible)
                .Include(s => s.Media)
                .SingleOrDefault(s => s.SessionId == sessionId);
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
                .Where(s => (s.Start - DateTime.Now).TotalHours <= 1)
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
                .Where(s => (s.Start - DateTime.Now).TotalHours <= 1)
                .ToList();
        }



        public void SaveChanges() {
            _context.SaveChanges();
        }
    }
}
