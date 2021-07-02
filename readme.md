This was initially created as part of the Introduction to C# assessment during the first year at AIE. As such, it is very basic.


### HOW TO RUN
First, you must double click on the program labeled "RPGStore.exe"
Once opened for the first time, a short monologue will be printed out within the command window.
To progress through the dialoge you must press a key on your keyboard. It isn't specific on which key you press, but it will wait on any key to be pressed to continue. 
The opening monologue is skipped if a proper save file is present.
After the beginning monologue ends, the program will then begin to ask you for some specified inputs correlating to their action "(Buy Item/Sell Item/View Inventory/Exit Shop)" 
You can either type out the whole phrase or you can use "Buy", "Sell", "Inventory", or "Exit" respectively. 
All input commands are not case sensitive, but they must be spelled correctly in order for the program to recognize it. Press enter for the program to recognize the input

![Main Menu](https://raw.githubusercontent.com/patgilfoil/RPGStore/master/Main_Menu.PNG)

## Buying items
  ![Buy Menu](https://raw.githubusercontent.com/patgilfoil/RPGStore/master/Buy_Menu.PNG)

	If you chose to buy an item, the shop's inventory will be listed out to you. You can then type out the name of your desired item to select it for purchase. 
	Much like the main menu commands, it is not case sensitive but it is spelling sensitive.
	Once an item is selected, it displays all information about that item and will ask you if you want to buy it or not. It is a "Yes" or "No" input.
	If you typed yes, the item will be purchased for the cost described and the item will be moved into your inventory, and it will return to the main menu.
	If you typed yes and do NOT have enough money to purchase the item, or if you typed no, the item will not be purchased.

## Selling items
  ![Sell Menu](https://github.com/patgilfoil/RPGStore/blob/master/Sell_Menu.PNG)
  
	If you chose to buy an item, your inventory will be shown to you. You can then select the item from your inventory you want to sell.
	The program will then ask if you want to sell it for an amount less than that of the normal buying price. This is a Yes or No input.
	If answered yes, you will receive the amount of money the item sells for and the item will be moved to the shop's inventory.
	If answered no, then the program will go back to the main menu without any item being sold
	If there is nothing in your inventory, then you are told that you have nothing and the program will take you to the main menu.

## Viewing your inventory
  ![View Inventory](https://github.com/patgilfoil/RPGStore/blob/master/View_Inventory.PNG?raw=true)
  
	If you choose to view your inventory, all of your items in your current inventory are shown. You can then select an item you wish to view the stats of.
	Selecting an item will show the name, description, cost (and, if the item is a weapon, the attack modifier) of the item you selected.
	To stop viewing an item, you can press any key to go back to your inventory.
	If you wish to exit back to the main menu, simply type "exit" then pres enter, and the program will take you back to the main menu.

## Exiting the game
  ![Exit Shop](https://github.com/patgilfoil/RPGStore/blob/master/Exit.PNG?raw=true)
  
	You can use Windows' regular exit button located at the top right of the program window, but it is generally reccommended you exit from the main menu.
	To exit the program this way. Type "Exit Shop" or "Exit" and press enter.
	While exiting, there will be dialogue about future visits to the shop. Much like the beginning monologue, any key can be pressed to progress through the text.

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

## Developer Menu
	A secret menu for created item is hidden in the game, but for all intents and purposes, the way to access it is by typing "onomatopoeia" and pressing enter on the main menu.
	The program will ask you the item you want to create, whether it be a weapon or a potion. Select the item you wish to create by entering the type of item you wish to make.
	Once selecting an item, the program will begin asking you for each of the weapons stats. 
	For names and descriptions, anything you type will be accepted by the program. 
	For item costs and weapon attack modifiers, they must be a number value, otherwise the program will not accept it, and return you to creating the item.
	Upon entering values for each of the weapon stats, the program will ask you if you are satisfied with the item.
	If you type "yes" and press enter, then the item will be created and then added to the shop.
	If you type "no" and press enter, then the program will start over from the beginning to create an item.
