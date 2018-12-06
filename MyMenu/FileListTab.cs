using System;
using System.Collections.Generic;
using System.Text;
using MyServices;

namespace Menu
{
    public class FileListTab : Tab
    {
        public FileListTab(DataService service, SetTab SetTab) : base(service, SetTab)
        {
            Topbar = new TopBar(new String[] { "New", "Open" });
            Update();
        }

        override
        public void Act(String command)
        {
            switch(command)
            {
                case "New":
                    Console.Write("File name: ");
                    String filename = Console.ReadLine();
                    Console.Write("Objects type: ");
                    String type = Console.ReadLine();
                    Type otype = Type.GetType("Classes." + type + ", Classes");
                    FileTab FIleTab = new FileTab(Service, SetTabDelegate, this, filename, otype);
                    SetTabDelegate.Invoke(FIleTab);
                    Updated = false;
                    break;
                case "Open":
                    Console.Write("Objects type: ");
                    type = Console.ReadLine();
                    otype = Type.GetType("Classes." + type + ", Classes");
                    FIleTab = new FileTab(Service, SetTabDelegate, this, ActiveElement.content, otype);
                    SetTabDelegate.Invoke(FIleTab);
                    break;
            }
        }

        override
        public void render()
        {
            base.render();
            RenderElements();
        }

        override
        public void Update()
        {
            Elements.Clear();
            List<String> files = Service.GetFiles();

            foreach(String file in files) {
                Elements.Add(new TabListElement(file));
            }

            Updated = true;

        }
    }
}
