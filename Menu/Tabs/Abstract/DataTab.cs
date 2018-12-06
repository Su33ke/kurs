using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicService;
using LogicService.Services;

namespace Menu
{
    public abstract class DataTab : Tab
    {
        protected Type ObjectsType;
        protected TabTopPanel TopPanel;
        protected String SortKey;
        protected String FilterKeyword = null;

        public DataTab(DataService Service, SetTab SetTab, Type type)
            : base(Service, SetTab)
        {
            ObjectsType = type;

            Button.ButtonCallback OpenCallBack = new Button.ButtonCallback(OpenItemFromActiveElement);
            TopButtons.Add(new Button(ConsoleKey.Enter, OpenCallBack));
            TopButtons.Add(new Button(ConsoleKey.LeftArrow, TopPanelMoveLeft));
            TopButtons.Add(new Button(ConsoleKey.RightArrow, TopPanelMoveRight));
            TopButtons.Add(new Button("Open", ConsoleKey.F5, OpenCallBack));
            TopButtons.Add(new Button("Remove", ConsoleKey.F6, new Button.ButtonCallback(RemoveItem)));

            TopPanelInitialize();
            UpdateElements();
            SetTopPanelCursor(0);

        }

        protected abstract void TopPanelInitialize();

        public void TopPanelMoveRight()
        {
            SetTopPanelCursor(TopPanel.ActiveElement + 1);
        }

        public void TopPanelMoveLeft()
        {
            SetTopPanelCursor(TopPanel.ActiveElement - 1);
        }

        public void SetTopPanelCursor(int pos)
        {
            if (pos >= 0 && pos < TopPanel.Elements.Length)
            {
                TopPanel.ActiveElement = pos;
                SortKey = TopPanel.Elements[TopPanel.ActiveElement].Key;
                Updated = false;
            }
        }
        public abstract void EditObjectField(String Key, Guid ObjectId, DataEditTab InvokerTab);
        public abstract void Sort(String FieldName);

        override
        public void Show()
        {
            if (Updated == false)
            {
                UpdateElements();
            }
            if (ActiveElement >= Elements.Count)
            {
                ActiveElement = Elements.Count - 1;
            }
            Console.WriteLine();
            TopPanel.Show();
            Console.WriteLine();

            if (FilterKeyword != null)
            {
                Console.WriteLine("Результат запроса \"" + FilterKeyword + "\": \n");
            }

            ShowElements();

        }

        public void OpenItemFromActiveElement()
        {
            if (Elements[ActiveElement].ObjectId != Guid.Empty)
            {
                SetTabDelegate.Invoke(new DataEditTab(Service, SetTabDelegate, this, Elements[ActiveElement].ObjectId, ObjectsType), AddMainButtons: false);
            }
        }

        public void RemoveItem()
        {
            if (Elements[ActiveElement].ObjectId != Guid.Empty)
            {
                Service.RemoveItem(ObjectsType, Elements[ActiveElement].ObjectId);
                Updated = false;
            }
        }

    }
}
