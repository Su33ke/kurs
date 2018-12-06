using System;
using System.Collections.Generic;
using System.Text;
using LogicService;

namespace Menu
{
    public abstract class Tab
    {
        protected DataService Service;
        protected List<Button> TopButtons;

        protected List<TabListElement> Elements = new List<TabListElement>();

        protected TabListElement ActiveElement;
        protected SetTab SetTabDelegate;

        public abstract void UpdateList();

        public bool Updated { get; protected set; }

        public Tab(DataService Service, SetTab SetTab)
        {
            Updated = false;
            SetTabDelegate = SetTab;
            this.Service = Service;
        }

        public void SetActiveElem(int n)
        {
            if (Elements.IndexOf(ActiveElement) + n >= 0 && Elements.IndexOf(ActiveElement) + n < Elements.Count)
            {
                ActiveElement = Elements[Elements.IndexOf(ActiveElement) + n];
            }
        }

        protected void ShowElements()
        {
            for (int i = 0; i < Elements.Count; i++)
            {
                int h = Elements.IndexOf(ActiveElement);
                if (Elements.IndexOf(ActiveElement) == i)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Elements[i].render();

                if (Elements.IndexOf(ActiveElement) == i)
                {
                    Console.ResetColor();
                }
            }
        }

        public virtual void Show()
        {
            if (Updated == false)
            {
                Update();
            }
            Console.WriteLine();

            if (ActiveElement == null && Elements.Count > 0)
            {
                ActiveElement = Elements[0];
                Elements.IndexOf(ActiveElement);
            }

        }
    }
}
