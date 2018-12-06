using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicService;
using LogicService.Services;

namespace Menu
{
    public class DataEditTab : Tab
    {
        private Guid EditObjectGuid;
        private DataTab DataTab;
        private Type ObjectsType;
        public  String Message { get; set; } = null;

        public DataEditTab(DataService Service, SetTab SetTab, DataTab DataTab, Guid ObjectGuid, Type ObjectsType) 
            : base(Service, SetTab)
        {
            this.ObjectsType = ObjectsType;
            object obj = Service.GetItem(ObjectsType, ObjectGuid);
            SetTabDelegate = SetTab;
            this.DataTab = DataTab;
            PrevTab = DataTab;
            EditObjectGuid = ObjectGuid;

            TopButtons.Add(new Button("Back", ConsoleKey.F1, new Button.ButtonCallback(Close)));
            TopButtons.Add(new Button(ConsoleKey.Backspace, new Button.ButtonCallback(Close)));

            TopButtons.Add(new Button("Edit", ConsoleKey.F2, new Button.ButtonCallback(EditObjectField)));
            TopButtons.Add(new Button(ConsoleKey.Enter, new Button.ButtonCallback(EditObjectField)));

        }

        override
        public void Close()
        {
            base.Close();
        }

        private void EditObjectField()
        {
            DataTab.EditObjectField(Elements[ActiveElement].Key, EditObjectGuid, this);
            Updated = false;
        }

        override
        public void Show()
        {
            if (Updated == false)
            {
                UpdateElements();
            }

            Console.WriteLine();
            Console.WriteLine(ObjectsType.Name + "\n" + EditObjectGuid.ToString());
            if (Message == null)
            {
                Console.WriteLine();
            } else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Message);
                Console.ResetColor();
                Message = null;
            }

            ShowElements();

        }

        public override void UpdateElements()
        {
            Elements.Clear();

            List<KeyValuePair<string, string>> Content = Formatter.GetKeyValuePairInfo(ObjectsType, EditObjectGuid);

            foreach (KeyValuePair<string, string> element in Content)
            {
                Elements.Add(new TabListElement(element.Value, Key: element.Key));
            }

            Updated = true;
        }
    }
}
