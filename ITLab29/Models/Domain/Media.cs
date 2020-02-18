using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Models.Domain
{
    public class Media
    {

        public int MediaId { get; }
        public MediaEnum Type { get; }
        public string Path { get; set; }

        public Media(int mediaId, MediaEnum type, string path)
        {
            MediaId = mediaId;
            Type = type;
            Path = path;
        }

    }
}
