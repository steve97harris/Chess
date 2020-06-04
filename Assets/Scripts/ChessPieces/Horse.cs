using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace DefaultNamespace
{
    public class Horse : Chessman
    {
        public override bool[,] PossibleMove(int x, int y)
        {
            var r = new bool[8, 8];
            Chessman c, c2;
            CurrentX = x;
            CurrentY = y;

            // forward
            HorseMove(CurrentX + 1, CurrentY + 2, ref r);
            HorseMove(CurrentX - 1, CurrentY + 2, ref r);
                
            // backward
            HorseMove(CurrentX + 1, CurrentY - 2, ref r);
            HorseMove(CurrentX - 1, CurrentY - 2, ref r);
                
            // right
            HorseMove(CurrentX + 2, CurrentY + 1, ref r);
            HorseMove(CurrentX + 2, CurrentY - 1, ref r);
                
            // left
            HorseMove(CurrentX - 2, CurrentY + 1, ref r);
            HorseMove(CurrentX - 2, CurrentY - 1, ref r);

            return r;
        }

        private void HorseMove(int x, int y, ref bool[,] r)
        {
            Chessman c;
            if (x >= 0 && x < 8 && y >= 0 && y < 8)
            {
                c = BoardManager.Instance.chessmen[x, y];
                if (c == null)
                    r[x, y] = true;
                else if (c.isWhite != isWhite)
                    r[x, y] = true;
            }
        }
    }
}

