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
            Game game = new Game();
            game.ProcessInput();
            Console.ReadKey();
            Console.WriteLine("After exiting, you wonder if you will visit again, and if such, what new items he will have.");
            Console.ReadKey();
        }
    }
}
