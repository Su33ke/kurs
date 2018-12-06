using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicService;

namespace Menu
{
    class GoodTab : Tab
    {
        public GoodTab(DataService Service, SetTab SetTab) 
            : base (Service, SetTab)
        {

        }
       
        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
