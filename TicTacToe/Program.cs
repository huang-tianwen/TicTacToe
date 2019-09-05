using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Classes;

namespace TicTacToe
{
    class Program
    {
        public static void Main(string[] args)
        {
            Game game = new Game();
            game.Initialize();
            game.Start();
            Console.Read();
        }
    }
}
