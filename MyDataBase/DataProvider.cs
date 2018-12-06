using System;
using System.Collections.Generic;
using System.Text;

namespace MyDataBase
{
    public abstract class DataProvider
    {
        protected String path;
        protected Type type;

        public DataProvider(String Path, Type Type)
        {
            path = Path;
            type = Type;
        }

        public abstract void Save(object obj);
        public abstract object Load();
    }
}
