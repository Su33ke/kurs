using System;
using System.Collections.Generic;
using System.Text;
using Menu.Exceptions;

namespace Menu
{
    public class TopBar
    {
        public List<Button> Buttons;

        public TopBar()
        {
            Buttons = new List<Button>();
        }

        public TopBar(List<Button> NewButtons)
        {
            Buttons = new List<Button>();
            AddButtons(NewButtons);
        }

        public void AddButtons(List<Button> NewButtons)
        {
            for (int i = 0; i < NewButtons.Count; i++)
            {
                if (NewButtons[i].key == 0)
                {
                    throw new ZeroButtonKeyPtrException();
                }
                if (FindButtonByKey(NewButtons[i].key) != null)
                {
                    throw new AlreadyUsedButtonNameException();
                }
                Buttons.Add(NewButtons[i]);
            }
        }

        public Button FindButtonByKey(ConsoleKey Key)
        {
            foreach(Button Button in Buttons)
            {
                if (Button.key == Key)
                {
                    return Button;
                }
            }

            return null;
        }

        public void SwitchButton(Button button)
        {
            foreach (Button Button in Buttons)
            {
                if (Button.Switchable && Button.Toggle == true)
                {
                    Button.Toggle = false;
                }

                button.Toggle = true;
            }
        }

        public void In(ConsoleKey key)
        {
            foreach (Button Button in Buttons)
            {
                if (Button.key == key && Button.CallBack != null)
                {
                    Button.CallBack.Invoke();
                    if (Button.Switchable)
                    {
                        SwitchButton(Button);
                    }
                }
            }
        }

        public void Show()
        {
            for (int i = 0; i < Buttons.Count; i++)
            {
                if (Buttons[i].Visible == true) {

                    if (Buttons[i].Toggle)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    Buttons[i].Show();

                    if (Buttons[i].Toggle)
                    {
                        Console.ResetColor();
                    }

                    Console.Write(" || ");
                }
            }
            Console.WriteLine();
        }

        public String NameByKey(int key)
        {
            for (int i = 0; i < Buttons.Count; i++)
            {
                //if (Buttons[i].key == key - 112)
                //{
                //    return Buttons[i].name;
                //}
            }

            return null;
        }

    }
}
