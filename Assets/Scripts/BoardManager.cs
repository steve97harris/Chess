using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5F;

    private int selectionX = -1;
    private int selectionY = -1;

    public List<GameObject> chessmanPrefabs = new List<GameObject>();
    public List<GameObject> activeChessman = new List<GameObject>();

    private Quaternion orientation = Quaternion.Euler(0, 180, 0);

    private void Start()
    {
        SpawnAllChessman();
    }

    private void Update()
    {
        DrawChessboard();
        UpdateSelection();
    }

    private void UpdateSelection()
    {
        if (!Camera.main)
            return;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f,
            LayerMask.GetMask("ChessPlane")))
        {
            selectionX = (int) hit.point.x;
            selectionY = (int) hit.point.z;
        }
        else
        {
            selectionX = -1;
            selectionY = -1;
        }
    }

    private void SpawnChessman(int index, Vector3 position)
    {
        GameObject gameOb = Instantiate(chessmanPrefabs[index], position, orientation) as GameObject;
        gameOb.transform.SetParent(transform);
        activeChessman.Add(gameOb);
    }

    private void SpawnAllChessman()
    {
        activeChessman = new List<GameObject>();
        
        // Spawn White Team:
        // King
        SpawnChessman(0, GetTileCenter(3,0));
        // Queen
        SpawnChessman(1, GetTileCenter(4,0));
        // Rooks
        SpawnChessman(2, GetTileCenter(0,0));
        SpawnChessman(2, GetTileCenter(7,0));
        // Horses
        SpawnChessman(3, GetTileCenter(1,0));
        SpawnChessman(3, GetTileCenter(6,0));
        // Bishop
        SpawnChessman(4, GetTileCenter(2,0));
        SpawnChessman(4, GetTileCenter(5,0));
        // Pawns
        for (int i = 0; i < 8; i++)
        {
            SpawnChessman(5, GetTileCenter(i,1));
        }
        
        // Spawn Black Team:
        // King
        SpawnChessman(6, GetTileCenter(3,7));
        // Queen
        SpawnChessman(7, GetTileCenter(4,7));
        // Rooks
        SpawnChessman(8, GetTileCenter(0,7));
        SpawnChessman(8, GetTileCenter(7,7));
        // Horses
        SpawnChessman(9, GetTileCenter(1,7));
        SpawnChessman(9, GetTileCenter(6,7));
        // Bishop
        SpawnChessman(10, GetTileCenter(2,7));
        SpawnChessman(10, GetTileCenter(5,7));
        // Pawns
        for (int i = 0; i < 8; i++)
        {
            SpawnChessman(11, GetTileCenter(i,6));
        }
    }

    private Vector3 GetTileCenter(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET;
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;
        return origin;
    }

    private void DrawChessboard()
    {
        Vector3 widthLine = Vector3.right * 8;
        Vector3 heightLine = Vector3.forward * 8;

        for (int i = 0; i <= 8; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine);
            
            for (int j = 0; j <= 8; j++)
            {
                start = Vector3.right * j;
                Debug.DrawLine(start, start + heightLine);
            }
        }
        
        // Draw the selection
        if (selectionX >= 0 && selectionY >= 0)
        {
            // Draw diagonal line for each square on board
            Debug.DrawLine(
                Vector3.forward * selectionY + Vector3.right * selectionX, 
                Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));
            Debug.DrawLine(
                Vector3.forward * (selectionY + 1) + Vector3.right * selectionX, 
                Vector3.forward * selectionY + Vector3.right * (selectionX + 1));
        }
    }
}
