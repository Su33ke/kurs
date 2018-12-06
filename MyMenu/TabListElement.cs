using System;
using System.Collections.Generic;
using System.Text;

namespace Menu
{
    public class TabListElement
    {

        public String content { get; }
        public object data { get; }

        public TabListElement(String content, object data = null)
        {
            this.data = data;
            this.content = content;
        }

        public void render()
        {
            Console.WriteLine(content);
        }
    }
}
