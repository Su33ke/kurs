using System;
using System.Collections.Generic;
using System.Text;
using MyMenu.Exceptions;

namespace Menu
{
    public class TopBar
    {
        List<Button> Buttons;

        public TopBar()
        {
            Buttons = new List<Button>();
        }

        public TopBar(List<Button> NewButtons)
        {
            Buttons = new List<Button>();
            SetButtons(NewButtons);
        }

        public void SetButtons(List<Button> Buttons)
        {
            for (int i = 0; i < Buttons.Count; i++)
            {
                if (FindButtonByName(Buttons[i].name != null))
                {
                    throw AlreadyUsedButtonNameException();
                }
                this.Buttons.Add(Buttons[i]);
            }
        }

        public Button FindButtonByName(String Name)
        {
            foreach(Button Button in Buttons)
            {
                if (Button.name == Name)
                {
                    return Button;
                }
            }

            return null;
        }

        public void render()
        {
            for (int i = 0; i < Buttons.Count; i++)
            {
                Buttons[i].Show();
                Console.Write(" || ");
            }
            Console.WriteLine();
        }

        public String NameByKey(int key)
        {
            for (int i = 0; i < Buttons.Count; i++)
            {
                if (Buttons[i].key == key - 112)
                {
                    return Buttons[i].name;
                }
            }

            return null;
        }

    }
}
