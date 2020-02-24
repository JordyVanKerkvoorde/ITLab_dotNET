using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Models.Domain
{
    interface IFeedbackRepository
    {
        IEnumerable<Feedback> GetAll();
        Feedback GetById(int mediaId);
        IEnumerable<Feedback> GetByUser(User user);
        void SaveChanges();
    }
}
