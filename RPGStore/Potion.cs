using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGStore
{
    class Potion : Item
    {
        //item type reference for potions, there's no necessity for just a "plain" item
        public Potion(string Name, string Description, int Cost)
        {
            _name = Name;
            _desc = Description;
            _cost = Cost;
        }
    }
}
