using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    public class TabTopPanel
    {
        //key, stringtoshow
        public KeyValuePair<string, string>[] Elements { get; set; }
        public int ActiveElement { get; set; }

        public TabTopPanel(KeyValuePair<string, string>[] elements)
        {
            Elements = elements;
        }

        public void Show()
        {
            for(int i = 0; i < Elements.Length; i++)
            {
                if (ActiveElement == i)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.Write(Elements[i].Value);

                if (ActiveElement == i)
                {
                    Console.ResetColor();
                }
            }

            Console.WriteLine();
        }
    }
}
