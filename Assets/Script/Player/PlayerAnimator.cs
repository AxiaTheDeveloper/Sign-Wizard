using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    [SerializeField]public Animator animator;
    private Vector2 keyInput, lastKeyInput;


    private float lastSaveHorizontal, lastSaveVertical;
    private bool wasFromOtherInterface = false, wasDiagonal = false;
    [SerializeField]private GameInput gameInput;
    [SerializeField]private float maxDiagonalChecker;
    private float diagonalChecker;

    private void Update() {
    

        if(WitchGameManager.Instance.IsInGame()){
            if(!wasFromOtherInterface){
                
                keyInput = gameInput.GetInputMovement();
            }
            else if(wasFromOtherInterface && gameInput.GetInputMovement() != Vector2.zero){
                wasFromOtherInterface = false;
            }   
            
        }
        else{
            if(keyInput != Vector2.zero){
                keyInput = Vector2.zero;
                
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
        if (keyInput.x > 0) keyInput.x = 1;
        if (keyInput.y > 0) keyInput.y = 1;
        if (keyInput.x < 0) keyInput.x = -1;
        if (keyInput.y < 0) keyInput.y = -1;
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
                    animator.SetBool("idle", true);
                }
                else{
                    wasDiagonal = false;
                    if(lastKeyInput.x == 1 && lastKeyInput.y == 1){
                        animator.Play("Player_Idle_Up_Right");
                        animator.SetBool("idle", true);
                    }
                    else if(lastKeyInput.x == 1 && lastKeyInput.y == -1){
                        animator.Play("Player_Idle_Right");
                        animator.SetBool("idle", true);
                    }
                    else if(lastKeyInput.x == -1 && lastKeyInput.y == 1){
                        animator.Play("Player_Idle_Up_Left");
                        animator.SetBool("idle", true);
                    }
                    else if(lastKeyInput.x == -1 && lastKeyInput.y == -1){
                        animator.Play("Player_Idle_Left");
                        animator.SetBool("idle", true);
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
