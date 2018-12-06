using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MyDataBase
{
    //public class BinaryProvider<T> : DataProvider
    //{
    //    BinaryFormatter formatter;

    //    public BinaryProvider()
    //    {
    //        formatter = new BinaryFormatter();
    //    }

    //    override
    //    public void Save(String path, object obj)
    //    {
    //        using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
    //        {
    //            formatter.Serialize(fs, obj);
    //        }
    //    }

    //    override
    //    public object Load(String path)
    //    {
    //        T obj;

    //        using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
    //        {
    //            obj = (T)formatter.Deserialize(fs);
    //        }

    //        return obj;
    //    }
    //}
}
