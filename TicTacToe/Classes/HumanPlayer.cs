using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Enums;

namespace TicTacToe.Classes
{
    class HumanPlayer : Interfaces.IPlayer
    {
        public void Initialize()
        {
            Console.WriteLine("Set your name: ");
            Name = Console.ReadLine();
        }

        public Marking Marking { get; set; }
        public string Name { get; set; }

        public int GetMove(Marking[] boardValues)
        {
            Console.SetCursorPosition(0, 20);
            int move = 0;
            bool valid = false;
            do
            {
                Console.WriteLine("Input your move (1-9): ");
                string input = Console.ReadLine();
                int.TryParse(input, out move);
                valid = move >= 1 && move <= 9;
                if (!valid)
                {
                    Console.WriteLine("Out of range. The input must be between 1 and 9.");
                    valid = false;
                    continue;
                }
                valid = boardValues[move - 1] == Marking.NONE;
                if(!valid)
                {
                    Console.WriteLine("The grid already filled.");
                    valid = false;
                    continue;
                }
                boardValues[move - 1] = Marking;
                valid = true;
            } while (!valid);
            return move - 1;
        }
    }
}
