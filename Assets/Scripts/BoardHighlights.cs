using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;

namespace DefaultNamespace
{
    public class BoardHighlights : MonoBehaviour
    {
        public static BoardHighlights Instance
        {
            set;
            get;
        }

        public GameObject highlightPrefab;
        private List<GameObject> highlights;

        private void Start()
        {
            Instance = this;
            highlights = new List<GameObject>();
        }

        private GameObject GetHighlightObject()
        {
            var gameObj = highlights.Find(g => !g.activeSelf);
            if (gameObj != null) 
                return gameObj;
            
            gameObj = Instantiate(highlightPrefab);
            highlights.Add(gameObj);

            return gameObj;
        }

        public void HighlightAllowedMoves(bool[,] moves)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (!moves[i, j]) 
                        continue;
                    
                    var gameObj = GetHighlightObject();
                    gameObj.SetActive(true);
                    gameObj.transform.position = new Vector3(i + 0.5f,0.01f,j+0.5f);
                }
            }
        }

        public void HideHighlights()
        {
            foreach (var gameObj in highlights)
                gameObj.SetActive(false);
        }
    }
}