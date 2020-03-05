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
        void SaveChanges();
    }
}
