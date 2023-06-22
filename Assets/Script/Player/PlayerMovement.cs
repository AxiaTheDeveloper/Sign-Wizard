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
    

    private void Update() {
        if(WitchGameManager.Instance.IsInGame()){
            keyInput = gameInput.GetInputMovement();
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
