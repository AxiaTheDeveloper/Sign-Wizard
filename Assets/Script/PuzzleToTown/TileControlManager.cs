using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileControlManager : MonoBehaviour
{
    private WitchGameManager gameManager;
    [SerializeField]private PlayerSaveManager playerSave;
    public static TileControlManager Instance{get; private set;}
    private WordInput wordInput;
    [SerializeField]private WordGenerator wordGenerator;
    private bool hasPembatasStartSet = false;
    [SerializeField]private PuzzleToTown_Pembatas puzzleToTown_Pembatas_Start;


    [SerializeField]private int levelPuzzle;
    [SerializeField]private bool isPuzzleSolved = false;
    [SerializeField]private int totalRow, totalColumn;
    int totalPuzzleSize;
    [SerializeField]private float tileDistance;
    [SerializeField]private Vector3 tileStartPosition;

    [Header("GAUSA DIISI GAUSA DIISI GAUSA DIISI")]

    [SerializeField]private List<TileControl> tileList = new List<TileControl>();
    [SerializeField]private List<int> isNotAPuzzleTile = new List<int>(); // ini ambil ini tile ke berapa dari list di atas, bukan berdasarkan posisinya
    [SerializeField]private List<WordManager> wordManagerList;

    [SerializeField]private List<TileControl> positionInPuzzleThatHasTile;
    [Header("PERLU DIISI PERLU DIISI PERLU DIISI")]
    [Header("Tolong Posisi ditaro berurutan 0 ke bawah")]
    [Header("Start dari 0 (0,1,2,etc)")]
    [SerializeField]private List<int> EmptyPositionList;
    [Header("Khusus nomor yang ada di ujung kanan or intinya ujung")]
    [Header("yang jadi garis finish karena ga semua finish line")]
    [Header("(bisa aja ketutup pohon), GABOLE SAMA")]
    [SerializeField]private List<int> StartPosition;
    [SerializeField]private List<int> FinishPosition;
    
    [SerializeField]private List<int> positionSaveTemporary;
    
    
    
    private void Awake() 
    {
        Instance = this;
        if(levelPuzzle != playerSave.GetPlayerLevel())
        {
            gameObject.SetActive(false);
        }
        else{
            if(playerSave.GetPlayerLevelMode() == levelMode.finishQuest)
            {
                gameObject.SetActive(false);
            }
            else
            {
                wordGenerator = GetComponent<WordGenerator>();

                positionInPuzzleThatHasTile = new List<TileControl>();
                totalPuzzleSize = totalColumn * totalRow;
                for(int i=0;i<(totalPuzzleSize); i++)
                {
                    positionInPuzzleThatHasTile.Add(null);
                }
                for(int i=0; i<transform.childCount; i++)
                {
                    TileControl tileNow = transform.GetChild(i).GetComponent<TileControl>();
                    tileNow.GiveWordGeneratorToAllWordManager(wordGenerator);
                    if(!tileNow.IsAPuzzleTile()) isNotAPuzzleTile.Add(i);
                    tileNow.HideAllWordInput();

                    tileList.Add(tileNow);
                }
                TilePositionStart();
            }
            
        }
        
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
            if(isPuzzleSolved)
            {
                WordManager[] wordManagers = {};
                wordInput.GetWordManager(wordManagers);
            }
            else{
                GetAllWordManagerAroundEmptyPosition();
            }
            
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
            if(IsTileFinishLine(tileNow.TilePuzzlePositionNow()))
            {
                tileNow.ChangeIsTileToFinish(true);
            }
            else{
                tileNow.ChangeIsTileToFinish(false);
            }
            positionInPuzzleThatHasTile[tileNow.TilePuzzlePositionNow()] = tileNow;

            if(!hasPembatasStartSet)
            {
                foreach(int startPos in StartPosition)
                {
                    if(tileNow.TilePuzzlePositionNow() == startPos)
                    {
                        if(tileNow.CanPlayerStandHere())
                        {
                            puzzleToTown_Pembatas_Start.SetNextPosition(tileNow.transform.localPosition);
                            hasPembatasStartSet = true;
                        }
                    }
                }
            }
        }
    }
    public bool IsTileFinishLine(int positionNow)
    {
        bool isFinishLine = false;
        foreach(int finishPosition in FinishPosition)
        {
            // Debug.Log(finishPosition + " dan " + positionNow);
            if(positionNow == finishPosition)
            {
                
                isFinishLine = true;
                break;
            }
        }
        return isFinishLine;
    }
    public bool IsTileStartLine(int positionNow)
    {
        bool isStartLine = false;
        foreach(int startPosition in StartPosition)
        {
            // Debug.Log(finishPosition + " dan " + positionNow);
            if(positionNow == startPosition)
            {
                isStartLine = true;
                break;
            }
        }
        return isStartLine;
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

            foreach(int notAPuzzleTile in isNotAPuzzleTile)
            {
                //INGET NOT A PUZZLE TILE DICEK DR URUTAN ANAK si tilemanager
                if(tileList[notAPuzzleTile] == positionInPuzzleThatHasTile[TileTopPosition]) TileTopPosition = emptyPosition;
                if(tileList[notAPuzzleTile] == positionInPuzzleThatHasTile[TileDownPosition]) TileDownPosition = emptyPosition;
                if(tileList[notAPuzzleTile] == positionInPuzzleThatHasTile[TileLeftPosition]) TileLeftPosition = emptyPosition;
                if(tileList[notAPuzzleTile] == positionInPuzzleThatHasTile[TileRightPosition]) TileRightPosition = emptyPosition;
            }

            if(TileTopPosition != emptyPosition)
            {
                if(positionInPuzzleThatHasTile[TileTopPosition] != null)
                {
                    if(positionInPuzzleThatHasTile[TileTopPosition].IsMoveable() && !positionInPuzzleThatHasTile[TileTopPosition].IsPlayerOnTop())
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
                    if(positionInPuzzleThatHasTile[TileDownPosition].IsMoveable() && !positionInPuzzleThatHasTile[TileDownPosition].IsPlayerOnTop())
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
                    if(positionInPuzzleThatHasTile[TileLeftPosition].IsMoveable() && !positionInPuzzleThatHasTile[TileLeftPosition].IsPlayerOnTop())
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
                    if(positionInPuzzleThatHasTile[TileRightPosition].IsMoveable() && !positionInPuzzleThatHasTile[TileRightPosition].IsPlayerOnTop())
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
    public int GetTileTopPosition(int positionNow)
    {
        int newPosition = positionNow;
        if(newPosition - totalColumn >= 0) newPosition -= totalColumn;
        return newPosition;
    }
    public int GetTileDownPosition(int positionNow)
    {
        int newPosition = positionNow;
        if(newPosition + totalColumn < totalPuzzleSize) newPosition += totalColumn;
        return newPosition;
    }
    public int GetTileLeftPosition(int positionNow)
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
    public int GetTileRightPosition(int positionNow)
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
        if(IsTileFinishLine(oldEmptyPosition))
        {
            // Debug.Log(positionInPuzzleThatHasTile[oldEmptyPosition] + "finish line");
            positionInPuzzleThatHasTile[oldEmptyPosition].ChangeIsTileToFinish(true);
        }
        else{
            // Debug.Log(positionInPuzzleThatHasTile[oldEmptyPosition] + "not finish line");
            positionInPuzzleThatHasTile[oldEmptyPosition].ChangeIsTileToFinish(false);
        }
        
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
    public bool IsTileNotAPuzzleTile(int tilePosition){
        foreach(int notAPuzzleTile in isNotAPuzzleTile)
        {
            if(tileList[notAPuzzleTile] == positionInPuzzleThatHasTile[tilePosition]) return true;
        }
        return false;
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
    public bool IsPuzzleSolved()
    {
        return isPuzzleSolved;
    }
    public void PuzzleSolved()
    {
        isPuzzleSolved = true;
        PlayerSaveManager.Instance.ChangeIsMagicalBridgeSolve(true);
        WordManager[] wordManagers = {};
        wordInput.GetWordManager(wordManagers);
        //save data posisi puzzle

    }
    public bool CanPlayerStandThisTile(int tilePosition)
    {
        if(positionInPuzzleThatHasTile[tilePosition] == null)
        {
            return false;
        }
        return positionInPuzzleThatHasTile[tilePosition].CanPlayerStandHere();
    }
}
