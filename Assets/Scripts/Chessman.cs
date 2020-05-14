namespace DefaultNamespace
{
    public class Chessman
    {
        public int currentX { get; set; }
        public int currentY { get; set; }

        public bool isWhite;

        public void SetPosition(int x, int y)
        {
            currentX = x;
            currentY = y;
        }

        public virtual bool[][] PossibleMoves()
        {
            return new bool [8][];
        }

        public bool Move(int x, int y, ref bool[][] r) // r - result?
        {
            if (x >= 0 && x < 8 && y >= 0 && y < 8)
            {
                var chessman = BoardManager.Instance.chessmen[x][y];
                if (chessman == null)
                    r[x][y] = true;
                else
                {
                    if (isWhite != chessman.isWhite)
                        r[x][y] = true;
                    return true;
                }
            }

            return false;
        }
    }
}