﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DefaultNamespace;
using Photon.Pun;
using UnityEngine;

public class BoardManager : EventManager
{
    public static BoardManager Instance
    {
        set; 
        get;
    }

    private bool[,] allowedMoves
    {
        set; 
        get;
    }

    public Chessman[,] chessmen { set; get; }
    private Chessman selectedChessman;

    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5F;

    private int selectionX = -1;
    private int selectionY = -1;

    public List<GameObject> chessmanPrefabs = new List<GameObject>();
    public List<GameObject> activeChessman = new List<GameObject>();
    
    public List<GameObject> environmentPrefabs = new List<GameObject>();

    private Quaternion orientation = Quaternion.Euler(0, 180, 0);

    public bool isWhiteTurn = true;

    public PhotonView photonView;

    protected void Start()
    {
        Instance = this;
        
        activeChessman = new List<GameObject>();
        chessmen = new Chessman[8,8];
        RegisterListeners();
    }

    protected void Update()
    {
        DrawChessboard();
        UpdateSelection();

        if (Input.GetMouseButtonDown(0))
        {
            if (selectionX >= 0 && selectionY >= 0)
            {
                Debug.LogError("selectionX = " + selectionX);
                Debug.LogError("selectionY = " + selectionY);
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

    private void RegisterListeners()
    {
        Register(EventID.SPAWN_WHITE_TEAM, HandleSpawnWhiteTeam);
        Register(EventID.SPAWN_BLACK_TEAM, HandleSpawnBlackTeam);
    }

    private void UpdateSelection()
    {
        if (!Camera.current)
            return;

        RaycastHit hit;
        if (Physics.Raycast(Camera.current.ScreenPointToRay(Input.mousePosition), out hit, 25.0f,
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
        var gameOb = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Pieces", chessmanPrefabs[index].name), GetTileCenter(x,y), orientation);
        // gameOb.transform.SetParent(transform);
        
        chessmen[x,y] = gameOb.GetComponent<Chessman>();
        chessmen[x,y].SetPosition(x,y);
        activeChessman.Add(gameOb);
    }

    private void SpawnAllChessman()
    {
        SpawnChessTeam(TeamColour.White);
        SpawnChessTeam(TeamColour.Black);
    }
    
    private enum TeamColour
    {
        White,
        Black
    }

    private void HandleSpawnWhiteTeam()
    {
        Debug.LogError("Handle White Team");
        SpawnChessTeam(TeamColour.White);
    }
    
    private void HandleSpawnBlackTeam()
    {
        Debug.LogError("Handle Black Team");
        SpawnChessTeam(TeamColour.Black);
    }
    
    private void SpawnChessTeam(TeamColour teamColour)
    {
        switch (teamColour)
        {
            case TeamColour.White:
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
                break;
            case TeamColour.Black:
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
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(teamColour), teamColour, null);
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
    }

    private void UpdateChessmanMatrix()
    {
        var allActiveChessmen = GameObject.FindGameObjectsWithTag("Chessman");
        if (allActiveChessmen.Length > 0)
        {
            Debug.LogError("Chessmen found in scene");
            for (int i = 0; i < allActiveChessmen.Length; i++)
            {
                var currentX = allActiveChessmen[i].GetComponent<Chessman>().CurrentX;
                var currentY = allActiveChessmen[i].GetComponent<Chessman>().CurrentY;
                chessmen[currentX, currentY] = allActiveChessmen[i].GetComponent<Chessman>();
                Debug.LogError($"{currentX}, {currentY}, {chessmen[currentX, currentY].name}");
            }
        }
    }

    private bool SceneContainsChessmen()
    {
        var allActiveChessmen = GameObject.FindGameObjectsWithTag("Chessman");
        return allActiveChessmen.Length > 0;
    }
    
    private void SelectChessman(int x, int y)
    {
        UpdateChessmanMatrix();
        
        if (chessmen[x, y] == null)
        {
            Debug.LogError($"Chessmen returned null for: {x}, {y}");
            return;
        }

        if (chessmen[x,y].isWhite != isWhiteTurn)        // if not white turn, return
            return;

        var hasAtLeastOneMove = false;
        allowedMoves = chessmen[x, y].PossibleMove(x,y);
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (allowedMoves[i, j])
                    hasAtLeastOneMove = true;
            }
        }       
        if (!hasAtLeastOneMove)
            return;
        
        selectedChessman = chessmen[x,y];
        
        BoardHighlights.Instance.HighlightAllowedMoves(allowedMoves);
    }

    private void MoveChessman(int x, int y)
    {
        if (allowedMoves[x,y])
        {
            var c = chessmen[x, y];
            if (c != null && c.isWhite != isWhiteTurn)
            {
                // Capture chess piece

                if (c.GetType() == typeof(King))    // if King, end game
                {
                    // end game
                    activeChessman.Remove(c.gameObject);
                    Destroy(c.gameObject);
                    
                    // GAME OVER !!
                    GameOver();
                    
                    return;
                }
                activeChessman.Remove(c.gameObject);
                Destroy(c.gameObject);
            }
            
            chessmen[selectedChessman.CurrentX,selectedChessman.CurrentY] = null;
            selectedChessman.transform.position = GetTileCenter(x, y);
            selectedChessman.SetPosition(x,y);
            chessmen[x,y] = selectedChessman;
            isWhiteTurn = !isWhiteTurn;
        }

        BoardHighlights.Instance.HideHighlights();
        selectedChessman = null;
    }

    private void GameOver()
    {
        Debug.Log(isWhiteTurn ? "White Team Wins" : "Black Team Wins");

        foreach (var obj in activeChessman)
        {
            Destroy(obj);
        }

        isWhiteTurn = true;
        BoardHighlights.Instance.HideHighlights();
        
        // TO DO
        // load lobby scene !! 
    }
}
