using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGStore
{
    class Program
    {
        static void Main(string[] args)
        {
            //create a new game
            Game game = new Game();

            //have the menu show up
            game.ProcessInput();

            //after exiting, print a new line of dialogue about future visits
            Console.ReadKey();
            Console.WriteLine("After exiting, you wonder if you will visit again, and if such, what new items he will have.");
            Console.ReadKey();
        }
    }
}
