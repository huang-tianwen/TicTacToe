using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Enums;
using TicTacToe.Interfaces;

namespace TicTacToe.Classes
{
    class AIPlayer : IPlayer
    {
        private IAIAlgorithm AIAlgorithm { get; set; }
        public void Initialize()
        {
            bool valid = false;
            do
            {
                Console.WriteLine("Choose the bot Intelligence Level.");
                Console.WriteLine("1. Easy (Random Move)");
                Console.WriteLine("2. Medium (Know how to win, but don't know how to prevent lose)");
                Console.WriteLine("3. Hard (Know how to prevent lose)");
                Console.WriteLine("Input your choice:");
                string input = Console.ReadLine();
                int choice = 0;
                int.TryParse(input, out choice);
                valid = choice >= 1 && choice <= 3;
                if (!valid)
                {
                    Console.WriteLine("Out of range. The input must be between 1 and 3.");
                    valid = false;
                    continue;
                }
                switch (choice)
                {
                    case 1:
                        AIAlgorithm = new AIAlgorithmEasy();
                        break;
                    case 2:
                        AIAlgorithm = new AIAlgorithmMedium();
                        break;
                    case 3:
                        AIAlgorithm = new AIAlgorithmHard();
                        break;
                }
                valid = true;
            } while (!valid);

            Random rnd = new Random();
            int rand = rnd.Next(1, 100);
            Console.WriteLine("");
            Console.WriteLine("Generate name... ");
            System.Threading.Thread.Sleep(1000);
            Name = "Com #" + rand.ToString();
            Console.WriteLine(Name);
            System.Threading.Thread.Sleep(1000);
        }
        public Marking Marking { get; set; }
        public string Name { get; set; }

        public int GetMove(Marking[] boardValues)
        {
            System.Threading.Thread.Sleep(1500);
            return AIAlgorithm.GetMove(boardValues, Marking);
        }
    }
}
