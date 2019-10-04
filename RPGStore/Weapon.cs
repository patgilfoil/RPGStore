using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RPGStore
{
    class Weapon : Item
    {
        //private int for attack modifiers, a value exclusive to weapons
        private int _attackModifier = 0;
        //item type reference for weapons, including the attack modifier unique to weapons
        public Weapon(string Name, string Description, int Cost, int AttackValue)
        {
            _name = Name;
            _desc = Description;
            _cost = Cost;
            _attackModifier = AttackValue;
        }
        //attack modifier refernce for saving the game
        public override int GetAttackModifier()
        {
            return _attackModifier;
        }
        //item print override for weapons that displays the attack modifier
        public override void PrintItem()
        {
            Console.WriteLine("Name: " + _name);
            Console.WriteLine(_desc);
            Console.WriteLine("Attack Value: " + _attackModifier);
            Console.WriteLine("Cost: " + _cost);
        }
        //item save override for weapons
        public override void SaveItem(StreamWriter writer)
        {
            writer.WriteLine(_name);
            writer.WriteLine(_desc);
            writer.WriteLine(_cost);
            writer.WriteLine(_attackModifier);
        }
        //item loader override for weapons to include the attack modifier when loading up a save
        public override void LoadItem(StreamReader reader)
        {
            _name = reader.ReadLine();
            _desc = reader.ReadLine();
            _cost = Convert.ToInt32(reader.ReadLine());
            _attackModifier = Convert.ToInt32(reader.ReadLine());
        }
    }
}
