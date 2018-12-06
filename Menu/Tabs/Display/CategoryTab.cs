using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicService;
using LogicService.EnvObjects;
using LogicService.Services;

namespace Menu
{
    class CategoryTab : DataTab
    {

        public CategoryTab(DataService Service, SetTab SetTab)
            : base(Service, SetTab, typeof(GoodCategory))
        {

            TopButtons.Add(new Button("Add", ConsoleKey.F7, new Button.ButtonCallback(AddItem)));

        }

        protected override void TopPanelInitialize()
        {
            KeyValuePair<string, string>[] TopPanelContent = new KeyValuePair<string, string>[]
           {
                new KeyValuePair<string, string>("Name", "Name".PadRight(FormatContants.CATEGORY_NAME_LENGHT)),
                new KeyValuePair<string, string>("Description", "Description".PadRight(FormatContants.CATEGORY_DESCRIPT_LENGHT)),
           };
            TopPanel = new TabTopPanel(TopPanelContent);
        }

        override
        public void Sort(String FieldName)
        {
            switch (FieldName)
            {
                case "Name":
                    Elements.Sort((x, y) => {
                        GoodCategory obj = Service.GetGoodsCategory(x.ObjectId);
                        GoodCategory obj2 = Service.GetGoodsCategory(y.ObjectId);
                        return obj.Name.CompareTo(obj2.Name);
                    });
                    break;
                case "Description":
                    Elements.Sort((x, y) => {
                        GoodCategory obj = Service.GetGoodsCategory(x.ObjectId);
                        GoodCategory obj2 = Service.GetGoodsCategory(y.ObjectId);
                        return obj.Description.CompareTo(obj2.Description);
                    });
                    break;
            }
        }

        public void AddItem()
        {
            GoodCategory NewItem = new GoodCategory();
            Guid ObjectId = Service.AddItem(NewItem);

            SetTabDelegate.Invoke(new DataEditTab(Service, SetTabDelegate, this, ObjectId, typeof(GoodCategory)), AddMainButtons: false);

            UpdateElements();
        }


        public override void EditObjectField(string FieldName, Guid ObjectId, DataEditTab InvokerTab)
        {
            String UserAnswer;
            GoodCategory obj;

            switch (FieldName)
            {
                case "Name":
                    Console.Write("Enter new " + FieldName + ": ");
                    try
                    {
                        UserAnswer = Console.ReadLine();
                        obj = Service.GetGoodsCategory(ObjectId);
                        obj.Name = UserAnswer;
                        Service.UpdateItem(obj);
                    }
                    catch (Exception e)
                    {
                        InvokerTab.Message = e.Message;
                    }
                    break;
                case "Description":
                    try
                    {
                        Console.Write("Enter new " + FieldName + ": ");
                        UserAnswer = Console.ReadLine();
                        obj = Service.GetGoodsCategory(ObjectId);
                        obj.Description = UserAnswer;
                        Service.UpdateItem(obj);
                    }
                    catch (Exception e)
                    {
                        InvokerTab.Message = e.Message;
                    }
                    break;
            }
        }

        override
        public void UpdateElements()
        {
            GoodCategory[] objs = Service.GetGoodsCategoryes();

            Elements.Clear();

            if (objs.Length == 0)
            {
                Elements.Add(new TabListElement("Здесь пусто."));
                return;
            }

            foreach (GoodCategory obj in objs)
            {
                Elements.Add(new TabListElement(Formatter.GetStringToShow(obj), Id: obj.GUID));
            }

            Sort(SortKey);

            Updated = true;
        }

    }
}
