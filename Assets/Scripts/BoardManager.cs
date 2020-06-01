using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public Chessman[,] chessmen { set; get; }
    private Chessman selectedChessman;
    public static BoardManager Instance { get; set; }
    private bool[][] validMoves { get; set; }
    
    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5F;

    private int selectionX = -1;
    private int selectionY = -1;

    public List<GameObject> chessmanPrefabs = new List<GameObject>();
    public List<GameObject> activeChessman = new List<GameObject>();
    
    public List<GameObject> environmentPrefabs = new List<GameObject>();

    private Quaternion orientation = Quaternion.Euler(0, 180, 0);

    public bool isWhiteTurn = true;

    private Material previousMat;
    public Material selectedMat;
    
    public int[] EnPassantMove { set; get; }
    

    private void Start()
    {
        SpawnAllChessman();
        SpawnEnvironment();
    }

    private void Update()
    {
        DrawChessboard();
        UpdateSelection();

        if (Input.GetMouseButtonDown(0))
        {
            if (selectionX >= 0 && selectionY >= 0)
            {
                if (selectedChessman == null)
                {
                    // if chessman is not selected, select a chessman
                    SelectChessman(selectionX, selectionY);        
                }
                else
                {
                    // if chessman is selected, move chessman
                    MoveChessman(selectionX, selectionY);
                }
            }
        }
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

    private void SpawnChessman(int index, int x, int y)
    {
        var gameOb = Instantiate(chessmanPrefabs[index], GetTileCenter(x,y), orientation) as GameObject;
        gameOb.transform.SetParent(transform);
        
        chessmen[x,y] = gameOb.GetComponent<Chessman>();
        // chessmen[x,y].SetPosition(x,y);
        activeChessman.Add(gameOb);
    }

    private void SpawnAllChessman()
    {
        activeChessman = new List<GameObject>();
        chessmen = new Chessman[8,8];
        
        // Spawn White Team:
        // King
        SpawnChessman(0, 3,0);
        // Queen
        SpawnChessman(1, 4,0);
        // Rooks
        SpawnChessman(2, 0,0);
        SpawnChessman(2, 7,0);
        // Horses
        SpawnChessman(3, 1,0);
        SpawnChessman(3, 6,0);
        // Bishop
        SpawnChessman(4, 2,0);
        SpawnChessman(4, 5,0);
        // Pawns
        for (int i = 0; i < 8; i++)
        {
            SpawnChessman(5, i,1);
        }
        
        // Spawn Black Team:
        // King
        SpawnChessman(6, 3,7);
        // Queen
        SpawnChessman(7, 4,7);
        // Rooks
        SpawnChessman(8, 0,7);
        SpawnChessman(8, 7,7);
        // Horses
        SpawnChessman(9, 1,7);
        SpawnChessman(9, 6,7);
        // Bishop
        SpawnChessman(10, 2,7);
        SpawnChessman(10, 5,7);
        // Pawns
        for (int i = 0; i < 8; i++)
        {
            SpawnChessman(11, i,6);
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
            // Debug.DrawLine(start, start + widthLine);
            
            for (int j = 0; j <= 8; j++)
            {
                start = Vector3.right * j;
                // Debug.DrawLine(start, start + heightLine);
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

    private void SpawnChessboardEnvironment(int index, Vector3 position)
    {
        var obj = Instantiate(environmentPrefabs[index], position, index == 3 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity);
        obj.transform.SetParent(transform);
    }

    private void SpawnEnvironment()
    {
        SpawnChessboardEnvironment(0,new Vector3(4,-0.6f,4));
        SpawnChessboardEnvironment(1,new Vector3(4,-0.38f,4));
        SpawnChessboardEnvironment(2,new Vector3(-0.375f,1.35f,-0.566f));
        SpawnChessboardEnvironment(3,new Vector3(-0.4f,1.35f,8.54f));
    }
    
    private void SelectChessman(int x, int y)
    {
        if (chessmen[x,y] == null)
            return;

        if (chessmen[x,y].isWhite != isWhiteTurn)        // if not white turn, return
            return;

        selectedChessman = chessmen[x,y];
        
    }

    private void MoveChessman(int x, int y)
    {
        if (selectedChessman.PossibleMove(x, y))
        {
            chessmen[selectedChessman.currentX,selectedChessman.currentY] = null;
            selectedChessman.transform.position = GetTileCenter(x, y);
            chessmen[x,y] = selectedChessman;
            isWhiteTurn = !isWhiteTurn;
        }

        selectedChessman = null;
    }
}
