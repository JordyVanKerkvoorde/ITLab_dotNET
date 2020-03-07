using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Models.Domain
{
    public interface IMediaRepository
    {

        IEnumerable<Media> GetAll();
        Media GetById(int mediaId);
        IEnumerable<Media> GetByType(MediaType type);
        Media GetAvatar(string userId);
        void SaveChanges();
    }
}
