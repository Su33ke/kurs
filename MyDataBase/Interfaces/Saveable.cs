using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataBase.Interfaces
{
    public interface Saveable
    {
        Guid GUID { get; set; }
    }
}
