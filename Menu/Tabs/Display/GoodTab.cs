using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicService;
using LogicService.EnvObjects;
using LogicService.Services;
using Menu.Exceptions;

namespace Menu
{
    class GoodTab : DataTab
    {

        public GoodTab(DataService Service, SetTab SetTab) 
            : base (Service, SetTab, typeof(Good))
        {

            TopButtons.Add(new Button("Add", ConsoleKey.F7, new Button.ButtonCallback(AddItem)));
            TopButtons.Add(new Button("Search", ConsoleKey.F8, new Button.ButtonCallback(Search)));
        }

        override
        protected void TopPanelInitialize()
        {
            KeyValuePair<string, string>[] TopPanelContent = new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("Name", "Name".PadRight(FormatContants.GOOD_NAME_LENGTH)),
                new KeyValuePair<string, string>("BrandName", "BrandName".PadRight(FormatContants.GOOD_BRANDNAME_LENGHT)),
                new KeyValuePair<string, string>("Price", "Price".PadRight(FormatContants.GOOD_PRICE_LENGTH)),
                new KeyValuePair<string, string>("Count", "Count".PadRight(FormatContants.GOOD_COUNT_LENGTH)),
            };
            TopPanel = new TabTopPanel(TopPanelContent);
        }

        private void Search()
        {
            Console.Write("Enter keyword: ");
            String Keyword = Console.ReadLine();
            FilterKeyword = Keyword;
            Updated = false;
        }

        override
        public void Sort(String FieldName)
        {
            switch(FieldName)
            {
                case "Name":
                    Elements.Sort((x, y) => {
                        Good obj = Service.GetGood(x.ObjectId);
                        Good obj2 = Service.GetGood(y.ObjectId);
                        return obj.Name.CompareTo(obj2.Name);
                    });
                    break;
                case "BrandName":
                    Elements.Sort((x, y) => {
                        Good obj = Service.GetGood(x.ObjectId);
                        Good obj2 = Service.GetGood(y.ObjectId);
                        return obj.BrandName.CompareTo(obj2.BrandName);
                    });
                    break;
                case "Count":
                    Elements.Sort((x, y) => {
                        Good obj = Service.GetGood(x.ObjectId);
                        Good obj2 = Service.GetGood(y.ObjectId);
                        return obj.Count.CompareTo(obj2.Count);
                    });
                    break;
                case "Price":
                    Elements.Sort((x, y) => {
                        Good obj = Service.GetGood(x.ObjectId);
                        Good obj2 = Service.GetGood(y.ObjectId);
                        return obj.Price.CompareTo(obj2.Price);
                    });
                    break;
            }
        }

        public void AddItem()
        {
            Good NewItem = new Good();
            Guid ObjectId = Service.AddItem(NewItem);

            SetTabDelegate.Invoke(new DataEditTab(Service, SetTabDelegate, this, ObjectId, typeof(Good)), AddMainButtons: false);

            UpdateElements();
        }


        override
        public void EditObjectField(String FieldName, Guid ObjectId, DataEditTab InvokerTab)
        {
            String UserAnswer;
            Good obj;
            switch (FieldName)
            {
                case "Name":
                    Console.Write("Enter new " + FieldName + ": ");

                    try
                    {
                        UserAnswer = Console.ReadLine();
                        obj = Service.GetGood(ObjectId);
                        obj.Name = UserAnswer;
                        Service.UpdateItem(obj);
                    }
                    catch (Exception e)
                    {
                        InvokerTab.Message = e.Message;
                    }
                    break;
                case "BrandName":
                    try
                    {
                        Console.Write("Enter new " + FieldName + ": ");
                        UserAnswer = Console.ReadLine();
                        obj = Service.GetGood(ObjectId);
                        obj.BrandName = UserAnswer;
                        Service.UpdateItem(obj);
                    }
                    catch (Exception e)
                    {
                        InvokerTab.Message = e.Message;
                    }
                    break;
                case "Count":
                    try
                    {
                        Console.Write("Enter new " + FieldName + ": ");
                        UserAnswer = Console.ReadLine();
                        obj = Service.GetGood(ObjectId);
                        obj.SetCount(UserAnswer);
                        Service.UpdateItem(obj);
                    }
                    catch (Exception e)
                    {
                        InvokerTab.Message = e.Message;
                    }
                    break;
                case "Price":
                    try
                    {
                        Console.Write("Enter new " + FieldName + ": ");
                        UserAnswer = Console.ReadLine();
                        obj = Service.GetGood(ObjectId);
                        obj.SetPrice(UserAnswer);
                        Service.UpdateItem(obj);
                    }
                    catch (Exception e)
                    {
                        InvokerTab.Message = e.Message;
                    }
                    break;
                case "Categories":
                    obj = Service.GetGood(ObjectId);
                    List<TabListElement> content = new List<TabListElement>();

                    GoodCategory[] Categories = Service.GetGoodsCategoryes();
                    foreach (GoodCategory Category in Categories)
                    {
                        if (obj.CategoryesIds != null && obj.CategoryesIds.Any(item => item.Equals(Category.GUID)))
                        {
                            content.Add(new TabListElement(Category.Name, Id: Category.GUID, Checkable: true, Checked: true));
                        } else
                        {
                            content.Add(new TabListElement(Category.Name, Id: Category.GUID, Checkable: true));
                        }
                    }
                    EditGoodCategoriesTab EditCatTab = new EditGoodCategoriesTab(Service, SetTabDelegate, InvokerTab, content, obj.GUID);
                    SetTabDelegate.Invoke(EditCatTab, false);
                    break;
                default:
                    throw new UnexpectedFieldKey(FieldName);
            }
        }

        public override void UpdateElements()
        {
            Good[] objs = Service.GetGoods(FilterKeyword);

            Elements.Clear();

            if (objs.Length == 0)
            {
                Elements.Add(new TabListElement("Здесь пусто."));
                return;
            }

            foreach (Good obj in objs)
            {
                Elements.Add(new TabListElement(Formatter.GetStringToShow(obj), Id: obj.GUID));
            }

            Sort(SortKey);

            Updated = true;
        }
    }
}
