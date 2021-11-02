using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dining_Philsophers.Lib
{
    public class Fork
    {
        public string Name { get; private set; }

        public Fork(string name)
        {
            Name = name;
        }
    }
}
