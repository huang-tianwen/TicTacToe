using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Enums;

namespace TicTacToe.Classes
{
    class AIAlgorithmEasy : Interfaces.IAIAlgorithm
    {
        public int GetMove(Marking[] boardValues, Marking playerMarking)
        {
            int[] availableGrids = boardValues.Select((v,i) => new { Index = i, Value = v})
                                            .Where(x => x.Value == Marking.NONE)
                                            .Select(x => x.Index)
                                            .ToArray();
            Random rnd = new Random();
            return availableGrids[rnd.Next(availableGrids.Length)];
        }
    }
}
