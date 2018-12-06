using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace MyClasses
{
    public class Book
    {
        int year, serial;
        String Name;

        public Book()
        {

        }

        public Book(int year, int serial, String Name)
        {
            this.year = year;
            this.serial = serial;
            this.Name = Name;
        }
    }
}
