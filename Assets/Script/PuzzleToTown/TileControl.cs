using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PuzzleTileType
{
    normal
}
public enum TileWordPosition
{
    Top, Down, Left, Right
}

public class TileControl : MonoBehaviour
{
    //0 - Top, 1 - Down, 2 - Left, 3 - Right
    [SerializeField]private FinishWordDoFunction[] finishWordFunction;
    [SerializeField]private WordManager[] tileWordManagers;
    private WitchGameManager gameManager;

    [SerializeField]private float totalMoveBlock;
    private Vector2 directionMove;
    [SerializeField]private float moveBlockSpeed;
    
    [Header("All The checker of Tile Control")]
    [SerializeField]private bool isAPuzzleTile; //inibuat mastiin apakah ini trmasuk tile puzzle atau walopun trmasuk hitungan tile dia cuma tanah biasa == gabisa digerakin == player gabisa lewat, or probably visualnya diilangin aja 
    private bool isPlayerOnTop;
    [SerializeField]private PuzzleTileType tileType;


    private void Start() 
    {
        
        if(isAPuzzleTile)
        {
            gameManager = WitchGameManager.Instance;
            gameManager.OnChangeToInGame += gameManager_OnChangeToInGame;

            finishWordFunction[0].OnTileMove += finishWordFunction_OnTileMove;
            finishWordFunction[1].OnTileMove += finishWordFunction_OnTileMove;
            finishWordFunction[2].OnTileMove += finishWordFunction_OnTileMove;
            finishWordFunction[3].OnTileMove += finishWordFunction_OnTileMove;
        }
        
    }

    private void gameManager_OnChangeToInGame(object sender, EventArgs e)
    {
        if(gameManager.IsInGameType() == WitchGameManager.InGameType.puzzle)
        {
            gameObject.SetActive(true);
        }
        else{
            gameObject.SetActive(false);
        }
    }

    private void finishWordFunction_OnTileMove(object sender, FinishWordDoFunction.OnTileMoveEventArgs e)
    {
        gameManager.ChangeToCinematic();
        if(e.typeTilePuzzleMoveNow == FinishWordDoFunction.TypeTilePuzzleMove.Top)
        {
            directionMove = new Vector2(0,1);
        }
        else if(e.typeTilePuzzleMoveNow == FinishWordDoFunction.TypeTilePuzzleMove.Down)
        {
            directionMove = new Vector2(0,-1);
        }
        else if(e.typeTilePuzzleMoveNow == FinishWordDoFunction.TypeTilePuzzleMove.Left)
        {
            directionMove = new Vector2(-1,0);
        }
        else if(e.typeTilePuzzleMoveNow == FinishWordDoFunction.TypeTilePuzzleMove.Right)
        {
            directionMove = new Vector2(1,0);
        }
        MoveTile();
    }
    private void MoveTile()
    {
        Vector3 newPosition = new Vector3(transform.position.x + directionMove.x * totalMoveBlock, transform.position.y + directionMove.y* totalMoveBlock, 0f);
        LeanTween.move(this.gameObject, newPosition, moveBlockSpeed).setOnComplete(
            ()=> FinishMovingTile()
        );
    }
    private void FinishMovingTile()
    {
        gameManager.ChangeToInGame(WitchGameManager.InGameType.puzzle);
    }
    public WordManager GetWordManager(TileWordPosition tileWordPosition) //buat dikirim ke wordinput
    {
        if(tileWordPosition == TileWordPosition.Top)
        {
            return tileWordManagers[0];
        }
        else if(tileWordPosition == TileWordPosition.Down)
        {
            return tileWordManagers[1];
        }
        else if(tileWordPosition == TileWordPosition.Left)
        {
            return tileWordManagers[2];
        }
        else if(tileWordPosition == TileWordPosition.Right)
        {
            return tileWordManagers[3];
        }
        return null;
    }

    public bool IsAPuzzleTile()
    {
        return isAPuzzleTile;
    }
    public bool IsPlayerOnTop()
    {
        return isPlayerOnTop;
    }
    public void ChangeIsPlayerOnTop(bool change)
    {
        isPlayerOnTop = change;
    }
    public PuzzleTileType GetPuzzleTileType()
    {
        return tileType;
    }
}
