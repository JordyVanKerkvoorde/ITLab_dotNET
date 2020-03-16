using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Exceptions
{
    public class EmptyListException : Exception
    {

        public EmptyListException(string message): base(message)
        {
            
        }
    }
}
