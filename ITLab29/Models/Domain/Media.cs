using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Models.Domain
{
    public class Media
    {

        public int MediaId { get; set; }
        public MediaType Type { get; set; }
        public string Path { get; set; }

        public Media(int mediaId, MediaType type, string path)
        {
            MediaId = mediaId;
            Type = type;
            Path = path;
        }

    }
}
