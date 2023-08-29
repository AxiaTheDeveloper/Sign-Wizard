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
    private InGameType saveInGameType_WhilePause;
    public event EventHandler OnChangeToCinematic, OnChangeToInGame, OnChangeToInterface, OnChangeToPause;
    public enum InGameType{
        normal, puzzle, none
    }
    private InGameType inGameType;


    public enum InterfaceType{
        InventoryTime, InventoryAndCauldron, CauldronFire, InventoryAndChest, QuantityTime, none, InventoryAndPenumbuk, TumbukTime, InventoryAndSubmit, SubmitPotion, DictionaryTime, InterfaceBed, InterfaceDoor, InterfaceQuestBox, InterfaceTutorial, InterfaceYesNoTutorial, InterfaceMap
    }
    private InterfaceType interfaceType, saveInterfaceType_forPause;

    public enum Place{
        indoor, outdoor, none
    }
    [SerializeField]private Place place;
    public enum OutDoorType{
        town, inFrontOfHouse, forest, graveyard, magicalBridge, brokenBridge_Graveyard, brokenBridge_Town ,none
    }
    [SerializeField]private OutDoorType outdoorType;
    private void Awake() {
        Instance = this;
        
        state = gameState.Cinematic;
        // Debug.Log(state);
    }
    private void Update() {
        if(inGameType == InGameType.normal)
        {
            if(Input.GetKeyDown(KeyCode.Alpha0))
            {
                Debug.Log("Don't forget to delete this thing!");
                inGameType = InGameType.puzzle;
                OnChangeToInGame?.Invoke(this,EventArgs.Empty);
            }
            
            
        }
        else if(inGameType == InGameType.puzzle)
        {
            if(Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                Debug.Log("Don't forget to delete this thing!");
                inGameType = InGameType.normal;
                OnChangeToInGame?.Invoke(this,EventArgs.Empty);
            }
        }
        
    }
    public Place GetPlace(){
        return place;
    }
    public OutDoorType GetOutDoorType()
    {
        return outdoorType;
    }
    public void ChangeToInGame(InGameType inGameTypeNow){
        interfaceType = InterfaceType.none;
        inGameType = inGameTypeNow;
        state = gameState.InGame;
        OnChangeToInGame?.Invoke(this, EventArgs.Empty);
    }
    public InGameType IsInGameType()
    {
        return inGameType;
    }

    //bagian urusan interface

    // public void ChangeToInterface(){
    //     state = gameState.InterfaceTime;
    // }

    //list of code need / change interface type : Chest(InteractObject), Cauldron(InteractObject), PlayerInventory, InventoryUI(UI), WordInput(Word)



    public void ChangeInterfaceType(InterfaceType type){
        state = gameState.InterfaceTime;
        inGameType = InGameType.none;
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
            saveInGameType_WhilePause = inGameType;
            inGameType = InGameType.none;
            state = gameState.Pause; 
            OnChangeToPause?.Invoke(this, EventArgs.Empty);       
        }
        else{
            state = gameState.InGame;
            inGameType = saveInGameType_WhilePause;
            OnChangeToInGame?.Invoke(this,EventArgs.Empty);
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
        inGameType = InGameType.none;
        interfaceType = InterfaceType.none;
        state = gameState.Cinematic;
        OnChangeToCinematic?.Invoke(this, EventArgs.Empty);
    }
    // public bool IsInterfaceTime(){
    //     return state == gameState.InterfaceTime;
    // }
    
}
