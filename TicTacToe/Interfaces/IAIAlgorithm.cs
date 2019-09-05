using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Enums;

namespace TicTacToe.Interfaces
{
    interface IAIAlgorithm
    {
        int GetMove(Marking[] boardValues, Marking playerMarking);
    }
}
