﻿using UnityEngine;

namespace DefaultNamespace
{
    public class Chessman : MonoBehaviour
    {
        public int CurrentX { set; get; }
        public int CurrentY { set; get; }

        public bool isWhite;

        public void SetPosition(int x, int y)
        {
            CurrentX = x;
            CurrentY = y;
        }

        public virtual bool[,] PossibleMove(int x, int y)
        {
            return new bool[8,8];
        }
    }
}