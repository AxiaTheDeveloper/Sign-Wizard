using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//game manageerrr, level, game state, interfacetype lalalalalala
public class WitchGameManager : MonoBehaviour
{
    public static WitchGameManager Instance{get;private set;}
    private enum gameState{
        InGame, InterfaceTime, Cinematic, Pause
    }
    private gameState state;

    private bool pauseState;
    private gameState saveState_WhilePause;
    public event EventHandler OnChangeToCinematic, OnChangeToInGame, OnChangeToInterface;


    public enum InterfaceType{
        InventoryTime, InventoryAndCauldron, CauldronFire, InventoryAndChest, QuantityTime, none, InventoryAndPenumbuk, TumbukTime, InventoryAndSubmit, SubmitPotion, DictionaryTime, InterfaceBed, InterfaceDoor, InterfaceQuestBox
    }
    private InterfaceType interfaceType, saveInterfaceType_forPause;

    public enum Place{
        indoor, outdoor, none
    }
    [SerializeField]private Place place;
    private void Awake() {
        Instance = this;
        
        state = gameState.Cinematic;
        // Debug.Log(state);
    }
    public Place GetPlace(){
        return place;
    }
    public void ChangeToInGame(){
        interfaceType = InterfaceType.none;
        state = gameState.InGame;
        OnChangeToInGame?.Invoke(this, EventArgs.Empty);
    }

    //bagian urusan interface

    // public void ChangeToInterface(){
    //     state = gameState.InterfaceTime;
    // }

    //list of code need / change interface type : Chest(InteractObject), Cauldron(InteractObject), PlayerInventory, InventoryUI(UI), WordInput(Word)



    public void ChangeInterfaceType(InterfaceType type){
        state = gameState.InterfaceTime;
        interfaceType = type;
        OnChangeToInterface?.Invoke(this, EventArgs.Empty);
    }
    public InterfaceType IsInterfaceType(){
        return interfaceType;
    }



    public void PauseGame(){
        pauseState = !pauseState;
        //ga mungkin bs pause kalo lg d state interface
        if(pauseState){
            saveState_WhilePause = state;
            state = gameState.Pause;        
        }
        else{
            
            state = saveState_WhilePause;
        }
    }

    //ambil lg state apa
    public bool IsInGame(){
        return state == gameState.InGame;
    }
    public bool isCinematic(){
        return state == gameState.Cinematic;
    }
    public bool isPause(){
        return state == gameState.Pause;
    }
    public void ChangeToCinematic(){
        interfaceType = InterfaceType.none;
        state = gameState.Cinematic;
        OnChangeToCinematic?.Invoke(this, EventArgs.Empty);
    }
    // public bool IsInterfaceTime(){
    //     return state == gameState.InterfaceTime;
    // }
    
}
