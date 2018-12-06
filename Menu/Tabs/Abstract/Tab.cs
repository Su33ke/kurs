using System;
using System.Collections.Generic;
using System.Text;
using LogicService;

namespace Menu
{
    public abstract class Tab
    {
        protected DataService Service;
        public List<Button> TopButtons;
        protected Tab PrevTab;

        protected List<TabListElement> Elements = new List<TabListElement>();

        //protected TabListElement ActiveElement;
        protected int ActiveElement;
        protected SetTab SetTabDelegate;

        public bool Updated { get; set; }
        public abstract void UpdateElements();

        public virtual void Close()
        {
            if (SetTabDelegate != null && PrevTab != null) 
                SetTabDelegate.Invoke(PrevTab);
        }

        public Tab(DataService Service, SetTab SetTab)
        {
            TopButtons = new List<Button>();
            TopButtons.Add(new Button(ConsoleKey.UpArrow, MoveCursorTop));
            TopButtons.Add(new Button(ConsoleKey.DownArrow, MoveCursorDown));
            Updated = false;
            SetTabDelegate = SetTab;
            this.Service = Service;
        }

        public void MoveCursorTop()
        {
            SetActiveElem(-1);
        }

        public void MoveCursorDown()
        {
            SetActiveElem(1);
        }

        public void SetActiveElem(int n)
        {
            if (ActiveElement + n >= 0 && ActiveElement + n < Elements.Count)
            {
                ActiveElement = ActiveElement + n;
            }
        }

        protected void ShowElements()
        {
            for (int i = 0; i < Elements.Count; i++)
            {
                int h = ActiveElement;
                if (ActiveElement == i)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Elements[i].Show();

                if (ActiveElement == i)
                {
                    Console.ResetColor();
                }
            }
        }

        public virtual void Show()
        {
            if (Updated == false)
            {
                UpdateElements();
            }
            
            Console.WriteLine();

            ShowElements();

        }
    }
}
