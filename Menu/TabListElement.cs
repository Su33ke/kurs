using System;
using System.Collections.Generic;
using System.Text;

namespace Menu
{
    public class TabListElement
    {

        public String Key { get; set; }
        public String StringToShow { get; set; }
        public Guid ObjectId { get; set; }
        public bool Checkable { get; private set; }
        public bool Checked { get; set; } = false;

        public TabListElement(String StringToShow, String Key = null, Guid? Id = null, bool Checkable = false, bool Checked = false)
        {
            this.Checked = Checked;
            this.Checkable = Checkable;
            this.Key = Key;
            this.StringToShow = StringToShow;
            if (Id.HasValue)
            {
                ObjectId = Id.Value;
            }
        }

        public void Show()
        {
            String Show = "";


            if (Checkable)
            {
                if (Checked) 
                    Show += "[+]";
                else
                    Show += "[-]";
            }
            if (Key != null)
            {
                Show += Key + ": ";
            }

            Show += StringToShow;

            Console.WriteLine(Show);
        }
    }
}
