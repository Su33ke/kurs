using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Menu;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using MyDataBase;
using LogicService;
using System.Runtime.Serialization.Json;
using System.Collections;
using Classes;
using MyDataBase;
using LogicService.EnvObjects;

namespace ooplab3
{
    class Program
    {


        static void Main(string[] args)
        {

            //Good g = new Good();
            //g.BrandName = "DOtetoda";
            //String[] a = new String[] { "16f46a36-ed01-4db7-a153-1d50ce868317", "f6fb31cc-8c5d-453f-b7b5-2282e94713c3" };
            //g.CategoryesIds = a;
            //DataBase db = new DataBase();
            //db.AddItem("Goods.xml", g);
            //db.RemoveItem("Goods.xml", typeof(Good), Guid.Parse("96467a16-5ab6-4766-a4f4-86f2ac82aeb1"));

            UIMain menu = new UIMain();
            menu.Show();

            while (true)
            {
                ConsoleKeyInfo at = Console.ReadKey();
                menu.In(at.Key);
            }
        }

    }
}
