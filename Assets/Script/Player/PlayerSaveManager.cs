using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveManager : MonoBehaviour
{
    public static PlayerSaveManager Instance{get; private set;}
    [SerializeField]private PlayerSave playerSaveSO;
    private void Awake() {
        Instance = this;
    }

    public int GetPlayerLevel(){
        return playerSaveSO.level;
    }
    public levelMode GetPlayerLevelMode(){
        return playerSaveSO.modeLevel;
    }

    public void ChangePlayerLevel(){
        if(playerSaveSO.level == 0){
            playerSaveSO.level = 1;
        }
        else if(playerSaveSO.level == 1){
            playerSaveSO.level = 2;
        }
        else if(playerSaveSO.level == 2){
            playerSaveSO.level = 3;
        }
        else if(playerSaveSO.level == 3){
            playerSaveSO.level = 4;
        }
        else if(playerSaveSO.level == 4){
            playerSaveSO.level = 5;
        }
        else if(playerSaveSO.level == 5){
            playerSaveSO.level = 5;
        }
        
        
    }
    public void ChangePlayerMode(levelMode mode){
        playerSaveSO.modeLevel = mode;
    }
}
