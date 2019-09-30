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
        //item print function, self explanatory, overridden for weapons
        public virtual void PrintItem()
        {
            Console.WriteLine("Name: " + _name);
            Console.WriteLine(_desc);
            Console.WriteLine("Cost: " + _cost);
        }
        //return type function for item names (used when saving the game)
        public string GetName()
        {
            return _name;
        }
        //return type function for item descriptions (used when saving the game)
        public string GetDesc()
        {
            return _desc;
        }
        //return type function for item costs (used when saving the game and when selling items)
        public int GetCost()
        {
            return _cost;
        }
        //return type virtual function (returns zero unless the item is a weapon)
        public virtual int GetAttackModifier()
        {
            return 0;
        }
        //function to return a bool that confirms the user's desire to purchase an item
        public bool ProcessBuyItem(string input)
        {
            Console.WriteLine();
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
                Console.WriteLine();
                Console.WriteLine("I can't understand what you're talking about.");
                return false;
            }
        }
        //this is largely the same as the ProcessBuyItem() function
        //with the exception of the text that shows the item's selling value
        public bool ProcessSellItem(string input)
        {
            //all items are sold at 85% of their original price
            double sellCost = _cost * 0.85;
            Console.WriteLine();
            Console.WriteLine("Would you be interested in selling me this item for "+Convert.ToInt32(sellCost)+"? (Yes/No)");
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
                Console.WriteLine();
                Console.WriteLine("I can't understand what you're talking about.");
                return false;
            }
        }
        //virtual function to save items when exiting the program, overridden for weapons
        public virtual void SaveItem(StreamWriter writer)
        {
            writer.WriteLine(_name);
            writer.WriteLine(_desc);
            writer.WriteLine(_cost);
        }

        //virtual function to load items when loading saves, overridden for weapons
        public virtual void LoadItem(StreamReader reader)
        {
            _name = reader.ReadLine();
            _desc = reader.ReadLine();
            _cost = Convert.ToInt32(reader.ReadLine());
        }
    }
}
