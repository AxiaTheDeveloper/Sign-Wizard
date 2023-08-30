using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//semua gameinput maksudnya kek input.getkey lalalala
public class GameInput : MonoBehaviour
{
    public static GameInput Instance {get; private set;}

    private Vector2 keyInput, lastKeyInput, lastKeyInputAnimation, lastDiagonalInput, keyArrowInputUI, keyInputPuzzle;
    private bool wasUsingArrowKeys, wasUsingArrowKeysDictionary;
    [SerializeField]private float arrowKeyCooldownMaxTime, arrowKeyCooldownDictionaryMaxTime;
    private float arrowKeyCooldown, arrowKeyCooldownDictionary;




    private void Awake() {
        Instance = this;
    }
    private void Start() {

        wasUsingArrowKeys = false;
        arrowKeyCooldown = arrowKeyCooldownMaxTime;
    }

    //Ini Buat word
    public bool GetInputCancelInputLetter(){
        return Input.GetKey(KeyCode.Backspace); 
    }
    public bool GetInputEscape(){
        return Input.GetKeyDown(KeyCode.Escape); 
    }
    public bool GetInputEscapeMainMenu(){
        return Input.GetKeyDown(KeyCode.X);
    }


    //Ini Buat Player Semua

    //movement
    public Vector2 GetInputMovement(){
        
        keyInput.Set(0f,0f);

        if(WitchGameManager.Instance.IsInGame()){
            if(Input.GetKey(KeyCode.UpArrow)) keyInput.y = 1;
            if(Input.GetKey(KeyCode.DownArrow)) keyInput.y = -1;
            if(Input.GetKey(KeyCode.RightArrow)) keyInput.x = 1;
            if(Input.GetKey(KeyCode.LeftArrow)) keyInput.x = -1;
        }
        
        keyInput = keyInput.normalized;

        return keyInput;
    }
    
    public Vector2 GetLastInputMovement(){

        if(WitchGameManager.Instance.IsInGame()){
            if(keyInput.y != 0 || keyInput.x != 0){
                lastKeyInput = keyInput;
            }
        }
        

        // Debug.Log(lastKeyInput);
        return lastKeyInput;
    }
    public bool GetInputRun(){
        return Input.GetKey(KeyCode.LeftShift);
    }

    public Vector2 GetInputMovementPuzzle(){
        
        // keyInputPuzzle.Set(0f,0f);

        if(WitchGameManager.Instance.IsInGame()){
            if(Input.GetKeyDown(KeyCode.UpArrow)) return new Vector2(0,1);
            else if(Input.GetKeyDown(KeyCode.DownArrow)) return new Vector2(0,-1);
            else if(Input.GetKeyDown(KeyCode.RightArrow)) return new Vector2(1,0);
            else if(Input.GetKeyDown(KeyCode.LeftArrow)) return new Vector2(-1,0);
            // Debug.Log(keyInputPuzzle);
        }
        return new Vector2(0,0);
    }

    //yang lain
    public bool GetInputInteract(){
        return Input.GetKeyDown(KeyCode.Z);
    }
    public bool GetInputOpenInventory(){
        return Input.GetKeyDown(KeyCode.C);
    }
    public bool GetInputOpenInventory_ChestOpen(){
        return Input.GetKeyDown(KeyCode.LeftAlt);
    }

    public Vector2 GetInputArrow(){
        keyArrowInputUI.Set(0,0);
        if(wasUsingArrowKeys){
            arrowKeyCooldown -= Time.deltaTime;
            if(arrowKeyCooldown <= 0){
                arrowKeyCooldown = arrowKeyCooldownMaxTime;
                wasUsingArrowKeys = false;
            }
        }
        else{
            if(Input.GetKey(KeyCode.UpArrow)){
                keyArrowInputUI.y = 1;
                wasUsingArrowKeys = true;
            } 
            else if(Input.GetKey(KeyCode.DownArrow)){
                keyArrowInputUI.y = -1;
                wasUsingArrowKeys = true;
            } 
            else if(Input.GetKey(KeyCode.LeftArrow)){
                keyArrowInputUI.x = -1;
                wasUsingArrowKeys = true;
            }
            
            else if(Input.GetKey(KeyCode.RightArrow)){
                keyArrowInputUI.x = 1;
                wasUsingArrowKeys = true;
            }
        }
        
        return keyArrowInputUI;
    }
    public Vector2 GetInputArrow_Dictionary(){
        keyArrowInputUI.Set(0,0);
        if(wasUsingArrowKeysDictionary){
            arrowKeyCooldownDictionary -= Time.deltaTime;
            if(arrowKeyCooldownDictionary <= 0){
                arrowKeyCooldownDictionary = arrowKeyCooldownDictionaryMaxTime;
                wasUsingArrowKeysDictionary = false;
            }
        }
        else{
            if(Input.GetKeyDown(KeyCode.UpArrow)){
                keyArrowInputUI.y = 1;
                wasUsingArrowKeysDictionary = true;
            } 
            else if(Input.GetKeyDown(KeyCode.DownArrow)){
                keyArrowInputUI.y = -1;
                wasUsingArrowKeysDictionary = true;
            } 
            else if(Input.GetKeyDown(KeyCode.LeftArrow)){
                keyArrowInputUI.x = -1;
                wasUsingArrowKeysDictionary = true;
            }
            
            else if(Input.GetKeyDown(KeyCode.RightArrow)){
                keyArrowInputUI.x = 1;
                wasUsingArrowKeysDictionary = true;
            }
        }
        
        return keyArrowInputUI;
    }

    public bool GetInputGetKeyQuantity(){
        return Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift);
    }

    public bool GetInputClearInventoryPlayer(){
        return (Input.GetKeyDown(KeyCode.C));
    }
    
    public bool GetInputSelectItemForCauldron(){
        return Input.GetKeyDown(KeyCode.Z);
    }
    public bool GetInputStartCookingForCauldron(){
        return Input.GetKeyDown(KeyCode.Return);
    }

    public bool GetInputNextLine_Dialogue(){
        return Input.GetKeyDown(KeyCode.Space);
    }
    public bool GetInputQuit_Announcement(){
        return Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape);
    }

    public bool GetInputShowQuestLog(){
        return Input.GetKeyDown(KeyCode.Tab);
    }
    public bool GetInputHideQuestLog(){
        return Input.GetKeyUp(KeyCode.Tab);
    }
    public bool GetInputShowMap(){
        return Input.GetKeyDown(KeyCode.M);
    }
    public bool GetInputShowRuneinPuzzle(){
        return Input.GetKeyDown(KeyCode.LeftAlt);
    }
    public bool GetInputHideRuneinPuzzle(){
        return Input.GetKeyUp(KeyCode.LeftAlt);
    }

    


    


}
