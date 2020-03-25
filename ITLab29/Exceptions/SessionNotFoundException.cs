using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Exceptions
{
    public class SessionNotFoundException : Exception
    {

        public SessionNotFoundException(string message) : base(message)
        {

        }
    }
}
