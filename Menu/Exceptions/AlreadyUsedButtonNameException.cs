using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Exceptions
{
    class AlreadyUsedButtonNameException : Exception
    {
        public AlreadyUsedButtonNameException()
            : base("")
        { }
    }
}
