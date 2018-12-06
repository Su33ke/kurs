using System;
using System.IO;
using System.Xml.Serialization;

namespace MyDataBase
{
    public class XmlProvider : DataProvider
    {
        XmlSerializer formatter;

        public XmlProvider(String Path, Type Type) : base(Path, Type)
        {
            formatter = new XmlSerializer(type);
        }

        override
        public void Save(object obj)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                fs.SetLength(0);
                formatter.Serialize(fs, obj);
            }
        }

        override
        public object Load()
        {
            object obj;

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                obj = formatter.Deserialize(fs);
            }

            return obj;
        }
    }
}
