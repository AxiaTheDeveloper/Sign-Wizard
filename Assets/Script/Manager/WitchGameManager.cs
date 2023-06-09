using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private void Awake() {
        Instance = this;
    }
    private void Start() {
        state = gameState.InGame; // ini tergantung sih mo drmn dl

    }

    public void ChangeToInterface(){
        state = gameState.InterfaceTime;
    }

    public void ChangeToInGame(){
        state = gameState.InGame;
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
    public bool IsInterfaceTime(){
        return state == gameState.InterfaceTime;
    }
    
}
