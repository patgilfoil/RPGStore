using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGStore
{
    class Game
    {
        private Item[] shopInventory;
        private Item[] playerInventory;
        private int shopFunds;
        private int playerFunds;

        private Weapon claymore = new Weapon("Well-Worn Claymore", "A hand-me-down claymore that has seen a fair share of it's battles over time", 25, 20);
        private Weapon rapier = new Weapon("Steel Rapier", "All point but not much blade", 40, 32);
        private Item healPotion = new Potion("Healing Potion", "A basic healing potion crafted from everyday ingredients. Heals 25 HP.", 15);
        private Item superPotion = new Potion("Buff-Up Potion", "A potion that fills the user with tremendous amounts of energy, boosting their attack by 20%", 25);

        public Game()
        {
            Item[] shopStock = { claymore, rapier, healPotion, superPotion };
            shopInventory = shopStock;
            Item[] playerItems = { };
            playerInventory = playerItems;
            int newShopFunds = 1000;
            shopFunds = newShopFunds;
            int newPlayerFunds = 200;
            playerFunds = newPlayerFunds;
        }

        public void ProcessInput()
        {
            bool satisfied = false;
            string input;
            while (!satisfied)
            {
                Console.WriteLine("Welcome to the Item Shop!");
                Console.WriteLine("(View Item/Sell Item)");
                input = Console.ReadLine();
                //now we start checking for their input
                if (input.ToLower() == "view item" || input.ToLower() == "view")
                {
                    Console.WriteLine("Which item?");
                    PrintShopInventory();
                    input = Console.ReadLine();
                    if (input.ToLower() == claymore.GetName().ToLower())
                    {
                        claymore.PrintItem();
                    }
                    else if (input.ToLower() == rapier.GetName().ToLower())
                    {
                        rapier.PrintItem();
                    }
                    else if (input.ToLower() == healPotion.GetName().ToLower())
                    {
                        healPotion.PrintItem();
                    }
                    else if (input.ToLower() == superPotion.GetName().ToLower())
                    {
                        superPotion.PrintItem();
                    }
                }
            }
        }

        public void PrintShopInventory()
        {
            foreach (Item i in shopInventory)
            {
                Console.WriteLine(i.GetName());
            }
        }
    }
}
