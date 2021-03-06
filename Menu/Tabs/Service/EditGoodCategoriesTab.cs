﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicService;

namespace Menu
{
    class EditGoodCategoriesTab : Tab
    {

        Guid ObjectId;

        public EditGoodCategoriesTab(DataService Service, SetTab SetTab, DataEditTab PrevTab, List<TabListElement> content, Guid ObjectId)
           : base(Service, SetTab)
        {
            this.PrevTab = PrevTab;
            this.ObjectId = ObjectId;

            TopButtons.Add(new Button("Back", ConsoleKey.F1, new Button.ButtonCallback(Close)));
            TopButtons.Add(new Button(ConsoleKey.Backspace, new Button.ButtonCallback(Close)));

            TopButtons.Add(new Button("Check", ConsoleKey.F2, new Button.ButtonCallback(CheckElement)));
            TopButtons.Add(new Button(ConsoleKey.Enter, new Button.ButtonCallback(CheckElement)));

            Elements = content;

        }

        override
        public void Close()
        {
            var Checked = Elements.FindAll(item => item.Checked == true);
            String Answer = "";

            foreach(TabListElement item in Checked)
            {
                Answer += item.StringToShow + " ";
            }

            SetGoodCategories(Checked);

            if (SetTabDelegate != null && PrevTab != null)
                SetTabDelegate.Invoke(PrevTab, false);
        }

        public void SetGoodCategories(List<TabListElement> CheckedElements)
        {
            Guid[] CategoriesIds = new Guid[CheckedElements.Count];
            for ( int i = 0; i < CheckedElements.Count; i++)
            {
                CategoriesIds[i] = CheckedElements[i].ObjectId;
            }
            Service.SetGoodCategories(ObjectId, CategoriesIds);
        }

        public void CheckElement()
        {
            Elements[ActiveElement].Checked = !Elements[ActiveElement].Checked;
        }

        public override void UpdateElements()
        {
            Updated = true;
        }
    }
}
