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
        //setting up the inventories and funds
        private Item[] shopInventory;
        private Item[] playerInventory;
        protected int shopFunds;
        protected int playerFunds;
        //shop items
        private Weapon claymore = new Weapon("Well-Worn Claymore", "A hand-me-down claymore that has seen a fair share of it's battles over time", 25, 20);
        private Weapon rapier = new Weapon("Steel Rapier", "All point but not much blade", 40, 32);
        private Item healPotion = new Potion("Healing Potion", "A basic healing potion crafted from everyday ingredients. Heals 25 HP.", 15);
        private Item superPotion = new Potion("Buff-Up Potion", "A potion that fills the user with tremendous amounts of energy, boosting their attack by 20%", 25);

        public Game()
        {
            //this is what the game will be like when starting up for the first time
            Item[] shopStock = { claymore, rapier, healPotion, superPotion };
            shopInventory = shopStock;
            Item[] playerItems = { };
            playerInventory = playerItems;
            int newShopFunds = 1000;
            shopFunds = newShopFunds;
            int newPlayerFunds = 200;
            playerFunds = newPlayerFunds;
        }
        //this is where pretty much all of the user input goes into and where feedback comes from
        public void ProcessInput()
        {
            Console.WriteLine("Welcome to the Item Shop!");
            LoadState("save.txt");
            bool satisfied = false;
            string input;
            while (!satisfied)
            {
                satisfied = true;
                Console.WriteLine("Currency: " + playerFunds);
                Console.WriteLine("(Buy Item/Sell Item/Exit Shop)");
                input = Console.ReadLine();
                //now we start checking for their input
                if (input.ToLower() == "buy item" || input.ToLower() == "buy")
                {
                    satisfied = false;
                    Console.WriteLine("Which item?");
                    PrintInventory(shopInventory);
                    input = Console.ReadLine();
                    for (int e = 0; e < shopInventory.Length; e++)
                    {
                        if (input.ToLower() == shopInventory[e].GetName().ToLower())
                        {
                            shopInventory[e].PrintItem();
                            if (shopInventory[e].ProcessBuyItem(input) == true)
                            {
                                playerFunds -= shopInventory[e].GetCost();
                                shopFunds += shopInventory[e].GetCost();
                                ItemPurchase(e);
                                Console.WriteLine("Thanks for your purchase!");
                            }
                        }
                    }
                }
                else if (input.ToLower() == "sell item" || input.ToLower() == "sell")
                {
                    satisfied = false;
                    Console.WriteLine("Let's have a look-see at your inventory shall we?");
                    PrintInventory(playerInventory);
                    input = Console.ReadLine();
                    for (int e = 0; e < playerInventory.Length; e++)
                    {
                        if (input.ToLower() == playerInventory[e].GetName().ToLower())
                        {
                            playerInventory[e].PrintItem();
                            if (playerInventory[e].ProcessSellItem(input) == true)
                            {
                                double sellCost = Convert.ToDouble(playerInventory[e].GetCost()) * 0.85;
                                shopFunds -= Convert.ToInt32(sellCost);
                                playerFunds += Convert.ToInt32(sellCost);
                                ItemBuyback(e);
                                Console.WriteLine("Thanks!");
                            }
                        }
                    }
                }
                //dev menu, you better spell onomatopoeia right, cause there's no exceptions
                else if (input.ToLower() == "onomatopoeia")
                {
                    satisfied = false;
                    bool itemCreated = false;
                    Console.WriteLine("WOW YOU ACCESSED THE SECRET DEV MENU!");
                    Console.WriteLine("What type of item is it?");
                    input = Console.ReadLine();
                    while (!itemCreated)
                    {
                        itemCreated = true;
                        if (input.ToLower() == "weapon")
                        {
                            Console.Write("Name: ");
                            string newName = Console.ReadLine();
                            Console.Write("Description: ");
                            string newDesc = Console.ReadLine();
                            Console.Write("Cost (Must be a number value): ");
                            int newCost = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Attack Modifier (Must be a number value): ");
                            int newAttack = Convert.ToInt32(Console.ReadLine());
                            Item newWeaponItem = new Weapon(newName, newDesc, newCost, newAttack);
                            newWeaponItem.PrintItem();
                            Console.WriteLine("Are you okay with this? (Yes/No)");
                            input = Console.ReadLine();
                            if (input.ToLower() == "yes")
                            {
                                Item[] tempList = new Item[shopInventory.Length + 1];
                                for (int i = 0; i < shopInventory.Length; i++)
                                {
                                    tempList[i] = shopInventory[i];
                                }
                                tempList[tempList.Length - 1] = newWeaponItem;
                                shopInventory = tempList;
                                Console.WriteLine("New item has been added to the shop.");
                            }
                            else if (input.ToLower() == "no")
                            {
                                itemCreated = false;
                            }
                        }
                        else if (input.ToLower() == "potion")
                        {
                            Console.Write("Name: ");
                            string newName = Console.ReadLine();
                            Console.Write("Description: ");
                            string newDesc = Console.ReadLine();
                            Console.Write("Cost (Must be a number value): ");
                            int newCost = Convert.ToInt32(Console.ReadLine());
                            Item newPotionItem = new Potion(newName, newDesc, newCost);
                            newPotionItem.PrintItem();
                            Console.WriteLine("Are you okay with this? (Yes/No)");
                            input = Console.ReadLine();
                            if (input.ToLower() == "yes")
                            {
                                Item[] tempList = new Item[shopInventory.Length + 1];
                                for (int i = 0; i < shopInventory.Length; i++)
                                {
                                    tempList[i] = shopInventory[i];
                                }
                                tempList[tempList.Length - 1] = newPotionItem;
                                shopInventory = tempList;
                                Console.WriteLine("New item has been added to the shop.");
                            }
                            else if (input.ToLower() == "no")
                            {
                                itemCreated = false;
                            }
                        }
                        else
                        {
                            itemCreated = false;
                        }
                    }
                }
                else if (input.ToLower() == "exit shop" || input.ToLower() == "exit")
                {
                    SaveState("save.txt");
                    Console.WriteLine("Thanks for stopping by!");
                }
                else
                {
                    satisfied = false;
                    Console.WriteLine("I can't understand what you're trying to say.");
                }
            }
        }

        public void PrintInventory(Item[] inv)
        {
            foreach (Item i in inv)
            {
                Console.WriteLine(i.GetName());
            }
        }

        public void SaveState(string path)
        {
            StreamWriter writer = File.CreateText(path);
            writer.WriteLine(shopInventory.Length);
            writer.WriteLine(playerInventory.Length);
            writer.WriteLine(shopFunds);
            writer.WriteLine(playerFunds);
            foreach (Item i in shopInventory)
            {
                writer.WriteLine(i);
                i.SaveItem(writer);
            }
            foreach (Item i in playerInventory)
            {
                writer.WriteLine(i);
                i.SaveItem(writer);
            }
            writer.Close();
        }

        public void LoadState(string path)
        {
            if (File.Exists(path))
            {
                StreamReader reader = new StreamReader(path);
                Item[] shopInvLoad = new Item[Convert.ToInt32(reader.ReadLine())];
                Item[] playerInvLoad = new Item[Convert.ToInt32(reader.ReadLine())];
                shopFunds = Convert.ToInt32(reader.ReadLine());
                playerFunds = Convert.ToInt32(reader.ReadLine());
                for (int i = 0; i < shopInvLoad.Length; i++)
                {
                    if (reader.ReadLine() == "RPGStore.Weapon")
                    {
                        shopInvLoad[i] = new Weapon("null", "null", 0, 0);
                        shopInvLoad[i].LoadItem(reader);
                    }
                    else if (reader.ReadLine() == "RPGStore.Potion")
                    {
                        shopInvLoad[i] = new Potion("null", "null", 0);
                        shopInvLoad[i].LoadItem(reader);
                    }
                }
                shopInventory = shopInvLoad;
                for (int i = 0; i < playerInvLoad.Length; i++)
                {
                    if (reader.ReadLine() == "RPGStore.Weapon")
                    {
                        playerInvLoad[i] = new Weapon("null", "null", 0, 0);
                        playerInvLoad[i].LoadItem(reader);
                    }
                    else if (reader.ReadLine() == "RPGStore.Potion")
                    {
                        playerInvLoad[i] = new Potion("null", "null", 0);
                        playerInvLoad[i].LoadItem(reader);
                    }
                }
                playerInventory = playerInvLoad;
                reader.Close();
            }
        }

        public void ItemPurchase(int index)
        {
            //temp list to store the item in
            Item[] tempList = new Item[playerInventory.Length + 1];
            for (int i = 0; i < playerInventory.Length; i++)
            {
                tempList[i] = playerInventory[i];
            }
            tempList[tempList.Length - 1] = shopInventory[index];
            playerInventory = tempList;
            tempList = new Item[shopInventory.Length - 1];
            int newPos = 0;
            for (int i = 0; i < shopInventory.Length; i++)
            {
                if (i != index)
                {
                    tempList[newPos] = shopInventory[i];
                    newPos++;
                }
            }
            shopInventory = tempList;
        }
        public void ItemBuyback(int index)
        {
            //this will be largely the same with the ItemPurchase function 
            //but shopInventory and playerInventory are flipped around
            Item[] tempList = new Item[shopInventory.Length + 1];
            for (int i = 0; i < shopInventory.Length; i++)
            {
                tempList[i] = shopInventory[i];
            }
            tempList[tempList.Length - 1] = playerInventory[index];
            shopInventory = tempList;
            tempList = new Item[playerInventory.Length - 1];
            int newPos = 0;
            for (int i = 0; i < playerInventory.Length; i++)
            {
                if (i != index)
                {
                    tempList[newPos] = playerInventory[i];
                    newPos++;
                }
            }
            playerInventory = tempList;
        }
    }
}
