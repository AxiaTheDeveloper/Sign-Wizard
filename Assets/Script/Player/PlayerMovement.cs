using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private GameInput gameInput;
    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private Animator animator;
    private Vector2 keyInput;

    [Header("This is for Player Movement")]
    [SerializeField]private float speedMovement;

    private bool wasFromOtherInterface = false;

    private void Start()
    {
        animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
    }

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
                PlayerWalk();
                
            }
            if(!wasFromOtherInterface){
                wasFromOtherInterface = true;
            }
            
        }
        // Debug.Log(keyInput + "dan" + wasFromOtherInterface);
        
        
        
    }
    private void FixedUpdate() {
        PlayerWalk();
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
    private void PlayerWalk(){

        rb.MovePosition(rb.position + keyInput * speedMovement * Time.fixedDeltaTime);

        if (keyInput.x > 0) keyInput.x = 1;
        if (keyInput.y > 0) keyInput.y = 1;
        if (keyInput.x < 0) keyInput.x = -1;
        if (keyInput.y < 0) keyInput.y = -1;

        switch (keyInput.x, keyInput.y)
        {   
            case (0, 0):
                animator.SetBool("idle", true);
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

    public Vector2 GetKeyInput(){
        return keyInput;
    }
    public float GetKeyInputX(){
        return keyInput.x;
    }


}
