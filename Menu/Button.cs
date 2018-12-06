using System;
using System.Collections.Generic;
using System.Text;

namespace Menu
{
    public class Button
    {
        public delegate void ButtonCallback();

        public ConsoleKey key { get; set; }
        public String name;
        public bool Visible { get; set; } = true;
        public ButtonCallback CallBack { get; set; }
        public bool Toggle { get; set; } = false;
        public bool Switchable { get; set; }

        public Button(String name)
        {
            this.name = name;
        }

        public Button(String name, ButtonCallback CallBack)
        {
            this.name = name;
            this.CallBack = CallBack;
        }

        public Button(String name, ConsoleKey Key, ButtonCallback CallBack, bool Switchable = false)
        {
            this.Switchable = Switchable;
            this.name = name;
            this.CallBack = CallBack;
            this.key = Key;
        }

        public Button(ConsoleKey Key, ButtonCallback CallBack)
        {
            this.CallBack = CallBack;
            this.key = Key;
            Visible = false;
        }

        public void Show()
        {
            Console.Write(key.ToString() + " " + name);
        }
    }
}
