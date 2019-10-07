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
                            //set up a bool as a return of the ProcessBuyItem() function
                            //the transaction is done in that function
                            bool bought = shopInventory[e].ProcessBuyItem(input, ref playerFunds, ref shopFunds);
                            //if the users says yes to buying the item
                            if (bought == true && playerFunds < shopInventory[e].GetCost())
                            {
                                //dont make a transaction if the player has no money
                                Console.WriteLine("'You're quite low on cash to pay for this.'");

                            }
                            else if (bought == true)
                            {
                                //give the player the item
                                ItemTransfer(e, ref playerInventory, ref shopInventory);
                                Console.WriteLine("'Very much a pleasure doing business with thee.'");
                                SaveState("save.txt");
                            }
                        }
                    }
                }
                //selling items
                else if (input.ToLower() == "sell item" || input.ToLower() == "sell")
                {
                    if (playerInventory.Length == 0)
                    {
                        Console.WriteLine();
                        //prints this if you have nothing in your inventory
                        Console.WriteLine("'My friend you have nothing!'");
                        satisfied = false;
                    }
                    else
                    {
                        //show the player inventory, then read next line for input
                        Console.WriteLine();
                        Console.WriteLine("'Let's have a look-see at your inventory shall we?'");
                        SortPlayerInventoryByCost(playerInventory);
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
                                //another bool is set up here mimicking the purpose of the one for buying except for being for selling
                                //like with buying, the selling function does the transaction
                                bool sold = playerInventory[e].ProcessSellItem(input, ref shopFunds, ref playerFunds);
                                //ask if they want to sell for 85% of the original price
                                if (sold == true && shopFunds < playerInventory[e].GetCost())
                                {
                                    //if the shopkeeper doesn't have enough funds, then don't make the sale
                                    Console.WriteLine("'I myself do not have enough money to take this in.'");
                                }
                                else if (sold == true)
                                {
                                    //goods transfer
                                    ItemTransfer(e, ref shopInventory, ref playerInventory);
                                    Console.WriteLine("'This will make a fine addition for my customers.'");
                                    SaveState("save.txt");
                                }
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
                        //again just print this instead of showing literally nothing
                        Console.WriteLine("'My friend you have nothing!'");
                        satisfied = false;
                    }
                    else
                    {
                        Console.WriteLine();
                        //we're being straightforward with the user input
                        Console.WriteLine("Type in the name of an item to view, or type 'exit' to exit.");
                        bool viewingItems = true;
                        while (viewingItems)
                        {
                            Console.WriteLine();
                            SortPlayerInventoryByCost(playerInventory);
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
                /*else if (input.ToLower() == "onomatopoeia")
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
                }*/
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
                    //display this if the user input is invalid
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

        //sort the player's inventory by cost of the item
        public void SortPlayerInventoryByCost(Item[] playerInventory)
        {
            bool sorted = false;
            while (!sorted)
            {
                sorted = true;
                for (int i = 0; i < playerInventory.Length - 1; i++)
                {
                    //goes by highest cost to lowest cost
                    if (playerInventory[i].GetCost() < playerInventory[i + 1].GetCost())
                    {
                        //store on item as a temporary item variable
                        Item tempItem = playerInventory[i];
                        //replace one item with the other
                        playerInventory[i] = playerInventory[i + 1];
                        //take the temp item and replace the other item with it
                        playerInventory[i + 1] = tempItem;
                        sorted = false;
                    }
                }
            }
        }

        //save game function, calls upon SaveItem() for each item
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

        //load game function, only used at startup
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
                    //have a string for a reference only string from executing reader.ReadLine() 
                    //this is done to avoid executing the function twice in the if/else statements
                    string shopItemType = reader.ReadLine();
                    if (shopItemType == "RPGStore.Weapon")
                    {
                        //load a placeholder weapon that the streamreader will overwrite with the data stored in the save file
                        //we do this otherwise a null object reference exception will popup, preventing anything from loading
                        shopInvLoad[i] = new Weapon("null", "null", 0, 0);
                        shopInvLoad[i].LoadItem(reader);
                    }
                    else if (shopItemType == "RPGStore.Potion")
                    {
                        //same thing with the weapons, except this will overrite for potions
                        shopInvLoad[i] = new Potion("null", "null", 0);
                        shopInvLoad[i].LoadItem(reader);
                    }
                }
                //set the shopInventory array to the temporary loading array, so that data is loaded in
                shopInventory = shopInvLoad;
                for (int i = 0; i < playerInvLoad.Length; i++)
                {
                    //same junk here, except with the player's inventory
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
                //I wanted to include some lines of text as a "first-time greeting" sort of situation
                Console.WriteLine("After a long bout of dealing with random encounters, long lines of dialogue, and poor game design; you finally turn upon an item shop to heal.");
                Console.ReadKey();
                Console.WriteLine("Entering the place, you are greeted by a mysterious old man in a torchlit cabin. 'Welcome, traveller' says the man, 'may I interst you in some equipment for your journeys?'");
                Console.ReadKey();
                Console.WriteLine("Knowing after the several encounters you've faced along the trails, you tell yourself to accept the man's openness to you and step forward to the counter.");
                Console.ReadKey();
            }
        }

        //function for transferring the items to the different inventories
        //ref item arrays are used so it directly references the arrays to be used
        public void ItemTransfer(int index, ref Item[] recipient, ref Item[] dealer)
        {
            //temp list to store the item in
            Item[] tempList = new Item[recipient.Length + 1];
            //add said item to the player inventory
            for (int i = 0; i < recipient.Length; i++)
            {
                tempList[i] = recipient[i];
            }
            tempList[tempList.Length - 1] = dealer[index];
            recipient = tempList;
            //remove it from the shop inventory
            tempList = new Item[dealer.Length - 1];
            int newPos = 0;
            for (int i = 0; i < dealer.Length; i++)
            {
                if (i != index)
                {
                    tempList[newPos] = dealer[i];
                    newPos++;
                }
            }
            dealer = tempList;
        }
    }
}
