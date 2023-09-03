using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]private WitchGameManager gameManager;
    [SerializeField]public Animator animator;
    private Vector2 keyInput, lastKeyInput, keyInputPuzzle;


    private float lastSaveHorizontal, lastSaveVertical;
    private bool wasFromOtherInterface = false, wasDiagonal = false;
    [SerializeField]private GameInput gameInput;
    [SerializeField]private float maxDiagonalChecker;
    private float diagonalChecker;
    private SoundManager soundManager;
    private WitchGameManager.InGameType LastInGameType;
    private void Start() {
        if(gameManager == null)gameManager = WitchGameManager.Instance;
        if(PlayerSaveManager.Instance.GetPlayerLevel() == 1 && PlayerSaveManager.Instance.GetPlayerLevelMode() == levelMode.outside && gameManager.GetOutDoorType() == WitchGameManager.OutDoorType.inFrontOfHouse) 
        {
            animator.Play("Player_Idle_Up");
            animator.SetBool("idle", true);
        }
        
        soundManager = SoundManager.Instance;
        
    }

    private void Update() {
    

        if(WitchGameManager.Instance.IsInGame()){
            if(!wasFromOtherInterface){
                if(gameManager.IsInGameType() == WitchGameManager.InGameType.normal)
                {
                    LastInGameType = gameManager.IsInGameType();
                    keyInput = gameInput.GetInputMovement();
                    if(keyInputPuzzle != Vector2.zero){
                        keyInputPuzzle = Vector2.zero;
                    }
                }
                
                else if(gameManager.IsInGameType() == WitchGameManager.InGameType.puzzle)
                {
                    LastInGameType = gameManager.IsInGameType();
                    if(keyInput != Vector2.zero){
                        soundManager.StopSFX_PlayerWalk();
                        keyInput = Vector2.zero;
                        animator.SetBool("idle", true);
                    }
                    keyInputPuzzle = gameInput.GetInputMovementPuzzle();
                }
            }
            else if(wasFromOtherInterface && gameInput.GetInputMovement() != Vector2.zero)
            {
                wasFromOtherInterface = false;
            } 
              
            
        }
        else{
            if(keyInput != Vector2.zero){
                keyInput = Vector2.zero;
                
            }
            if(keyInputPuzzle != Vector2.zero){
                keyInputPuzzle = Vector2.zero;
            }
            if(!wasFromOtherInterface){
                wasFromOtherInterface = true;
            }
            
        }
        /*
        X  Y
        0  0 = Not Moving
        0  1 = Move Up
        0 -1 = Move Down
        1  1 = Move Top Right
        1  0 = Move Right
        1 -1 = Move Bottom Right
        -1  0 = Move Left
        -1 -1 = Move Bottom Left
        -1  1 = Move Top Left
        
        */
        if(gameManager.IsInGameType() != WitchGameManager.InGameType.puzzle)
        {
            if (keyInput.x > 0) keyInput.x = 1;
            if (keyInput.y > 0) keyInput.y = 1;
            if (keyInput.x < 0) keyInput.x = -1;
            if (keyInput.y < 0) keyInput.y = -1;
            if(keyInput != Vector2.zero){
                if(!soundManager.isPlayedSFX_PlayerWalk()){
                    soundManager.PlaySFX_PlayerWalk();
                }
            }
            else{
                if(gameManager.IsInGame())
                {
                    soundManager.StopSFX_PlayerWalk();
                }
                else{
                    if(LastInGameType == WitchGameManager.InGameType.normal)
                    {
                        soundManager.StopSFX_PlayerWalk();
                    }
                }
                
            }
            if(keyInput.y != 0 && keyInput.x != 0){
                wasDiagonal = true;
                lastKeyInput = keyInput;
                diagonalChecker = maxDiagonalChecker;
            }
            else if((keyInput.y != 0 && keyInput.x == 0) || (keyInput.y == 0 && keyInput.x != 0)){
                if(wasDiagonal){
                    if(((keyInput.y != 0 && keyInput.x == 0) && keyInput.y == lastKeyInput.y) || ((keyInput.x != 0 && keyInput.y == 0) && keyInput.x == lastKeyInput.x)){
                        if(diagonalChecker > 0){
                            diagonalChecker -= Time.deltaTime;
                            keyInput = lastKeyInput;
                        }
                        else{
                            wasDiagonal = false;
                        }
                    }
                    else{
                        wasDiagonal = false;
                    }
                    
                }
            }

            switch (keyInput.x, keyInput.y)
            {   
                case (0, 0):
                    if(!wasDiagonal){
                        if(LastInGameType == WitchGameManager.InGameType.normal) animator.SetBool("idle", true);
                        
                    }
                    else{
                        wasDiagonal = false;
                        if(lastKeyInput.x == 1 && lastKeyInput.y == 1){
                            if(LastInGameType == WitchGameManager.InGameType.normal)
                            {
                                animator.Play("Player_Idle_Up_Right");
                                animator.SetBool("idle", true);
                            }
                            
                        }
                        else if(lastKeyInput.x == 1 && lastKeyInput.y == -1){
                            if(LastInGameType == WitchGameManager.InGameType.normal)
                            {
                                animator.Play("Player_Idle_Right");
                                animator.SetBool("idle", true);
                            }
                            
                        }
                        else if(lastKeyInput.x == -1 && lastKeyInput.y == 1){
                            if(LastInGameType == WitchGameManager.InGameType.normal)
                            {
                                animator.Play("Player_Idle_Up_Left");
                                animator.SetBool("idle", true);
                            }
                            
                        }
                        else if(lastKeyInput.x == -1 && lastKeyInput.y == -1){
                            if(LastInGameType == WitchGameManager.InGameType.normal)
                            {
                                animator.Play("Player_Idle_Left");
                                animator.SetBool("idle", true);
                            }
                            
                            
                        }
                    }
                    
                    break;

                case (0, 1):
                    animator.Play("Player_Walk_Up");
                    animator.SetBool("idle", false);
                    break;

                case (0, -1):
                    animator.Play("Player_Walk_Down");
                    animator.SetBool("idle", false);
                    break;

                case (1, 1):
                    animator.Play("Player_Walk_Up_Right");
                    animator.SetBool("idle", false);
                    break;

                case (1, 0):
                    animator.Play("Player_Walk_Right");
                    animator.SetBool("idle", false);
                    break;

                case (1, -1):
                    animator.Play("Player_Walk_Right");
                    animator.SetBool("idle", false);
                    break;

                case (-1, 0):
                    animator.Play("Player_Walk_Left");
                    animator.SetBool("idle", false);
                    break;

                case (-1, 1):
                    animator.Play("Player_Walk_Up_Left");
                    animator.SetBool("idle", false);
                    break;

                case (-1, -1):
                    animator.Play("Player_Walk_Left");
                    animator.SetBool("idle", false);
                    break;
                default:
                    break;
            }
        }
    }

    public void PlayAnimatorWhileMovingPuzzle(Vector2 inputMovementPuzzle)
    {
        // Debug.Log(inputMovementPuzzle);
        if(inputMovementPuzzle != Vector2.zero)
        {
            // Debug.Log("mainkan");
            if(!soundManager.isPlayedSFX_PlayerWalk()){
                soundManager.PlaySFX_PlayerWalk();
            }
            if(inputMovementPuzzle.y == 1)
            {
                animator.Play("Player_Walk_Up");
                animator.SetBool("idle", false);
            }
            else if(inputMovementPuzzle.y == -1)
            {
                // Debug.Log("Halo1" + animator.GetBool("idle"));
                animator.Play("Player_Walk_Down");
                animator.SetBool("idle", false);
                // Debug.Log("Halo2" + animator.GetBool("idle"));
            }
            else if(inputMovementPuzzle.x == 1)
            {
                animator.Play("Player_Walk_Right");
                animator.SetBool("idle", false);
            }
            else if(inputMovementPuzzle.x == -1)
            {
                animator.Play("Player_Walk_Left");
                animator.SetBool("idle", false);
            }
        }
        else
        {
            soundManager.StopSFX_PlayerWalk();
            animator.SetBool("idle", true);
        }
        
        
    }
    public void PlayAnimatorCinematic(Vector2 inputMovementPuzzle)
    {
        // Debug.Log(inputMovementPuzzle);
        if(inputMovementPuzzle != Vector2.zero)
        {
            // Debug.Log("mainkan");
            if(!soundManager.isPlayedSFX_PlayerWalk()){
                soundManager.PlaySFX_PlayerWalk();
            }
            switch (inputMovementPuzzle.x, inputMovementPuzzle.y)
            {   
                case (0, 1):
                    animator.Play("Player_Walk_Up");
                    animator.SetBool("idle", false);
                    break;

                case (0, -1):
                    animator.Play("Player_Walk_Down");
                    animator.SetBool("idle", false);
                    break;

                case (1, 1):
                    animator.Play("Player_Walk_Up_Right");
                    animator.SetBool("idle", false);
                    break;

                case (1, 0):
                    animator.Play("Player_Walk_Right");
                    animator.SetBool("idle", false);
                    break;

                case (1, -1):
                    animator.Play("Player_Walk_Right");
                    animator.SetBool("idle", false);
                    break;

                case (-1, 0):
                    animator.Play("Player_Walk_Left");
                    animator.SetBool("idle", false);
                    break;

                case (-1, 1):
                    animator.Play("Player_Walk_Up_Left");
                    animator.SetBool("idle", false);
                    break;

                case (-1, -1):
                    animator.Play("Player_Walk_Left");
                    animator.SetBool("idle", false);
                    break;
                default:
                    break;
            }
        }
        else
        {
            soundManager.StopSFX_PlayerWalk();
            animator.SetBool("idle", true);
        }
        
        
    }
    public void changeLastInGameType(WitchGameManager.InGameType lastInGameType)
    {
        LastInGameType = lastInGameType;
    }
}
