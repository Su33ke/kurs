using System;
using System.Collections.Generic;
using System.Text;
using LogicService;
using Menu.Tabs.Display;

namespace Menu
{

    public delegate void SetTab(Tab NewTab, bool AddMainButtons = true);

    public class UIMain
    {
        TopBar Topbar;
        List<Button> TopButtons;
        Tab Activetab;
        DataService Service;
        SetTab SetTabDelegate;

        public UIMain()
        {
            SetTabDelegate = SetTab;
            Service = new DataService();

            TopButtons = new List<Button>();
            TopButtons.Add(new Button("Good", ConsoleKey.F1, new Button.ButtonCallback(OpenGoodsTub), true));
            TopButtons.Add(new Button("Category", ConsoleKey.F2, new Button.ButtonCallback(OpenCategoryTab), true));
            TopButtons.Add(new Button("Provider", ConsoleKey.F3, new Button.ButtonCallback(OpenProvidersTab), true));

            SetTabDelegate = SetTab;
            SetTab(null);

            Console.CursorVisible = false;
        }

        public void OpenGoodsTub()
        {
            SetTab(new GoodTab(Service, SetTabDelegate));
        }

        public void OpenCategoryTab()
        {
            SetTab(new CategoryTab(Service, SetTabDelegate));
        }

        public void OpenProvidersTab()
        {
            SetTab(new ProvidersTab(Service, SetTabDelegate));
        }

        private void UpdateTopbar(bool MainButtons)
        {
            Topbar = new TopBar();

            if (MainButtons == true)
            {
                Topbar.AddButtons(TopButtons);
            }

            if (Activetab != null && Activetab.TopButtons != null)
            {
                Topbar.AddButtons(Activetab.TopButtons);
            }
        }

        public void SetTab(Tab NewTab, bool AddMainButtons = true)
        {
            if (NewTab != null)
            {
                NewTab.Updated = false;
            }
            Activetab = NewTab;
            UpdateTopbar(AddMainButtons);
        }

        public void Show()
        {
            Console.Clear();
            Topbar.Show();
            if (Activetab != null)
            {
                Activetab.Show();
            }
        }

        public void In(ConsoleKey key)
        {
            Topbar.In(key);
            Show();
        }
    }
}
