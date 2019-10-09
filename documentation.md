| Patrick Gilfoil |
| :--- |
| s198018 |
| Introduction to C# |
| RPGStore Documentation |

## I. Requirements

1. Description of Problem

    - Name: **RPGStore**
    - Using your knowledge of stores in modern RPGs, create a RPG Store program using the C# language.
    - The program will maintain an array of items for both the store and the player, and allow the player to purchase items from, or sell items to the store. The player will interact through text commands to perform actions with the storekeeper. The inventories and funds of both the player and the store will be saved to and loaded from a text file. The program must also demonstrate multiple levels of inheritance with items from a base 'Item' class. 

2. Input Information
    - In cases of dialogue being output upon first startup and when exiting the shop, any key input is used to progress text.
    - Selections take in string input to select items and options in menus.

3. Output Information
    - Different information is displayed dependent on each action the player performs
        - **Buy Item**: Upon selecting an item, it will show the stats of the weapon including name, description, and cost, before asking the player if they want to buy it.
        - **Sell Item**: Upon selecting an item, it will show the stats of the weapon including name, description, and cost, before asking the player if they want to sell it at a reduced price.
            - If there are no items in the player's inventory, the program will tell the player that they have no items in their inventory.
        - **View Inventory**: Upon selecting an item, the stats of the item are shown including name, description, and cost.

4. UI Information
    - ***Main Menu***: The player's current amount of currency is displayed along with the available options to select from.
        - **Buy Item**: Shows the entirety of the shop's inventory to select from.
            - Item Purchase: Item stats are shown along with a prompt for the player whether they want to purchase the item or not.
        - **Sell Item**: Shows the entirety of the player's inventory to select from.
            - Item Sell: Item stats are show along with a prompt if the player wnats to sell the item at a reduced price from the original cost
        - **View Inventory** - Show's the entirety of the player's inventory for the player to inspect an item.
            - Item Inspection - The Item's stats are shown to the player to view.
        - **Exit Shop** - The game saves and dialogue is shown about the next visit.

## II. Design

### 1. System Architecture

![Diagram of the whole program](Whole_Program_Diagram.png)

### 2. _Player Interface_

Main Menu  
![Main Menu](Main_Menu.png)

Buy Menu  
![Buy Menu](Buy_Menu.png)

Sell Menu  
![Sell Menu](Sell_Menu.png)

View Inventory  
![View Inventory](View_Inventory.png)

Exit Shop  
![Exit Shop](Exit.png)

3. ### Object Information

    ***File:*** Item.cs
    - Description: The base constructor class that contains attributes and functions for both types of items.
    - Attributes
        - Name: _name
            - Description: Variable that stores a string for an item name.
            - Type: protected string

        - Name: _desc
            - Description: Variable that stores a string for an item description.
            - Type: protected string

        - Name: _cost
            - Description: Variable that stores an item cost for purchases.
            - Type: protected int

    - Operations
        - Name: PrintItem
            - Description: Function that prints out all of the variables an item has, vitural function.
            - Visibility: Public

        - Name: GetName
            - Description: Retrieves the name of an item.
            - Return type: string
            - Visibility: Public

        - Name: GetDesc
            - Description: Retrieves the description of an item.
            - Return type: string
            - Visibility: Public

        - Name: GetCost
            - Description: Retrieves the cost of an item.
            - Return type: int
            - Visibility: Public

        - Name: ProcessBuyItem
            - Description: Function that processes input for buying an item.
            - Arguments: string input, ref int buyerMoney, ref int sellerMoney
            - Return type: bool
            - Visibility: Public

        - Name: ProcessSellItem
            - Description: Function that processes input for selling an item.
            - Arguments: string input, ref int, buyerMoney, ref int sellerMoney
            - Return type: bool
            - Visibility: Public

        - Name: SaveItem
            - Description: Function that saves an item to a text file.
            - Arguments: StreamWriter writer
            - Visibility: Public

        - Name: LoadItem
            - Desctiption: Function that loads an item and it's information from a text file.
            - Arguments: StreamReader reader
            - Visibility: Public

    ***File:*** Weapon.cs
    - Description: The constructor class that holds attributes and operations exclusive to Weapons.
    - Attributes
        - Name: _attackModifier
            - Description: The value for a weapon's attack modifier.
            - Type: private int

    - Operations
        - Name: GetAttackModifier()
            - Description: Return type function for attack modifiers, used when saving.
            - Visibility: Public

        - Name: PrintItem()
            - Description: Override function for weapon items to include the attack modifier variable.
            - Visibility: Public

        - Name: SaveItem()
            - Description: Override function for saving weapons during the Save() function in Game.cs to include the weapon's attack modifier value.
            - Arguments: StreamWriter writer
            - Visibility: Public

        - Name: LoadItem()
            - Description: Override function for loading weapons during the Load() function in Game.cs to include the weapon's attack modifier value.
            - Arguments: StreamReader reader

    ***File:*** Potion.cs
    - Description: Constructor class for Potion items, only includes the constructor in the class.

    ***File:*** Game.cs
    - Description: Primary class for most operational functions used to execute the program properly.
    - Attributes
        - Name: playerInventory
            - Description: Item array that stores the player's items.
            - Type: private Item[]

        - Name: shopInventory
            - Description: Item array that stores the shop's items.
            - Type: private Item[]

        - Name: playerFunds
            - Description: Value to store the player's currently available funds, defaults to 200.
            - Type: private int

        - Name: shopFunds
            - Description: Value to store the shop's currently available funds, defaults to 1000.
            - Type: private int

        - Name: claymore
            - Description: One of the standard shop items, being one of two weapons available.
            - Type: Weapon

        - Name: rapier
            - Description: The second of the standard weapons available in the shop.
            - Type: Weapon

        - Name: healPotion
            - Description: One of the standard shop items, being one of two potions available.
            - Type: Potion

        - Name: superPotion
            - Description: The second potion and the last standard item available in the shop by default.

    - Operations:
        - Name: ProcessInput()
            - Description: Function that houses most of the important menus, including the main menu. Runs on program startup.
            - Visibility: Public

        - Name: PrintInventory()
            - Description: Function to print out the whole inventory of the inventory described in the fucntion's argument.
            - Arguments: Item[] inv
            - Visibility: Public

        - Name: SortInventoryByCost()
            - Description: A sorting fucntion primarily used to sort the player's inventory by item cost when they choose to access it.
            - Arguments ref Item[] inventory
            - Visibility: Public

        - Name: ItemTransfer()
            - Description: Function to transfer an item from a dealer array to a recipient array, used after purchases are completed successfully.
            - Arguments: int index, ref Item[] recipient, ref Item[] dealer
            - Visibility: Public

        - Name: SaveState()
            - Description: Save function used after the inventory has been changed/updated or when the program is exited.
            - Arguments: string path
            - Visibility: Public

        - Name: LoadState()
            - Description: Load function for saves, if a save doesn't exist, there will be introductory dialogue at the beginning before going to the menus
            - Arguments: string path
            - Visibility: Public

    ***File:*** Program.cs
    - Description: Class used to start the main game function for the player to run the program properly. Also contains exit dialogue for when closing out the shop using the exit function.
    - Attributes
        - Name: game
            - Description: The program's game to run on startup
            - Type: Game
