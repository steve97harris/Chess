using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Pawn : Chessman
    {
        public override bool[,] PossibleMove(int x, int y)
        {
            var r = new bool[8,8];
            Chessman c, c2;
            currentX = x;
            currentY = y;
            
            // White team move
            if (isWhite)
            {
                Debug.LogError("currentX = " + currentX);
                Debug.LogError("currentY = " + currentY);
                // Diagonal Left
                if (currentX != 0 && currentY != 7)
                {
                    c = BoardManager.Instance.chessmen[currentX - 1, currentY + 1];
                    if (c != null && c.isWhite)
                        r[currentX - 1, currentY + 1] = true;
                }
                
                // Diagonal Right
                if (currentX != 7 && currentY != 7)
                {
                    c = BoardManager.Instance.chessmen[currentX + 1, currentY + 1];
                    if (c != null && c.isWhite)
                        r[currentX + 1, currentY + 1] = true;
                }
                
                // Middle 
                if (currentY != 7)
                {
                    c = BoardManager.Instance.chessmen[currentX, currentY + 1];
                    if (c == null)
                        r[currentX, currentY + 1] = true;
                }
                
                // Middle on first move
                if (currentY == 1)
                {
                    c = BoardManager.Instance.chessmen[currentX, currentY + 1];
                    c2 = BoardManager.Instance.chessmen[currentX, currentY + 2];
                    if (c == null & c2 == null)
                        r[currentX, currentY + 2] = true;
                }
            }
            else
            {
                // Diagonal Left
                if (currentX != 0 && currentY != 0)
                {
                    c = BoardManager.Instance.chessmen[currentX - 1, currentY - 1];
                    if (c != null && !c.isWhite)
                        r[currentX - 1, currentY - 1] = true;
                }
                
                // Diagonal Right
                if (currentX != 7 && currentY != 0)
                {
                    c = BoardManager.Instance.chessmen[currentX + 1, currentY - 1];
                    if (c != null && !c.isWhite)
                        r[currentX + 1, currentY - 1] = true;
                }
                
                // Middle 
                if (currentY != 0)
                {
                    c = BoardManager.Instance.chessmen[currentX, currentY - 1];
                    if (c == null)
                        r[currentX, currentY - 1] = true;
                }
                
                // Middle on first move
                if (currentY == 1)
                {
                    c = BoardManager.Instance.chessmen[currentX, currentY - 1];
                    c2 = BoardManager.Instance.chessmen[currentX, currentY - 2];
                    if (c == null & c2 == null)
                        r[currentX, currentY - 2] = true;
                }
            }
            
            return r;
        }
    }
}