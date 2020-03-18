using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Models.Domain {
    public interface ISessionRepository {
        IEnumerable<Session> GetAll();
        IEnumerable<Session> GetByDate(DateTime date);
        Session GetById(int sessionId);
        IEnumerable<Session> GetByResponsibleId(string id);
        IEnumerable<Session> GetOpenableSessions(string id);
        IEnumerable<Session> GetOpenableSessionsAsAdmin();
        IEnumerable<Session> GetOpenedSessions(string id);
        IEnumerable<Session> GetOpenedSessionsAsAdmin();
        IEnumerable<User> GetRegisteredUsersBySessionId(int id);
        void SaveChanges();
    }
}
