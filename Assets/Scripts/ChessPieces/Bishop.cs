using UnityEngine;

namespace DefaultNamespace
{
    public class Bishop : Chessman
    {
        public override bool[,] PossibleMove(int x, int y)
        {
            var r = new bool[8,8];
            CurrentX = x;
            CurrentY = y;
            
            BishopMove(1,1, ref r);
            
            BishopMove(-1,-1, ref r);

            BishopMove(1,-1, ref r);

            BishopMove(-1,1, ref r);


            return r;
        }

        private void BishopMove(int x, int y, ref bool[,] r)
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