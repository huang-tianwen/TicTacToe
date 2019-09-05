using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Enums;
using TicTacToe.Interfaces;

namespace TicTacToe.Classes
{
    class Game
    {
        public Game()
        {
            BoardValues = new Marking[9];
        }
        public void Initialize()
        {
            Console.Clear();
            Console.WriteLine("============================================================");
            Console.WriteLine("========================TIC TAC TOE=========================");
            Console.WriteLine("============================================================");

            bool valid = false;
            do
            {
                Console.WriteLine("Choose game mode.");
                Console.WriteLine("1. Human vs Com");
                Console.WriteLine("2. Human vs Human");
                Console.WriteLine("3. Com vs Com");
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
                        Random rnd = new Random();
                        int rand = rnd.Next(0, 2);
                        switch (rand)
                        {
                            case 0:
                                Player1 = new HumanPlayer();
                                Player2 = new AIPlayer();
                                break;
                            case 1:
                                Player1 = new AIPlayer();
                                Player2 = new HumanPlayer();
                                break;
                        }
                        break;
                    case 2:
                        Player1 = new HumanPlayer();
                        Player2 = new HumanPlayer();
                        break;
                    case 3:
                        Player1 = new AIPlayer();
                        Player2 = new AIPlayer();
                        break;
                }
                valid = true;
            } while (!valid);

            Player1.Marking = Marking.CROSS;
            Player2.Marking = Marking.NOUGHT;
            Console.WriteLine("Initialize Player #1");
            Player1.Initialize();
            Console.WriteLine("============================================================");
            Console.WriteLine("Initialize Player #2");
            Player2.Initialize();
            Console.WriteLine("============================================================");
            WriteBoard();
            Turn = 0;
        }

        public int Turn { get; set; }
        public Marking[] BoardValues { get; }
        public IPlayer Player1 { get; set; }
        public IPlayer Player2 { get; set; }

        private void WriteBoard()
        {
            Console.Clear();
            Console.WriteLine("-------------------------    ||    -------------------------");
            Console.WriteLine("|       |       |       |    ||    |       |       |       |");
            Console.WriteLine("|       |       |       |    ||    |   1   |   2   |   3   |");
            Console.WriteLine("|       |       |       |    ||    |       |       |       |");
            Console.WriteLine("|-------+-------+-------|    ||    |-------+-------+-------|");
            Console.WriteLine("|       |       |       |    ||    |       |       |       |");
            Console.WriteLine("|       |       |       |    ||    |   4   |   5   |   6   |");
            Console.WriteLine("|       |       |       |    ||    |       |       |       |");
            Console.WriteLine("|-------+-------+-------|    ||    |-------+-------+-------|");
            Console.WriteLine("|       |       |       |    ||    |       |       |       |");
            Console.WriteLine("|       |       |       |    ||    |   7   |   8   |   9   |");
            Console.WriteLine("|       |       |       |    ||    |       |       |       |");
            Console.WriteLine("-------------------------    ||    -------------------------");
            Console.WriteLine("============================================================");
            Console.WriteLine("Player #1 " + Player1.Name + " = " + (Player1.Marking == Marking.CROSS ? "X" : "O"));
            Console.WriteLine("Player #2 " + Player2.Name + " = " + (Player2.Marking == Marking.CROSS ? "X" : "O"));
            Console.WriteLine("============================================================");
            for (int i = 0;i < 9; i++)
            {
                if(BoardValues[i] != Marking.NONE)
                {
                    int row = i / 3;
                    int col = i % 3;
                    switch (BoardValues[i])
                    {
                        case Marking.NONE:
                            break;
                        case Marking.CROSS:
                            WriteCross(row, col);
                            break;
                        case Marking.NOUGHT:
                            WriteNought(row, col);
                            break;
                    }
                }
            }
            Console.SetCursorPosition(0, 17);
        }
        private void WriteCross(int gridRow, int gridCol)
        {
            int rowPosition = gridRow * 4;
            int colPosition = (gridCol * 8) + 1;
            Console.SetCursorPosition(colPosition, rowPosition + 1);
            Console.Write("  \\ / ");
            Console.SetCursorPosition(colPosition, rowPosition + 2);
            Console.Write("   X  ");
            Console.SetCursorPosition(colPosition, rowPosition + 3);
            Console.Write("  / \\ ");
        }
        private void WriteNought(int gridRow, int gridCol)
        {
            int rowPosition = gridRow * 4;
            int colPosition = (gridCol * 8) + 1;
            Console.SetCursorPosition(colPosition, rowPosition + 1);
            Console.Write("  /-\\ ");
            Console.SetCursorPosition(colPosition, rowPosition + 2);
            Console.Write("  | | ");
            Console.SetCursorPosition(colPosition, rowPosition + 3);
            Console.Write("  \\-/ ");
        }

        public void Start()
        {
            do
            {
                Console.WriteLine("                      Turn #" + (Turn + 1).ToString());
                Console.WriteLine("============================================================");
                //even for Player1 turn, odd for Player2 turn.
                switch (Turn % 2)
                {
                    case 0:
                        Console.WriteLine("Player #1");
                        BoardValues[Player1.GetMove(BoardValues)] = Player1.Marking;
                        break;
                    case 1:
                        Console.WriteLine("Player #2");
                        BoardValues[Player2.GetMove(BoardValues)] = Player2.Marking;
                        break;
                }
                Turn += 1;
                WriteBoard();
            } while (IsOver() == false);
        }
        /// <summary>
        /// Check if the game is over or not. The game is over if the board is filled up or has a winner.
        /// </summary>
        /// <returns></returns>
        private bool IsOver()
        {
            //This action is to print out the winner. Parameter marking is the winner's marking.
            Action<Marking> PrintWinner = delegate (Marking marking)
            {
                IPlayer player = Player1.Marking == marking ? Player1 : Player2;
                Console.WriteLine(player.Name + " WIN.");
                Console.WriteLine("============================================================");
            };

            bool hasWinner = false;
            //Check horizontal or vertical
            for (int i = 0; i < 3; i++)
            {
                int j = i * 3;
                //check rows
                hasWinner = BoardValues[j] != Marking.NONE && BoardValues[j] == BoardValues[j + 1] && BoardValues[j] == BoardValues[j + 2];
                if(hasWinner)
                {
                    PrintWinner(BoardValues[j]);
                    return true;
                }
                //check columns
                hasWinner = BoardValues[i] != Marking.NONE && BoardValues[i] == BoardValues[i + 3] && BoardValues[i] == BoardValues[i + 6];
                if (hasWinner)
                {
                    PrintWinner(BoardValues[i]);
                    return true;
                }
            }

            //Check Diagonal
            hasWinner = BoardValues[0] != Marking.NONE && BoardValues[0] == BoardValues[4] && BoardValues[0] == BoardValues[8];
            if (hasWinner)
            {
                PrintWinner(BoardValues[0]);
                return true;
            }
            hasWinner = BoardValues[2] != Marking.NONE && BoardValues[2] == BoardValues[4] && BoardValues[2] == BoardValues[6];
            if (hasWinner)
            {
                PrintWinner(BoardValues[2]);
                return true;
            }

            // if turn is 9 then and has no winner then the game is draw.
            if (Turn >= 9)
            {
                Console.WriteLine("Game Over.");
                Console.WriteLine("============================================================");
                Console.WriteLine("DRAW.");
                Console.Read();
                return true;
            }
            return false;
        }
    }
}
