using System;
using System.Collections.Generic;
using System.Text;
using MyServices;

namespace Menu
{

    public delegate void SetTab(Tab NewTab);

    public class UIMain
    {
        TopBar topbar = new TopBar();
        Tab Activetab;
        DataService Service;
        SetTab SetTabDelegate;


        public UIMain()
        {
            SetTabDelegate = SetTab;
            Service = new DataService();
            //Activetab = new FileListTab(Service);
            Activetab = new FileListTab(Service, SetTabDelegate);
        }

        public void SetTab(Tab NewTab)
        {
            Activetab = NewTab;
        }

        public void render()
        {
            Console.Clear();
            Activetab.render();
        }

        public void In(ConsoleKey key)
        {
            Activetab.In(key);
            render();
        }
    }
}
