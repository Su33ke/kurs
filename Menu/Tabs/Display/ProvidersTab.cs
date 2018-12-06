using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicService;
using LogicService.EnvObjects;
using LogicService.Services;
using Menu.Exceptions;

namespace Menu.Tabs.Display
{
    public class ProvidersTab : DataTab
    {
        public ProvidersTab(DataService Service, SetTab SetTab)
            : base(Service, SetTab, typeof(GoodsProvider))
        {

            TopButtons.Add(new Button("Add", ConsoleKey.F7, new Button.ButtonCallback(AddItem)));
            TopButtons.Add(new Button("Search", ConsoleKey.F8, new Button.ButtonCallback(Search)));
        }

        override
        protected void TopPanelInitialize()
        {
            KeyValuePair<string, string>[] TopPanelContent = new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("FirstName", "FirstName".PadRight(FormatContants.PROVIDER_FIRSTNAME_LENGHT)),
                new KeyValuePair<string, string>("LastName", "LastName".PadRight(FormatContants.PROVIDER_LASTNAME_LENGHT)),
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
            switch (FieldName)
            {
                case "FirstName":
                    Elements.Sort((x, y) => {
                        GoodsProvider obj = Service.GetProvider(x.ObjectId);
                        GoodsProvider obj2 = Service.GetProvider(y.ObjectId);
                        return obj.FirstName.CompareTo(obj2.FirstName);
                    });
                    break;
                case "LastName":
                    Elements.Sort((x, y) => {
                        GoodsProvider obj = Service.GetProvider(x.ObjectId);
                        GoodsProvider obj2 = Service.GetProvider(y.ObjectId);
                        return obj.LastName.CompareTo(obj2.LastName);
                    });
                    break;
            }
        }

        public void AddItem()
        {
            GoodsProvider NewItem = new GoodsProvider();
            Guid ObjectId = Service.AddItem(NewItem);

            SetTabDelegate.Invoke(new DataEditTab(Service, SetTabDelegate, this, ObjectId, typeof(GoodsProvider)), AddMainButtons: false);

            UpdateElements();
        }


        override
        public void EditObjectField(String FieldName, Guid ObjectId, DataEditTab InvokerTab)
        {
            String UserAnswer;
            GoodsProvider obj;
            switch (FieldName)
            {
                case "FirstName":
                    try
                    {
                        Console.Write("Enter new " + FieldName + ": ");
                        UserAnswer = Console.ReadLine();
                        obj = Service.GetProvider(ObjectId);
                        obj.FirstName = UserAnswer;
                        Service.UpdateItem(obj);
                    }
                    catch (Exception e)
                    {
                        InvokerTab.Message = e.Message;
                    }
                    break;
                case "LastName":
                    try
                    {
                        Console.Write("Enter new " + FieldName + ": ");
                        UserAnswer = Console.ReadLine();
                        obj = Service.GetProvider(ObjectId);
                        obj.LastName = UserAnswer;
                        Service.UpdateItem(obj);
                    }
                    catch (Exception e)
                    {
                        InvokerTab.Message = e.Message;
                    }
                    break;
                case "Goods":
                    obj = Service.GetProvider(ObjectId);
                    List<TabListElement> content = new List<TabListElement>();

                    Good[] Categories = Service.GetGoods();
                    foreach (Good Good in Categories)
                    {
                        if (obj.ProvideGoodsIds != null && obj.ProvideGoodsIds.Any(item => item.Equals(Good.GUID)))
                        {
                            content.Add(new TabListElement(Good.Name + " " + Good.BrandName, Id: Good.GUID, Checkable: true, Checked: true));
                        }
                        else
                        {
                            content.Add(new TabListElement(Good.Name + " " + Good.BrandName, Id: Good.GUID, Checkable: true));
                        }
                    }
                    EditProvideGoodsTab EditProvideGoodsTab = new EditProvideGoodsTab(Service, SetTabDelegate, InvokerTab, content, obj.GUID);
                    SetTabDelegate.Invoke(EditProvideGoodsTab, false);
                    break;
                default:
                    throw new UnexpectedFieldKey(FieldName);
            }
        }

        public override void UpdateElements()
        {
            GoodsProvider[] objs = Service.GetProviders(FilterKeyword);

            Elements.Clear();

            if (objs.Length == 0)
            {
                Elements.Add(new TabListElement("Здесь пусто."));
                return;
            }

            foreach (GoodsProvider obj in objs)
            {
                Elements.Add(new TabListElement(Formatter.GetStringToShow(obj), Id: obj.GUID));
            }

            Sort(SortKey);

            Updated = true;
        }
    }
}
