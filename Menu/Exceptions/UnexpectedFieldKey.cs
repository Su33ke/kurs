using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Exceptions
{
    class UnexpectedFieldKey : Exception
    {
        public UnexpectedFieldKey(String Key) : base("Field key \"" + Key + "\" not expected.")
        {

        }
    }
}
