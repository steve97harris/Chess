﻿using UnityEngine;

namespace DefaultNamespace
{
    public class Chessman : MonoBehaviour
    {
        public int currentX { set; get; }
        public int currentY { set; get; }

        public bool isWhite;

        public void SetPosition(int x, int y)
        {
            currentX = x;
            currentY = y;
        }

        public virtual bool[,] PossibleMove(int x, int y)
        {
            return new bool[8,8];
        }

        public bool Move(int x, int y, ref bool[][] r) // r - result?
        {
            if (x >= 0 && x < 8 && y >= 0 && y < 8)
            {
                var chessman = BoardManager.Instance.chessmen[x,y];
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