using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace Classes
{
    [Serializable]
    public class Student : Saveable, IComparable, ISerializable//, IXmlSerializable
    {
        public String FitstName;
        public String LastName;
        public int Course;

        public Student()
        {

        }

        public Student(String FitstName, String LastName, int Course)
        {
            this.FitstName = FitstName;
            this.LastName = LastName;
            this.Course = Course;
        }

        public Student(SerializationInfo info, StreamingContext context)
        {
            LastName = info.GetString("LastName");
        }

        public int CompareTo(object robj)
        {
            Book obj = robj as Book;

            if (Course > obj.serial)
            {
                return 1;
            }
            if (Course < obj.serial)
            {
                return -1;
            }

            return 0;
        }

        public String GetString()
        {
            return Course.ToString() + " " + LastName.ToString() + " " + FitstName;
        }

        public String[] GetParams()
        {
            return new String[] { "Course", "LastName", "FitstName" };
        }

        public void Initialize(String[] Params)
        {
            Course = Convert.ToInt32(Params[0]);
            LastName = Params[1];
            FitstName = Params[2];
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("LastName", LastName);
        }

        //public void WriteXml(XmlWriter writer)
        //{
        //    writer.WriteAttributeString("test", "USER");
        //    writer.WriteAttributeString("Course", Course.ToString());
        //    writer.WriteAttributeString("FitstName", FitstName);
        //    writer.WriteAttributeString("LastName", LastName);
        //}

        //public void ReadXml(XmlReader reader)
        //{
        //    Course = Convert.ToInt32(reader.GetAttribute("Course"));
        //    FitstName = reader.GetAttribute("FitstName");
        //    LastName = reader.GetAttribute("LastName");
        //}

        //public XmlSchema GetSchema()
        //{
        //    return (null);
        //}

    }
}
