using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//game manageerrr, level, game state, interfacetype lalalalalala
public class WitchGameManager : MonoBehaviour
{
    public static WitchGameManager Instance{get;private set;}
    private enum gameState{
        InGame, InterfaceTime, Cinematic, Pause
    }
    private gameState state;

    private bool pauseState;

    private enum gameLevelQuest{
        lvl1, lvl2 // ini trgantung ntr mo brp lvl - ntr di save di scriptable object aja
    }
    private gameLevelQuest level;

    public enum InterfaceType{
        InventoryTime, InventoryAndCauldron, CauldronFire, InventoryAndChest, QuantityTime, none, InventoryAndPenumbuk, TumbukTime, InventoryAndSubmit, SubmitPotion
    }
    private InterfaceType interfaceType, saveInterfaceType_forPause;
    private void Awake() {
        Instance = this;
        state = gameState.Cinematic;
        // Debug.Log(state);
    }
    private void Start() {
        // state = gameState.InGame; // ini tergantung sih mo drmn dl
        //baca save 

        //inicontoh doang...
        // Debug.Log(state);
        

    }
    private void Update() {
        // Debug.Log(state);
    }

    public void ChangeToInGame(){
        interfaceType = InterfaceType.none;
        state = gameState.InGame;
    }

    //bagian urusan interface

    // public void ChangeToInterface(){
    //     state = gameState.InterfaceTime;
    // }

    //list of code need / change interface type : Chest(InteractObject), Cauldron(InteractObject), PlayerInventory, InventoryUI(UI), WordInput(Word)



    public void ChangeInterfaceType(InterfaceType type){
        state = gameState.InterfaceTime;
        interfaceType = type;
    }
    public InterfaceType IsInterfaceType(){
        return interfaceType;
    }



    public void PauseGame(){
        pauseState = !pauseState;
        //ga mungkin bs pause kalo lg d state interface
        if(pauseState){
            state = gameState.Pause;
        }
        else{
            state = gameState.InGame;
        }
    }

    //ambil lg state apa
    public bool IsInGame(){
        return state == gameState.InGame;
    }
    public bool isCinematic(){
        return state == gameState.Cinematic;
    }
    public void ChangeToCinematic(){
        interfaceType = InterfaceType.none;
        state = gameState.Cinematic;
    }
    // public bool IsInterfaceTime(){
    //     return state == gameState.InterfaceTime;
    // }
    
}
