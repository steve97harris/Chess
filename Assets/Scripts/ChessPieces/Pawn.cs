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
            CurrentX = x;
            CurrentY = y;
            
            // White team move
            if (isWhite)
            {
                Debug.LogError("currentX = " + CurrentX);
                Debug.LogError("currentY = " + CurrentY);
                // Diagonal Left
                if (CurrentX != 0 && CurrentY != 7)
                {
                    // check for instance of another chessman in position x-1, y+1;
                    c = BoardManager.Instance.chessmen[CurrentX - 1, CurrentY + 1];       
                    if (c != null && !c.isWhite)
                        r[CurrentX - 1, CurrentY + 1] = true;
                }
                
                // Diagonal Right
                if (CurrentX != 7 && CurrentY != 7)
                {
                    c = BoardManager.Instance.chessmen[CurrentX + 1, CurrentY + 1];
                    if (c != null && !c.isWhite)
                        r[CurrentX + 1, CurrentY + 1] = true;
                }
                
                // Middle 
                if (CurrentY != 7)
                {
                    c = BoardManager.Instance.chessmen[CurrentX, CurrentY + 1];
                    if (c == null)
                        r[CurrentX, CurrentY + 1] = true;
                }
                
                // Middle on first move
                if (CurrentY == 1)
                {
                    c = BoardManager.Instance.chessmen[CurrentX, CurrentY + 1];
                    c2 = BoardManager.Instance.chessmen[CurrentX, CurrentY + 2];
                    if (c == null & c2 == null)
                        r[CurrentX, CurrentY + 2] = true;
                }
            }
            else
            {
                // Diagonal Left
                if (CurrentX != 0 && CurrentY != 0)
                {
                    c = BoardManager.Instance.chessmen[CurrentX - 1, CurrentY - 1];
                    if (c != null && c.isWhite)
                        r[CurrentX - 1, CurrentY - 1] = true;
                }
                
                // Diagonal Right
                if (CurrentX != 7 && CurrentY != 0)
                {
                    c = BoardManager.Instance.chessmen[CurrentX + 1, CurrentY - 1];
                    if (c != null && c.isWhite)
                        r[CurrentX + 1, CurrentY - 1] = true;
                }
                
                // Middle 
                if (CurrentY != 0)
                {
                    c = BoardManager.Instance.chessmen[CurrentX, CurrentY - 1];
                    if (c == null)
                        r[CurrentX, CurrentY - 1] = true;
                }
                
                // Middle on first move
                if (CurrentY == 6)
                {
                    c = BoardManager.Instance.chessmen[CurrentX, CurrentY - 1];
                    c2 = BoardManager.Instance.chessmen[CurrentX, CurrentY - 2];
                    if (c == null & c2 == null)
                        r[CurrentX, CurrentY - 2] = true;
                }
            }
            
            return r;
        }
    }
}