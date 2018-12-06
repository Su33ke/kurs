using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Classes
{
    [Serializable]
    public class Book : IComparable, Saveable
    {
        public int year, serial;
        public String Name;

        public Book()
        {

        }

        public Book(int year, int serial, String Name)
        {
            this.year = year;
            this.serial = serial;
            this.Name = Name;
        }

        public int CompareTo(object robj)
        {
            Book obj = robj as Book;

            if (serial > obj.serial)
            {
                return 1;
            }
            if (serial < obj.serial)
            {
                return -1;
            }

            return 0;
        }

        public String GetString()
        {
            return serial.ToString() + " " + year.ToString() + " " + Name;
        }

        public String[] GetParams()
        {
            return new String[] { "Year", "Serial", "Name" };
        }

        public void Initialize(String[] Params)
        {
            year = Convert.ToInt32(Params[0]);
            serial = Convert.ToInt32(Params[1]);
            Name = Params[2];
        }
    }
}
