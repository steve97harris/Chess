using UnityEngine;

namespace DefaultNamespace
{
    public class King : Chessman
    {
        public override bool[,] PossibleMove(int x, int y)
        {
            var r = new bool[8, 8];
            CurrentX = x;
            CurrentY = y;

            // top side
            var i = CurrentX - 1;
            var j = CurrentY + 1;
            if (CurrentY != 7)
                KingMove(i,j, ref r);
            
            // down side
            i = CurrentX - 1;
            j = CurrentY - 1;
            if (CurrentY != 0)
                KingMove(i,j, ref r);
            
            // mid left 
            if (CurrentX != 0)
            {
                KingMove2(-1, ref r);
            }
            
            // mid right 
            if (CurrentX != 7)
            {
                KingMove2(1, ref r);
            }
            
            return r;
        }

        private void KingMove(int i, int j, ref bool[,] r)
        {
            Chessman c;

            for (int k = 0; k < 3; k++)
            {
                if (i >= 0 || i < 8)
                {
                    c = BoardManager.Instance.chessmen[i, j];
                    if (c == null)
                        r[i, j] = true;
                    else if (isWhite != c.isWhite)
                        r[i, j] = true;
                }

                i++;
            }
        }

        private void KingMove2(int x, ref bool[,] r)
        {
            var c = BoardManager.Instance.chessmen[CurrentX + x, CurrentY];
            if (c == null)
                r[CurrentX + x, CurrentY] = true;
            else if (isWhite != c.isWhite)
                r[CurrentX + x, CurrentY] = true;
        }
    }
}