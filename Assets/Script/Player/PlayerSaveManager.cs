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

    public void ChangePlayerLevel(int level){
        playerSaveSO.level = level;
    }
    public void ChangePlayerMode(levelMode mode){
        playerSaveSO.modeLevel = mode;
    }
}
