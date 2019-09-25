using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RPGStore
{
    class Item
    {
        protected string _name = "";
        protected string _desc = "";
        protected int _cost = 0;

        public virtual void PrintItem()
        {
            Console.WriteLine("Name: " + _name);
            Console.WriteLine(_desc);
            Console.WriteLine("Cost: " + _cost);
        }

        public string GetName()
        {
            return _name;
        }

        public string GetDesc()
        {
            return _desc;
        }

        public int GetCost()
        {
            return _cost;
        }

        public virtual int GetAttackModifier()
        {
            return 0;
        }

        public bool ProcessBuyItem(string input)
        {
            Console.WriteLine("Would you like to buy this item? (Yes/No)");
            input = Console.ReadLine();
            if (input.ToLower() == "yes")
            {
                //Console.WriteLine("Whoops! Can't figure out how to reference functions from 'Game' in 'Item'");
                return true;
            }
            else if (input.ToLower() == "no")
            {
                return false;
            }
            else
            {
                Console.WriteLine("I can't understand what you're talking about.");
                return false;
            }
        }

        public bool ProcessSellItem(string input)
        {
            double sellCost = _cost * 0.85;
            Console.WriteLine("Would you be interested in selling me this item for "+sellCost+"?");
            input = Console.ReadLine();
            if (input.ToLower() == "yes")
            {
                //Console.WriteLine("Whoops! Can't figure out how to reference functions from 'Game' in 'Item'");
                return true;
            }
            else if (input.ToLower() == "no")
            {
                return false;
            }
            else
            {
                Console.WriteLine("I can't understand what you're talking about.");
                return false;
            }
        }

        public virtual void LoadItem(StreamReader reader)
        {
            _name = reader.ReadLine();
            _desc = reader.ReadLine();
            _cost = Convert.ToInt32(reader.ReadLine());
            reader.ReadLine();
        }
    }
}
