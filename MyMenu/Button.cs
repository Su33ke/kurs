using System;
using System.Collections.Generic;
using System.Text;

namespace Menu
{
    public class Button
    {
        public delegate void ButtonCallback();

        public int key { get; set; }
        public String name;
        public bool Visible { get; set; }
        public ButtonCallback CallBack { get; set; }

        public Button(String name)
        {
            this.name = name;
        }

        public void Show()
        {
            Console.Write("F" + Convert.ToString(key + 1) + " " + name);
        }
    }
}
