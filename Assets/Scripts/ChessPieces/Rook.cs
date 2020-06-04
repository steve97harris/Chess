using UnityEngine;

namespace DefaultNamespace
{
    public class Rook : Chessman
    {
        public override bool[,] PossibleMove(int x, int y)
        {
            var r = new bool[8, 8];
            Chessman c;
            CurrentX = x;
            CurrentY = y;

            // right
            var i = CurrentX;
            while (true)
            {
                i++;
                if (i >= 8)
                    break;

                c = BoardManager.Instance.chessmen[i, CurrentY];
                if (c == null)
                    r[i, CurrentY] = true;
                else
                {
                    if (c.isWhite != isWhite)
                        r[i, CurrentY] = true;
                    break;
                }
            }

            // left
            i = CurrentX;
            while (true)
            {
                i--;
                if (i < 0)
                    break;

                c = BoardManager.Instance.chessmen[i, CurrentY];
                if (c == null)
                    r[i, CurrentY] = true;
                else
                {
                    if (c.isWhite != isWhite)
                        r[i, CurrentY] = true;
                    break;
                }
            }
            
            // up
            i = CurrentY;
            while (true)
            {
                i++;
                if (i >= 8)
                    break;

                c = BoardManager.Instance.chessmen[CurrentX, i];
                if (c == null)
                    r[CurrentX, i] = true;
                else
                {
                    if (c.isWhite != isWhite)
                        r[CurrentX, i] = true;
                    break;
                }
            }
            
            // down
            i = CurrentY;
            while (true)
            {
                i--;
                if (i < 0)
                    break;

                c = BoardManager.Instance.chessmen[CurrentX, i];
                if (c == null)
                    r[CurrentX, i] = true;
                else
                {
                    if (c.isWhite != isWhite)
                        r[CurrentX, i] = true;
                    break;
                }
            }

            return r;
        }
    }
}