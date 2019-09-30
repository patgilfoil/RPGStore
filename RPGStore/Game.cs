﻿using System;
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
        private int shopFunds;
        private int playerFunds;
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
            LoadState("save.txt");
            Console.WriteLine("'Ah, welcome traveler, to my shoppe.'");
            bool satisfied = false;
            //this one string is for all user input, since there is no need for other strings to replace it
            string input;
            while (!satisfied)
            {
                //show how much currency the user has, and their current options
                Console.WriteLine();
                Console.WriteLine("Currency: " + playerFunds);
                Console.WriteLine("(Buy Item/Sell Item/View Inventory/Exit Shop)");
                input = Console.ReadLine();
                //after every function is successfully completed, the game will save
                //now we start checking for their input, first up is buying
                if (input.ToLower() == "buy item" || input.ToLower() == "buy")
                {
                    //show the user the shop inventory so they can see what they can buy
                    Console.WriteLine();
                    Console.WriteLine("'Let me show you my wares.'");
                    PrintInventory(shopInventory);
                    input = Console.ReadLine();
                    //goes through each ite, in the shopInventory array 
                    //and checks to see if the input matches the name of any of the shop items
                    for (int e = 0; e < shopInventory.Length; e++)
                    {
                        //input has to match the exact characters of the item
                        if (input.ToLower() == shopInventory[e].GetName().ToLower())
                        {
                            //display item attributes (name, cost, etc.)
                            shopInventory[e].PrintItem();
                            //if the users says yes to buying the item
                            if (shopInventory[e].ProcessBuyItem(input) == true)
                            {
                                //take the cost of the item, subtract it from the player's funds
                                playerFunds -= shopInventory[e].GetCost();
                                //and add it to the shop funds
                                shopFunds += shopInventory[e].GetCost();
                                //then give the player the item
                                ItemPurchase(e);
                                Console.WriteLine("'Very much a pleasure doing business with thee.'");
                                SaveState("save.txt");
                            }
                        }
                    }
                }
                //selling items
                else if (input.ToLower() == "sell item" || input.ToLower() == "sell")
                {
                    //show the player inventory, then read next line for input
                    Console.WriteLine();
                    Console.WriteLine("'Let's have a look-see at your inventory shall we?'");
                    PrintInventory(playerInventory);
                    input = Console.ReadLine();
                    //same item lookup method as buying except with player inventory
                    for (int e = 0; e < playerInventory.Length; e++)
                    {
                        //still looks for exact spelling
                        if (input.ToLower() == playerInventory[e].GetName().ToLower())
                        {
                            //show item attributes blahblahblah
                            playerInventory[e].PrintItem();
                            //ask if they want to sell for 85% of the original price
                            if (playerInventory[e].ProcessSellItem(input) == true)
                            {
                                //make a new double that takes the cost of the chosen item 
                                //then reduces it to 85% of its original value
                                double sellCost = Convert.ToDouble(playerInventory[e].GetCost()) * 0.85;
                                //transaction
                                shopFunds -= Convert.ToInt32(sellCost);
                                playerFunds += Convert.ToInt32(sellCost);
                                //goods transfer, economics definition woo
                                ItemBuyback(e);
                                Console.WriteLine("'This will make a fine addition for my customers.'");
                                SaveState("save.txt");
                            }
                        }
                    }
                }
                //view inventory
                else if (input.ToLower() == "view inventory" || input.ToLower() == "inventory")
                {
                    if (playerInventory.Length == 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("'My friend you have nothing!'");
                        satisfied = false;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Type in the name of an item, or type 'exit' to exit.");
                        bool viewingItems = true;
                        while (viewingItems)
                        {
                            Console.WriteLine();
                            PrintInventory(playerInventory);
                            input = Console.ReadLine();
                            for (int e = 0; e < playerInventory.Length; e++)
                            {

                                if (input.ToLower() == playerInventory[e].GetName().ToLower())
                                {
                                    playerInventory[e].PrintItem();
                                    Console.ReadKey();
                                }
                                else if (input.ToLower() == "exit")
                                {
                                    viewingItems = false;
                                }
                            }
                        }
                    }
                }
                //dev menu, you better spell onomatopoeia right, cause there's no exceptions
                //this'll be commented out eventually
                else if (input.ToLower() == "onomatopoeia")
                {
                    //bool for a loop to check for item creation
                    bool itemCreated = false;
                    Console.WriteLine("WOW YOU ACCESSED THE SECRET DEV MENU!");
                    Console.WriteLine("What type of item are you creating?");
                    input = Console.ReadLine();
                    while (!itemCreated)
                    {
                        itemCreated = true;
                        if (input.ToLower() == "weapon")
                        {
                            //ask the user what all the attributes should be
                            Console.Write("Name: ");
                            string newName = Console.ReadLine();
                            Console.Write("Description: ");
                            string newDesc = Console.ReadLine();
                            Console.Write("Cost (Must be a number value): ");
                            int newCost = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Attack Modifier (Must be a number value): ");
                            int newAttack = Convert.ToInt32(Console.ReadLine());
                            //create the item and show them how it looks
                            Item newWeaponItem = new Weapon(newName, newDesc, newCost, newAttack);
                            newWeaponItem.PrintItem();
                            //ask if it's how they wanted it to be
                            Console.WriteLine("Are you okay with this? (Yes/No)");
                            input = Console.ReadLine();
                            if (input.ToLower() == "yes")
                            {
                                //place the item in the shop
                                Item[] tempList = new Item[shopInventory.Length + 1];
                                for (int i = 0; i < shopInventory.Length; i++)
                                {
                                    tempList[i] = shopInventory[i];
                                }
                                tempList[tempList.Length - 1] = newWeaponItem;
                                shopInventory = tempList;
                                Console.WriteLine("New item has been added to the shop.");
                                SaveState("save.txt");
                            }
                            else if (input.ToLower() == "no")
                            {
                                //loop back to the beginning
                                itemCreated = false;
                            }
                        }
                        else if (input.ToLower() == "potion")
                        {
                            //same biz but with potions
                            Console.Write("Name: ");
                            string newName = Console.ReadLine();
                            Console.Write("Description: ");
                            string newDesc = Console.ReadLine();
                            Console.Write("Cost (Must be a number value): ");
                            int newCost = Convert.ToInt32(Console.ReadLine());
                            //show them the item
                            Item newPotionItem = new Potion(newName, newDesc, newCost);
                            newPotionItem.PrintItem();
                            //ask if it be how it should be
                            Console.WriteLine("Are you okay with this? (Yes/No)");
                            input = Console.ReadLine();
                            if (input.ToLower() == "yes")
                            {
                                //add it to the shop
                                Item[] tempList = new Item[shopInventory.Length + 1];
                                for (int i = 0; i < shopInventory.Length; i++)
                                {
                                    tempList[i] = shopInventory[i];
                                }
                                tempList[tempList.Length - 1] = newPotionItem;
                                shopInventory = tempList;
                                Console.WriteLine("New item has been added to the shop.");
                                SaveState("save.txt");
                            }
                            else if (input.ToLower() == "no")
                            {
                                //loop back to the beginning
                                itemCreated = false;
                            }
                        }
                        else
                        {
                            itemCreated = false;
                        }
                    }
                }
                //exit shop function
                else if (input.ToLower() == "exit shop" || input.ToLower() == "exit")
                {
                    //end the loop and save data
                    satisfied = true;
                    SaveState("save.txt");
                    Console.WriteLine("'Thanks for stopping by.'");
                }
                else
                {
                    Console.WriteLine("I can't understand what you're trying to say.");
                }
            }
        }
        //simple print inventory function that just calls for the items' names
        //takes an item array as an input
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
            //get the lengths of the arrays, so when loading it will have the correct number of items
            writer.WriteLine(shopInventory.Length);
            writer.WriteLine(playerInventory.Length);
            //get the player and shop funds
            writer.WriteLine(shopFunds);
            writer.WriteLine(playerFunds);
            foreach (Item i in shopInventory)
            {
                //writer prints out "RPGStore.(Type of item)" to later determine the specific item when loading saves
                writer.WriteLine(i);
                //the spews out all the other associated variables in the shop inventory
                i.SaveItem(writer);
            }
            foreach (Item i in playerInventory)
            {
                //same as above, but for the player inventory
                writer.WriteLine(i);
                i.SaveItem(writer);
            }
            writer.Close();
        }

        public void LoadState(string path)
        {
            //we check to see if a save file exists before we load anything
            if (File.Exists(path))
            {
                StreamReader reader = new StreamReader(path);
                //temporary arrays created during the loading process 
                //eventually for the data to be transferred over to their respective normal arrays
                Item[] shopInvLoad = new Item[Convert.ToInt32(reader.ReadLine())];
                Item[] playerInvLoad = new Item[Convert.ToInt32(reader.ReadLine())];
                //retrieve the shop and player funds
                shopFunds = Convert.ToInt32(reader.ReadLine());
                playerFunds = Convert.ToInt32(reader.ReadLine());
                for (int i = 0; i < shopInvLoad.Length; i++)
                {
                    //have a string for a reference only version of reader.ReadLine(), to avoid executing the function twice
                    string shopItemType = reader.ReadLine();
                    if (shopItemType == "RPGStore.Weapon")
                    {
                        //load a placeholder weapon that the streamreader will overwrite with the data stored in the save file
                        shopInvLoad[i] = new Weapon("null", "null", 0, 0);
                        shopInvLoad[i].LoadItem(reader);
                    }
                    else if (shopItemType == "RPGStore.Potion")
                    {
                        //load a placeholder weapon that the streamreader will overwrite with the data stored in the save file
                        shopInvLoad[i] = new Potion("null", "null", 0);
                        shopInvLoad[i].LoadItem(reader);
                    }
                }
                //set the shopInventory array to the temporary loading array, so that data is loaded in
                shopInventory = shopInvLoad;
                for (int i = 0; i < playerInvLoad.Length; i++)
                {
                    //same junk here, except with a new name for the string in this scope
                    string playerItemType = reader.ReadLine();
                    if (playerItemType == "RPGStore.Weapon")
                    {
                        playerInvLoad[i] = new Weapon("null", "null", 0, 0);
                        playerInvLoad[i].LoadItem(reader);
                    }
                    else if (playerItemType == "RPGStore.Potion")
                    {
                        playerInvLoad[i] = new Potion("null", "null", 0);
                        playerInvLoad[i].LoadItem(reader);
                    }
                }
                //load player inventory from teporary loading array
                playerInventory = playerInvLoad;
                reader.Close();
            }
            else
            {
                Console.WriteLine("After a long bout of dealing with random encounters, long lines of dialogue, and poor game design; you finally turn upon an item shop to heal.");
                Console.ReadKey();
                Console.WriteLine("Entering the place, you are greeted by a mysterious old man in a torchlit cabin. 'Welcome, traveller' says the man, 'may I interst you in some equipment for your journeys?'");
                Console.ReadKey();
                Console.WriteLine("Knowing after the several encounters you've faced along the trails, you tell yourself to accept the man's openness to you and step forward to the counter.");
                Console.ReadKey();
            }
        }
        //two separate variables for item purchases and buybacks 
        //because doing it in the item class will make it readonly
        //cause c sharp really do be like that all the time
        public void ItemPurchase(int index)
        {
            //temp list to store the item in
            Item[] tempList = new Item[playerInventory.Length + 1];
            //add said item to the player inventory
            for (int i = 0; i < playerInventory.Length; i++)
            {
                tempList[i] = playerInventory[i];
            }
            tempList[tempList.Length - 1] = shopInventory[index];
            playerInventory = tempList;
            //remove it from the shop inventory
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
            //this will be largely the same as the ItemPurchase function 
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
