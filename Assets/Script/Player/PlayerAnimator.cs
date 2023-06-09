using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    [SerializeField]private Animator animatorController;
    private const string ANIMATOR_HORIZONTAL = "Horizontal";
    private const string ANIMATOR_VERTICAL = "Vertical";
    private const string ANIMATOR_LAST_HORIZONTAL = "LastHorizontal";
    private const string ANIMATOR_LAST_VERTICAL = "LastVertical";
    private const string ANIMATOR_SPEED = "Speed";
    private Vector2 keyInput, lastKeyInput;
    // private bool isDiagonal;

    //waktu penghitung diagonal;

    private float lastSaveHorizontal, lastSaveVertical;

    private void Update() {
        keyInput = GameInput.Instance.GetInputMovement();
        lastKeyInput = GameInput.Instance.GetLastInputMovementAnimation();
        

        //ini timer utk animasi, biar
        // if(lastKeyInput.x != 0 && lastKeyInput.y != 0){
        //     //kalo misalnya sblmnya blm pernah dpt 1,-1 atau intinya bukan 1,0 0,1 gitu, maka masuk sini
        //     lastDiagonalInput = lastKeyInput;
        //     isDiagonal = true;
        // }
        // if(isDiagonal){
        //     //sblmnya diagonal dan skrg bukan diagonal, maka disuru masuk sini, trus dicek brp kali, selama berapa kali ini kita akan tetap dengan diagonal
        //     if(diagonalChecker <= 0){
        //             isDiagonal = false;
        //             diagonalChecker = diagonalCheckerMaxTime;
        //     }
        //     if(diagonalChecker > 0){
        //         diagonalChecker-=Time.deltaTime;
        //         lastKeyInput = lastDiagonalInput;
        //     }
        //     if((lastKeyInput.x != 0 && lastKeyInput.y == 0) || (lastKeyInput.x == 0 && lastKeyInput.y != 0)){
                
                
        //     }
            
        // }
        // Debug.Log("dig" +lastDiagonalInput);
        // Debug.Log(lastKeyInput);

        animatorController.SetFloat(ANIMATOR_HORIZONTAL, keyInput.x);
        animatorController.SetFloat(ANIMATOR_VERTICAL, keyInput.y);
        animatorController.SetFloat(ANIMATOR_LAST_HORIZONTAL, lastKeyInput.x);
        animatorController.SetFloat(ANIMATOR_LAST_VERTICAL, lastKeyInput.y);
        animatorController.SetFloat(ANIMATOR_SPEED, keyInput.sqrMagnitude);

    }
}
