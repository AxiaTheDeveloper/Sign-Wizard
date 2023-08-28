using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private WitchGameManager gameManager;
    [SerializeField]private TileControlManager[] tileControlManagerList;
    private TileControlManager tileControlManager;
    private DialogueManager dialogueManager;

    [SerializeField]private GameInput gameInput;
    [SerializeField]private Rigidbody2D rb;
    private Vector2 keyInput, keyInputPuzzle;

    [Header("This is for Player Movement")]
    [SerializeField]private float speedMovement;

    private bool wasFromOtherInterface = false;
    [Header("This is for Player Movement Puzzle")]
    private bool canWalk = true;
    [SerializeField] private float totalMoveBlock;
    [SerializeField]private float durationMovementPuzzle;
    [SerializeField]private PlayerAnimator playerAnimator;
    [SerializeField]private int playerPuzzlePositionNow;

    private bool isInsidePuzzle;

    private void Start() {
        gameManager = WitchGameManager.Instance;
        if(gameManager.GetOutDoorType() == WitchGameManager.OutDoorType.magicalBridge)
        {
            tileControlManager = tileControlManagerList[PlayerSaveManager.Instance.GetPlayerLevel()-1];
            dialogueManager = DialogueManager.Instance;
        }
        
        canWalk = true;
    }


    private void Update() {
        if(gameManager.IsInGame()){
            if(!wasFromOtherInterface){
                if(gameManager.IsInGameType() == WitchGameManager.InGameType.normal)
                {
                    keyInput = gameInput.GetInputMovement();
                    if(keyInputPuzzle != Vector2.zero)
                    {
                        keyInputPuzzle = Vector2.zero;
                    }
                }
                else if(gameManager.IsInGameType() == WitchGameManager.InGameType.puzzle)
                {
                    if(keyInput != Vector2.zero)
                    {
                        keyInput = Vector2.zero;
                        PlayerWalk();
                    }
                    if(gameInput.GetInputMovementPuzzle() != Vector2.zero)
                    {
                        keyInputPuzzle = gameInput.GetInputMovementPuzzle();
                        if(canWalk)
                        {
                            //checker kalo ke arah sini itu tuh bisa dilewatin ga
                            if(CanMove())
                            {
                                canWalk = false;
                                PlayerMoveInPuzzle();
                                gameManager.ChangeToCinematic();
                            }
                            
                        }
                    }
                    
                }
                
            }
            else if(wasFromOtherInterface && gameInput.GetInputMovement() != Vector2.zero){
                wasFromOtherInterface = false;
            }   
            
        }
        else{
            if(keyInput != Vector2.zero){
                keyInput = Vector2.zero;
                PlayerWalk();
                
            }
            if(keyInputPuzzle != Vector2.zero){
                keyInputPuzzle = Vector2.zero;
            }
            if(!wasFromOtherInterface){
                wasFromOtherInterface = true;
            }
            
        }
    }
    private void FixedUpdate() {
        if(gameManager.IsInGameType() == WitchGameManager.InGameType.normal) PlayerWalk();
    }
    private void PlayerWalk(){
        rb.MovePosition(rb.position + keyInput * speedMovement * Time.fixedDeltaTime);
    }

    public Vector2 GetKeyInput(){
        return keyInput;
    }
    public float GetKeyInputX(){
        return keyInput.x;
    }

    private void PlayerMoveInPuzzle()
    {
        Vector3 playerMovePosition = new Vector3(transform.position.x + keyInputPuzzle.x * totalMoveBlock, transform.position.y + keyInputPuzzle.y * totalMoveBlock, 0f);
        playerAnimator.PlayAnimatorWhileMovingPuzzle(keyInputPuzzle);
        LeanTween.move(this.gameObject, playerMovePosition, durationMovementPuzzle).setOnComplete(
            ()=> FinishMoving()
        );
    }

    private void FinishMoving()
    {
        if(isInsidePuzzle)
        {
            gameManager.ChangeToInGame(WitchGameManager.InGameType.puzzle);
        }
        else{
            gameManager.ChangeToInGame(WitchGameManager.InGameType.normal);
        }
        
        canWalk = true;
        playerAnimator.PlayAnimatorWhileMovingPuzzle(Vector2.zero);
    }
    private bool CanMove()
    {
        int newPosition = 0;
        if(keyInputPuzzle.y == 1)
        {
            newPosition = tileControlManager.GetTileTopPosition(playerPuzzlePositionNow);
        }
        else if(keyInputPuzzle.y == -1)
        {
            newPosition = tileControlManager.GetTileDownPosition(playerPuzzlePositionNow);
        }
        else if(keyInputPuzzle.x == 1)
        {
            newPosition = tileControlManager.GetTileRightPosition(playerPuzzlePositionNow);
        }
        else if(keyInputPuzzle.x == -1)
        {
            newPosition = tileControlManager.GetTileLeftPosition(playerPuzzlePositionNow);
        }
        if(playerPuzzlePositionNow != newPosition)
        {
            if(!tileControlManager.IsTileNotAPuzzleTile(newPosition) && tileControlManager.CanPlayerStandThisTile(newPosition))
            {
                isInsidePuzzle = true;
                return true;
            }
            else{
                playerAnimator.PlayAnimatorWhileMovingPuzzle(keyInputPuzzle);
                playerAnimator.PlayAnimatorWhileMovingPuzzle(Vector2.zero);
                dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.tidakBisaGerakKeArahSana_ForPuzzle_PlayerMovement);
                return false;
            }
        }
        else{
            if(tileControlManager.IsTileFinishLine(newPosition) || tileControlManager.IsTileStartLine(newPosition))
            {
                isInsidePuzzle = false;
                return true;
            }
            else{
                playerAnimator.PlayAnimatorWhileMovingPuzzle(keyInputPuzzle);
                playerAnimator.PlayAnimatorWhileMovingPuzzle(Vector2.zero);
                dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.tidakBisaGerakKeArahSana_ForPuzzle_PlayerMovement);
                return false;
            }
            
        }
    }

    public void ChangePositionNow(int positionNow)
    {
        playerPuzzlePositionNow = positionNow;
    }
    public int GetPositionNow()
    {
        return playerPuzzlePositionNow;
    }


}
