using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


namespace MyDataBase
{
    //public class JsonProvider<T> : DataProvider
    //{
    //    DataContractJsonSerializer formatter;

    //    public JsonProvider()
    //    {
    //        formatter = new DataContractJsonSerializer(typeof(T));
    //    }

    //    override
    //    public void Save(String path, object obj)
    //    {
    //        using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
    //        {
    //            formatter.WriteObject(fs, obj);
    //        }
    //    }

    //    override
    //    public object Load(String path)
    //    {
    //        object obj;

    //        using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
    //        {
    //            obj = formatter.ReadObject(fs);
    //        }

    //        return obj;
    //    }
    //}
}
