using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Enums;

namespace TicTacToe.Classes
{
    class AIAlgorithmMedium : Interfaces.IAIAlgorithm
    {
        public int GetMove(Marking[] boardValues, Marking playerMarking)
        {
            int currentInput = 0;
            int currentValue = 0;
            for (int i = 0; i < 9; i++)
            {
                if (boardValues[i] != Marking.NONE) continue;
                int tmpValue = 0;
                int row = i / 3;
                int col = i % 3;
                Marking opponentMarking = playerMarking == Marking.CROSS ? Marking.NOUGHT : Marking.CROSS;

                //Check row
                int x = row * 3;
                int val = GetGridValue(boardValues, playerMarking, x, x + 1, x + 2, opponentMarking);
                if (val == 2) //if val == 2 then we can win, so we return it directly
                    return i;
                else
                    tmpValue += val;

                //Check column
                val = GetGridValue(boardValues, playerMarking, col, col + 3, col + 6, opponentMarking);
                if (val == 2)
                    return i;
                else
                    tmpValue += val;

                //Check diagonal \
                if (i == 0 || i == 4 || i == 8)
                {
                    val = GetGridValue(boardValues, playerMarking, 0, 4, 8, opponentMarking);
                    if (val == 2)
                        return i;
                    else
                        tmpValue += val;
                }
                //Check diagonal /
                if (i == 2 || i == 4 || i == 6)
                {
                    val = GetGridValue(boardValues, playerMarking, 2, 4, 6, opponentMarking);
                    if (val == 2)
                        return i;
                    else
                        tmpValue += val;
                }
                if (tmpValue >= currentValue)
                {
                    currentValue = tmpValue;
                    currentInput = i;
                }
            }
            return currentInput;
        }

        private int GetGridValue(Marking[] boardValues, Marking playerMarking, int grid1, int grid2, int grid3, Marking opponentMarking)
        {
            bool isWinnable = !(boardValues[grid1] == opponentMarking || boardValues[grid2] == opponentMarking || boardValues[grid3] == opponentMarking);
            if (isWinnable)
            {
                int tmp = 0;
                if (boardValues[grid1] == playerMarking) tmp++;
                if (boardValues[grid2] == playerMarking) tmp++;
                if (boardValues[grid3] == playerMarking) tmp++;
                return tmp;
            }
            return -1;
        }
    }
}
