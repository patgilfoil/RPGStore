using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGStore
{
    class Potion : Item
    {
        public Potion(string Name, string Description, int Cost)
        {
            _name = Name;
            _desc = Description;
            _cost = Cost;
        }
    }
}
