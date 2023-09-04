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
    [SerializeField]private List<FinishWordDoFunction> finishWordFunction;
    [SerializeField]private List<WordManager> tileWordManagers;
    [SerializeField]private List<GameObject> UIWordInputs;
    private WitchGameManager gameManager;
    private TileControlManager tileControlManager;
    

    [SerializeField]private float totalMoveBlock;
    private Vector2 directionMove;
    [SerializeField]private float moveBlockDuration;
    
    [Header("All The checker of Tile Control")]
    [Header("Ini tandain ini termasuk tile puzzle atau tanah biasa saja")]
    [SerializeField]private bool isAPuzzleTile; //inibuat mastiin apakah ini trmasuk tile puzzle atau walopun trmasuk hitungan tile dia cuma tanah biasa == gabisa digerakin == player gabisa lewat, or probably visualnya diilangin aja 
    [SerializeField]private bool isPlayerOnTop;
    [SerializeField]private PuzzleTileType tileType;
    [Header("Ini tandain PuzzleTilenya bisa digerakkin atau ga (tapi tetep bisa dilewatin)")]
    [SerializeField]private bool isMoveable;
    [Header("Ini tandain PuzzleTilenya, player bisa jalan di sini ga")]
    [SerializeField]private bool canPlayerStandHere;
    [SerializeField]private bool isTileToFinish;//tile yang terakhir + bisa ke tanah, kalo misal tile akhir tp ke pohon kan sama aja boong
    [SerializeField]private int tilePuzzlePositionNow;
    

    private void Awake() 
    {
        tileControlManager = GetComponentInParent<TileControlManager>();
        totalMoveBlock = tileControlManager.GetTileDistance();
    }


    private void Start() 
    {
        if(isAPuzzleTile)
        {
            gameManager = WitchGameManager.Instance;

            finishWordFunction[0].OnTileMove += finishWordFunction_OnTileMove;
            finishWordFunction[1].OnTileMove += finishWordFunction_OnTileMove;
            finishWordFunction[2].OnTileMove += finishWordFunction_OnTileMove;
            finishWordFunction[3].OnTileMove += finishWordFunction_OnTileMove;
        }
    }
    private void finishWordFunction_OnTileMove(object sender, FinishWordDoFunction.OnTileMoveEventArgs e)
    {
        gameManager.ChangeToCinematic();
        if(e.typeTilePuzzleMoveNow == FinishWordDoFunction.TypeTilePuzzleMove.Top)
        {
            int emptyPositionOld = tilePuzzlePositionNow - tileControlManager.GetTotalColumn();
            tileControlManager.UpdateTilePositioninPuzzle(tilePuzzlePositionNow, emptyPositionOld);
            tilePuzzlePositionNow = emptyPositionOld;
            
            directionMove = new Vector2(0,1);
        }
        else if(e.typeTilePuzzleMoveNow == FinishWordDoFunction.TypeTilePuzzleMove.Down)
        {
            int emptyPositionOld = tilePuzzlePositionNow + tileControlManager.GetTotalColumn();
            tileControlManager.UpdateTilePositioninPuzzle(tilePuzzlePositionNow, emptyPositionOld);
            tilePuzzlePositionNow = emptyPositionOld;

            directionMove = new Vector2(0,-1);
        }
        else if(e.typeTilePuzzleMoveNow == FinishWordDoFunction.TypeTilePuzzleMove.Left)
        {
            int emptyPositionOld = tilePuzzlePositionNow - 1;
            tileControlManager.UpdateTilePositioninPuzzle(tilePuzzlePositionNow, emptyPositionOld);
            tilePuzzlePositionNow = emptyPositionOld;

            directionMove = new Vector2(-1,0);
        }
        else if(e.typeTilePuzzleMoveNow == FinishWordDoFunction.TypeTilePuzzleMove.Right)
        {
            int emptyPositionOld = tilePuzzlePositionNow + 1;
            tileControlManager.UpdateTilePositioninPuzzle(tilePuzzlePositionNow, emptyPositionOld);
            tilePuzzlePositionNow = emptyPositionOld;

            directionMove = new Vector2(1,0);
        }
        MoveTile();
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isPlayerOnTop = true;
            PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            playerMovement.ChangePositionNow(tilePuzzlePositionNow);
            if(isTileToFinish && !tileControlManager.IsPuzzleSolved())
            {
                tileControlManager.PuzzleSolved();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isPlayerOnTop = false;
        }
    }
    private void MoveTile()
    {
        Vector3 newPosition = new Vector3(transform.position.x + directionMove.x * totalMoveBlock, transform.position.y + directionMove.y* totalMoveBlock, 0f);
        if(!SoundManager.Instance.isWaterMovePlay())SoundManager.Instance.PlayWaterMoveSound();
        
        LeanTween.move(this.gameObject, newPosition, moveBlockDuration).setOnComplete(
            ()=> FinishMovingTile()
        );
    }
    private void FinishMovingTile()
    {
        SoundManager.Instance.StopWaterMoveSound();
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
    public void ShowWordInput(TileWordPosition tileWordPosition)
    {
        if(tileWordPosition == TileWordPosition.Top)
        {
            tileWordManagers[0].createWord();
            UIWordInputs[0].SetActive(true);
        }
        else if(tileWordPosition == TileWordPosition.Down)
        {
            tileWordManagers[1].createWord();
            UIWordInputs[1].SetActive(true);
        }
        else if(tileWordPosition == TileWordPosition.Left)
        {
            tileWordManagers[2].createWord();
            UIWordInputs[2].SetActive(true);
        }
        else if(tileWordPosition == TileWordPosition.Right)
        {
            tileWordManagers[3].createWord();
            UIWordInputs[3].SetActive(true);
        }
    }
    public void HideAllWordInput()
    {
        foreach(GameObject UIWordInput in UIWordInputs)
        {
            UIWordInput.SetActive(false);
        }
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
    public bool IsMoveable()
    {
        return isMoveable;
    }
    public void ChangeIsMoveable(bool change)
    {
        isMoveable = change;
    }
    public bool CanPlayerStandHere()
    {
        return canPlayerStandHere;
    }
    public bool IsTileToFinish()
    {
        return isTileToFinish;
    }
    public void ChangeIsTileToFinish(bool change)
    {
        isTileToFinish = change;
    }
    public int TilePuzzlePositionNow()
    {
        return tilePuzzlePositionNow;
    }
    public void ChangeTilePuzzlePositionNow(int positionNow)
    {
        tilePuzzlePositionNow = positionNow;
    }
    public void GiveWordGeneratorToAllWordManager(WordGenerator wordGen)
    {
        for(int i=0;i<tileWordManagers.Count;i++)
        {
            tileWordManagers[i].GetWordGenerator(wordGen);
        }
    }
}
