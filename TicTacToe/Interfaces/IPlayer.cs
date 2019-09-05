using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Enums;

namespace TicTacToe.Interfaces
{
    interface IPlayer
    {
        string Name { get; set; }
        Marking Marking { get; set; }
        int GetMove(Marking[] boardValues);
        void Initialize();
    }
}
