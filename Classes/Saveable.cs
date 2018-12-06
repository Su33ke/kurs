using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public interface Saveable
    {
        String GetString();




        String[] GetParams();
        void Initialize(String[] Params);
    }
}
