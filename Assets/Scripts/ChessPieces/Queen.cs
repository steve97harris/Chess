using UnityEngine;

namespace DefaultNamespace
{
    public class Queen : Chessman
    {
        public override bool[,] PossibleMove(int x, int y)
        {
            var r = new bool[8,8];
            CurrentX = x;
            CurrentY = y;
            Chessman c;

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
            
            BishopMoveForQueen(1,1, ref r);
            BishopMoveForQueen(-1,-1, ref r);
            BishopMoveForQueen(1,-1, ref r);
            BishopMoveForQueen(-1,1, ref r);
            
            
            return r;
        }
        
        private void BishopMoveForQueen(int x, int y, ref bool[,] r)
        {
            Chessman c;
            
            var i = CurrentX;
            var j = CurrentY;
            while (true)
            {
                i += x;
                j += y;
                if (i >= 8 || j >= 8 || i < 0 || j < 0)
                    break;

                c = BoardManager.Instance.chessmen[i, j];
                if (c == null)
                    r[i, j] = true;
                else
                {
                    if (c.isWhite != isWhite)
                        r[i, j] = true;
                    break;
                }
            }
        }
    }
}