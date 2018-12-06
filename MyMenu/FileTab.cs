using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServices;
using Classes;

namespace Menu
{
    public class FileTab : Tab
    {
        String path;
        Type type;
        protected Tab PrevTab;

        public FileTab(DataService service, SetTab SetTab, Tab PrevTab, String path, Type type) : base(service, SetTab)
        {
            this.path = path;
            this.type = type;
            this.PrevTab = PrevTab;
            Topbar = new TopBar(new String[] { "Back", "Add", "Remove" });
            Update();
        }

        override
        public void Act(String command)
        {
            switch(command)
            {
                case "Remove":
                    Service.RemoveItem(path, ActiveElement.data);
                    Updated = false;
                    break;
                case "Add":
                    Saveable obj = (Saveable)Activator.CreateInstance(type);
                    String[] Params = obj.GetParams();

                    for(int i = 0; i < Params.Length; i++)
                    {
                        Console.Write(Params[i] + ": ");
                        Params[i] = Console.ReadLine();
                    }

                    obj.Initialize(Params);
                    Service.AddItem(path, obj);
                    Updated = false;
                    break;
                case "Back":
                    SetTabDelegate.Invoke(PrevTab);
                    break;
            }
        }

        override
        public void render()
        {
            base.render();
            Console.WriteLine("FIle " + path + ". Type: " + type.ToString());
            Console.WriteLine();
            RenderElements();
        }

        override
        public void Update()
        {
            Elements.Clear();
            Saveable[] objs;
            try
            {
                objs = (Saveable[])Service.GetItems(path, type);
            }
            catch (Exception)
            {
                objs = null;
            }

            if (objs == null)
            {
                Elements.Add(new TabListElement("Здесь пусто."));
                return;
            }

            foreach (Saveable obj in objs)
            {
                Elements.Add(new TabListElement(obj.GetString(), obj));
            }

            if (Elements.Count > 0)
            {
                ActiveElement = Elements[0];
            }
            Updated = true;

        }
    }
}
