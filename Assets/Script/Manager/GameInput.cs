using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance {get; private set;}

    private Vector2 keyInput, lastKeyInput, lastKeyInputAnimation, lastDiagonalInput;
    private bool wasDiagonal;
    [SerializeField]private float diagonalCheckerMaxTime;
    private float diagonalChecker;




    private void Awake() {
        Instance = this;
    }
    private void Start() {
        wasDiagonal = false;
        diagonalChecker = diagonalCheckerMaxTime;
    }

    //Ini Buat word
    public bool GetInputCancelInputLetter(){
        return Input.GetKey(KeyCode.Backspace); 
    }
    public bool GetInputEscape(){
        return Input.GetKey(KeyCode.Escape); 
    }


    //Ini Buat Player Semua

    //movement
    public Vector2 GetInputMovement(){
        
        keyInput.Set(0f,0f);

        if(WitchGameManager.Instance.IsInGame()){
            if(Input.GetKey(KeyCode.W)) keyInput.y = 1;
            if(Input.GetKey(KeyCode.S)) keyInput.y = -1;
            if(Input.GetKey(KeyCode.D)) keyInput.x = 1;
            if(Input.GetKey(KeyCode.A)) keyInput.x = -1;
        }
        

        
        keyInput = keyInput.normalized;

        return keyInput;
    }
    
    public Vector2 GetLastInputMovement(){
        
        // if(Input.GetKey(KeyCode.W)) {
        //     if(Input.GetKey(KeyCode.A)){
        //         lastKeyInput.Set(-1,1);
        //     }
        //     else if(Input.GetKey(KeyCode.D)){
        //         lastKeyInput.Set(1,1);
        //     }
        //     else{
        //         lastKeyInput.Set(0,1);
        //     }
        //     lastKeyInput = lastKeyInput.normalized;

        //     return lastKeyInput;
        // }
        // if(Input.GetKey(KeyCode.S)){
        //     if(Input.GetKey(KeyCode.A)){
        //         lastKeyInput.Set(-1,-1);
        //     }
        //     else if(Input.GetKey(KeyCode.D)){
        //         lastKeyInput.Set(1,-1);
        //     }
        //     else{
        //         lastKeyInput.Set(0,-1);
        //     }
        //     lastKeyInput = lastKeyInput.normalized;
        //     return lastKeyInput;
        // }
        // if(Input.GetKey(KeyCode.D)) lastKeyInput.Set(1,0);
        // if(Input.GetKey(KeyCode.A)) lastKeyInput.Set(-1,0);

        if(WitchGameManager.Instance.IsInGame()){
            if(keyInput.y != 0 || keyInput.x != 0){
                lastKeyInput = keyInput;
            }
        }
        

        // Debug.Log(lastKeyInput);
        return lastKeyInput;
    }

    public Vector2 GetLastInputMovementAnimation(){
        //the ultimate biar dia stuck di diagonal di animasinya
        if(WitchGameManager.Instance.IsInGame()){
            if((keyInput != Vector2.zero)){
            // Debug.Log("Keyinput : " + keyInput);
                if((keyInput.x != 0 && keyInput.y == 0) || (keyInput.x == 0 && keyInput.y != 0)){
                    if(!wasDiagonal){
                        // Debug.Log("Belum Diagonal");
                        lastKeyInputAnimation = keyInput;
                    }
                    else{
                        // Debug.Log("Pernah Diagonal");
                        if(diagonalChecker > 0){
                            // Debug.Log("DiagonalCountdown");
                            lastKeyInputAnimation = lastDiagonalInput;
                            diagonalChecker -= Time.deltaTime;
                        }
                        else if(diagonalChecker <= 0){
                            // Debug.Log("Ternyata sudah tidak diagonal");
                            lastKeyInputAnimation = keyInput;
                            diagonalChecker = diagonalCheckerMaxTime;
                            wasDiagonal = false;
                        }
                    }
                }
                else if(keyInput.x != 0 && keyInput.y != 0){
                    // Debug.Log("Diagonal");
                    lastKeyInputAnimation = keyInput;
                    lastDiagonalInput = keyInput;
                    wasDiagonal = true;
                }
            }
            else{
                diagonalChecker = diagonalCheckerMaxTime;
                wasDiagonal = false;
            }
        }
        

        // Debug.Log(lastKeyInputAnimation);
        return lastKeyInputAnimation;
    }

    //yang lain
    public bool GetInputInteract(){
        return Input.GetKeyDown(KeyCode.F);
    }
    public bool GetInputOpenInventory(){
        return Input.GetKeyDown(KeyCode.E);
    }

    


}
