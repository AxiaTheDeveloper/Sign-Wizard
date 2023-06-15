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

    private enum InterfaceType{
        writingTime, selectTime, InventoryTime, InventoryAndCauldron, InventoryAndChest, QuantityTime
    }
    private InterfaceType interfaceType;
    private void Awake() {
        Instance = this;
    }
    private void Start() {
        state = gameState.InGame; // ini tergantung sih mo drmn dl
        //baca save 

    }

    public void ChangeToInGame(){
        state = gameState.InGame;
    }

    //bagian urusan interface

    // public void ChangeToInterface(){
    //     state = gameState.InterfaceTime;
    // }

    public void ChangeInterfaceType(int nomor){
        state = gameState.InterfaceTime;
        if(nomor == 0){
            interfaceType = InterfaceType.writingTime;
        }
        else if(nomor == 1){
            interfaceType = InterfaceType.selectTime;
        }
        else if(nomor == 2){
            interfaceType = InterfaceType.InventoryTime;
        }
        else if(nomor == 3){
            interfaceType = InterfaceType.InventoryAndCauldron;
        }
        else if(nomor == 4){
            interfaceType = InterfaceType.InventoryAndChest;
        }
        else if(nomor == 5){
            interfaceType = InterfaceType.QuantityTime;
        }
    }
    public int IsInterfaceType(){
        //1 - write, 2 - select, 3 - Inventory +1 dr atas
        if(state != gameState.InterfaceTime) return 0;
        else if(state == gameState.InterfaceTime){
            if(interfaceType == InterfaceType.writingTime){
                return 1;
            }
            else if(interfaceType == InterfaceType.selectTime){
                return 2;
            }
            else if(interfaceType == InterfaceType.InventoryTime){
                return 3;
            }
            else if(interfaceType == InterfaceType.InventoryAndCauldron){
                return 4;
            }
            else if(interfaceType == InterfaceType.InventoryAndChest){
                return 5;
            }
            else if(interfaceType == InterfaceType.QuantityTime){
                return 6;
            }
        }
        return 0;

    }

    

    public void PauseGame(){
        pauseState = !pauseState;
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
    // public bool IsInterfaceTime(){
    //     return state == gameState.InterfaceTime;
    // }
    
}
