using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private GameInput gameInput;
    [SerializeField]private Rigidbody2D rb;
    private Vector2 keyInput;

    [Header("This is for Player Movement")]
    [SerializeField]private float speedMovement;

    private bool wasFromOtherInterface = false;

    

    private void Update() {
        if(WitchGameManager.Instance.IsInGame()){
            if(wasFromOtherInterface){
                wasFromOtherInterface = false;
            }
            else{
                keyInput = gameInput.GetInputMovement();
            }
            
        }
        else{
            if(keyInput != Vector2.zero){
                keyInput = Vector2.zero;
                PlayerWalk();
                wasFromOtherInterface = true;
            }
            
        }
        
        
        
    }
    private void FixedUpdate() {
        PlayerWalk();
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


}
