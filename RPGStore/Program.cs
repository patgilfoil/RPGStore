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
            //Console.WriteLine("Whoops! Nothing yet!");

            Game game = new Game();
            game.ProcessInput();
            Console.ReadKey();
        }
    }
}
