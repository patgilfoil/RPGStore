using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
            LoadState("save.txt");
            bool satisfied = false;
            string input;
            while (!satisfied)
            {
                Console.WriteLine("Welcome to the Item Shop!");
                Console.WriteLine("(View Item/Sell Item/Exit Shop)");
                input = Console.ReadLine();
                //now we start checking for their input
                if (input.ToLower() == "view item" || input.ToLower() == "view")
                {
                    Console.WriteLine("Which item?");
                    PrintShopInventory();
                    input = Console.ReadLine();
                    for (int e = 0; e < shopInventory.Length; e++)
                    {
                        if (input.ToLower() == shopInventory[e].GetName().ToLower())
                        {
                            shopInventory[e].PrintItem();
                            shopInventory[e].BuyItem(input, playerFunds, shopFunds);
                            
                        }
                    }
                }
                else if (input.ToLower() == "exit shop" || input.ToLower() == "exit")
                {
                    SaveState("save.txt");
                    Console.WriteLine("Thanks for stopping by!");
                    return;
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

        public void SaveState(string path)
        {
            StreamWriter writer = File.AppendText(path);
            writer.WriteLine(shopFunds);
            writer.WriteLine(playerFunds);
            foreach (Item i in shopInventory)
            {
                writer.WriteLine(i.GetName());
                writer.WriteLine(i.GetDesc());
                writer.WriteLine(i.GetCost());
                writer.WriteLine(i.GetAttackModifier());
            }
            foreach (Item i in playerInventory)
            {
                writer.WriteLine(i.GetName());
                writer.WriteLine(i.GetDesc());
                writer.WriteLine(i.GetCost());
                writer.WriteLine(i.GetAttackModifier());
            }
            writer.Close();
        }

        public void LoadState(string path)
        {
            if (File.Exists(path))
            {
                StreamReader reader = new StreamReader(path);
                shopFunds = Convert.ToInt32(reader.ReadLine());
                playerFunds = Convert.ToInt32(reader.ReadLine());
                for (int i = 0; i < shopInventory.Length; i++)
                {
                    shopInventory[i].LoadItem(reader);
                }
                for (int i = 0; i < playerInventory.Length; i++)
                {
                    playerInventory[i].LoadItem(reader);
                }
            }
        }

        public void ItemTransfer(int index, Item[] sender, Item[] receiver)
        {

        }
    }
}
