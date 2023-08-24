using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileControlManager : MonoBehaviour
{
    private WitchGameManager gameManager;
    private WordInput wordInput;

    [SerializeField]private bool isPuzzleSolved = false;
    [SerializeField]private int totalRow, totalColumn;
    int totalPuzzleSize;
    [SerializeField]private float tileDistance;
    [SerializeField]private Vector3 tileStartPosition;

    [SerializeField]private List<TileControl> tileList = new List<TileControl>();
    [SerializeField]private List<int> isNotAPuzzleTile = new List<int>(); // ini ambil ini tile ke berapa dari list di atas, bukan berdasarkan posisinya
    [Header("Tolong Posisi ditaro berurutan 0 ke bawah")]
    [Header("Start dari 0 (0,1,2,etc)")]
    [SerializeField]private List<int> EmptyPositionList;
    [SerializeField]private List<int> positionSaveTemporary;
    
    [SerializeField]private List<WordManager> wordManagerList;

    [SerializeField]private List<TileControl> positionInPuzzleThatHasTile;
    
    private void Awake() 
    {
        positionInPuzzleThatHasTile = new List<TileControl>();
        totalPuzzleSize = totalColumn * totalRow;
        for(int i=0;i<(totalPuzzleSize); i++)
        {
            positionInPuzzleThatHasTile.Add(null);
        }
        for(int i=0; i<transform.childCount; i++)
        {
            TileControl tileNow = transform.GetChild(i).GetComponent<TileControl>();
            if(!tileNow.IsAPuzzleTile()) isNotAPuzzleTile.Add(i);
            tileNow.HideAllWordInput();

            tileList.Add(tileNow);
        }
        TilePositionStart();
    }
    private void Start() 
    {
        wordInput = WordInput.Instance;

        gameManager = WitchGameManager.Instance;
        gameManager.OnChangeToCinematic += gameManager_OnChangeToCinematic;
        gameManager.OnChangeToInGame += gameManager_OnChangeToInGame;
    }

    private void gameManager_OnChangeToCinematic(object sender, EventArgs e)
    {
        foreach(TileControl tile in tileList)
        {
            tile.HideAllWordInput();
        }
    }

    private void gameManager_OnChangeToInGame(object sender, EventArgs e)
    {
        if(gameManager.IsInGameType() == WitchGameManager.InGameType.puzzle)
        {
            // Debug.Log("HALOOOOO");
            GetAllWordManagerAroundEmptyPosition();
        }
        else{
            foreach(TileControl tile in tileList)
            {
                tile.HideAllWordInput();
            }
        }
    }

    private void TilePositionStart()
    {
        int positionCounter = 0;
        int emptyPositionList_ArrayNumber = 0;
        for(int i=0; i<tileList.Count; i++)
        {
            TileControl tileNow = tileList[i];
            if(i >= totalPuzzleSize - EmptyPositionList.Count)
            {
                tileNow.gameObject.SetActive(false);
                continue;
            }
            if(isPuzzleSolved)
            {
                tileNow.ChangeTilePuzzlePositionNow(positionSaveTemporary[i]);//ngikut save an file
            }
            else
            {
                if(emptyPositionList_ArrayNumber < EmptyPositionList.Count)
                {
                    if(EmptyPositionList[emptyPositionList_ArrayNumber] == positionCounter)
                    {
                        ++positionCounter;
                        ++emptyPositionList_ArrayNumber;
                    }
                }
                tileNow.ChangeTilePuzzlePositionNow(positionCounter);
                ++positionCounter;
            }
            // Debug.Log(tileNow.TilePuzzlePositionNow());
            tileNow.transform.localPosition = TilePosition_Selector_AtStart(tileNow.TilePuzzlePositionNow());
            positionInPuzzleThatHasTile[tileNow.TilePuzzlePositionNow()] = tileNow;
            // Debug.Log(tileNow.TilePuzzlePositionNow() + " " + i);
        }
    }
    private Vector3 TilePosition_Selector_AtStart(int positionNow)
    {
        int rowNow = 0;

        for(int i = 0; i<totalRow; i++)
        {
            rowNow = totalColumn - 1 + (i * totalColumn);
            if(positionNow <= rowNow)
            {
                rowNow = i;
                break;
            }
        }
        
        float xPosition = tileStartPosition.x + (tileDistance * (positionNow - (rowNow * totalColumn)));
        float yPosition = tileStartPosition.y - (tileDistance * rowNow);
        Vector3 newPosition = new Vector3(xPosition, yPosition, 0f);

        return newPosition;
    }
    private void GetAllWordManagerAroundEmptyPosition()
    {
        wordManagerList.Clear();
        foreach(int emptyPosition in EmptyPositionList)
        {   
            int TileTopPosition = GetTileTopPosition(emptyPosition);
            int TileDownPosition = GetTileDownPosition(emptyPosition);
            int TileLeftPosition = GetTileLeftPosition(emptyPosition);
            int TileRightPosition = GetTileRightPosition(emptyPosition);
            // Debug.Log("Top" + TileTopPosition);
            // Debug.Log("Down" + TileDownPosition);
            // Debug.Log("Left" + TileLeftPosition);
            // Debug.Log("Right" + TileRightPosition);
            foreach(int notAPuzzleTile in isNotAPuzzleTile)
            {
                //INGET NOT A PUZZLE TILE DICEK DR URUTAN ANAK si tilemanager
                if(tileList[notAPuzzleTile] == positionInPuzzleThatHasTile[TileTopPosition]) TileTopPosition = emptyPosition;
                if(tileList[notAPuzzleTile] == positionInPuzzleThatHasTile[TileDownPosition]) TileDownPosition = emptyPosition;
                if(tileList[notAPuzzleTile] == positionInPuzzleThatHasTile[TileLeftPosition]) TileLeftPosition = emptyPosition;
                if(tileList[notAPuzzleTile] == positionInPuzzleThatHasTile[TileRightPosition]) TileRightPosition = emptyPosition;
            }
            // Debug.Log("Top" + TileTopPosition);
            // Debug.Log("Down" + TileDownPosition);
            // Debug.Log("Left" + TileLeftPosition);
            // Debug.Log("Right" + TileRightPosition);
            //Top
            if(TileTopPosition != emptyPosition)
            {
                if(positionInPuzzleThatHasTile[TileTopPosition] != null)
                {
                    if(positionInPuzzleThatHasTile[TileTopPosition].IsMoveAble() && !positionInPuzzleThatHasTile[TileTopPosition].IsPlayerOnTop())
                    {
                        positionInPuzzleThatHasTile[TileTopPosition].ShowWordInput(TileWordPosition.Down);
                        wordManagerList.Add(positionInPuzzleThatHasTile[TileTopPosition].GetWordManager(TileWordPosition.Down));
                    }
                }
            }

            //down
            if(TileDownPosition != emptyPosition)
            {
                
                if(positionInPuzzleThatHasTile[TileDownPosition] != null)
                {
                    if(positionInPuzzleThatHasTile[TileDownPosition].IsMoveAble())
                    {
                        positionInPuzzleThatHasTile[TileDownPosition].ShowWordInput(TileWordPosition.Top);
                        wordManagerList.Add(positionInPuzzleThatHasTile[TileDownPosition].GetWordManager(TileWordPosition.Top));
                    }
                }
            }

            //left
            if(TileLeftPosition != emptyPosition)
            {
                
                if(positionInPuzzleThatHasTile[TileLeftPosition] != null)
                {
                    if(positionInPuzzleThatHasTile[TileLeftPosition].IsMoveAble())
                    {
                        positionInPuzzleThatHasTile[TileLeftPosition].ShowWordInput(TileWordPosition.Right);
                        wordManagerList.Add(positionInPuzzleThatHasTile[TileLeftPosition].GetWordManager(TileWordPosition.Right));
                    }
                }
            }

            //right
            if(TileRightPosition != emptyPosition)
            {
                if(positionInPuzzleThatHasTile[TileRightPosition] != null)
                {
                    if(positionInPuzzleThatHasTile[TileRightPosition].IsMoveAble())
                    {
                        positionInPuzzleThatHasTile[TileRightPosition].ShowWordInput(TileWordPosition.Left);
                        wordManagerList.Add(positionInPuzzleThatHasTile[TileRightPosition].GetWordManager(TileWordPosition.Left));
                    }
                }
            }
        }
        WordManager[] wordManagerArray = new WordManager[wordManagerList.Count]; 
        for(int i=0;i<wordManagerList.Count;i++)
        {
            wordManagerArray[i] = wordManagerList[i];
        }
        wordInput.GetWordManager(wordManagerArray);
    }
    //ngecek posisi atas,bawah kiri kanan dari posisi sekarang itu termasuk dalam tabel ga, tabel itu ya itu templatenya, dia bakal balikin angka yang sama ama positionNow kalo gabisa ke mana-mana
    private int GetTileTopPosition(int positionNow)
    {
        int newPosition = positionNow;
        if(newPosition - totalColumn > 0) newPosition -= totalColumn;
        return newPosition;
    }
    private int GetTileDownPosition(int positionNow)
    {
        int newPosition = positionNow;
        if(newPosition + totalColumn < totalPuzzleSize) newPosition += totalColumn;
        return newPosition;
    }
    private int GetTileLeftPosition(int positionNow)
    {
        bool canGoLeft = true;
        int newPosition = positionNow;
        int rowNow = 0;
        for(int i=0; i<totalRow; i++)
        {
            rowNow = i * totalColumn;
            if(newPosition == rowNow)
            {
                canGoLeft = false;
                break;
            }
        }
        if(canGoLeft)
        {
            --newPosition;
        }
        return newPosition;
    }
    private int GetTileRightPosition(int positionNow)
    {
        bool canGoRight = true;
        int newPosition = positionNow;
        int rowNow = 0;
        for(int i=0; i<totalRow; i++)
        {
            rowNow = i * totalColumn;
            if(newPosition == totalColumn - 1 + rowNow)
            {
                canGoRight = false;
                break;
            }
        }
        // ga mungkin kurang dari totalRow*totalColumn adanya cuma apakah yg sekarang ada di sana itu bukan termasuk puzzle tile dan apakah itu jg kosong
        // if(canGoRight && newPosition == (totalRow * totalColumn) - 1)
        // {
        //     canGoRight = false;
        // }
        if(canGoRight)
        {
            ++newPosition;
        }
        //kalo ga ada apa apa di kanan ato gabisa ke kanan berarti diam di tempat
        return newPosition;
    }
    public void UpdateTilePositioninPuzzle(int oldTilePosition, int oldEmptyPosition)
    {
        positionInPuzzleThatHasTile[oldEmptyPosition] = positionInPuzzleThatHasTile[oldTilePosition];
        positionInPuzzleThatHasTile[oldTilePosition] = null;
        for(int i = 0; i<EmptyPositionList.Count;i++)
        {
            if(EmptyPositionList[i] == oldEmptyPosition)
            {
                EmptyPositionList[i] = oldTilePosition;
                break;
            }
        }
    }
    public int GetTotalRow()
    {
        return totalRow;
    }
    public int GetTotalColumn()
    {
        return totalColumn;
    }
    public float GetTileDistance()
    {
        return tileDistance;
    }
}
