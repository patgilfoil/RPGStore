using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGStore
{
    class Weapon : Item
    {
        private int _attackModifier = 0;

        public Weapon(string Name, string Description, int Cost, int AttackValue)
        {
            _name = Name;
            _desc = Description;
            _cost = Cost;
            _attackModifier = AttackValue;
        }

        public override int GetAttackModifier()
        {
            return _attackModifier;
        }

        public override void PrintItem()
        {
            Console.WriteLine("Name: " + _name);
            Console.WriteLine(_desc);
            Console.WriteLine("Attack Value: " + _attackModifier);
            Console.WriteLine("Cost: " + _cost);
        }
    }
}
