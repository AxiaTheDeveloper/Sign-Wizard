using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSaveManager : MonoBehaviour
{
    public static PlayerSaveManager Instance{get; private set;}
    [SerializeField]private PlayerSave playerSaveSO;

    private bool hasReset;
    private void Awake() {
        Instance = this;
        hasReset = false;
        if(playerSaveSO.isResetDay){
            //change position player jgn lupa
        }
    }
    private void Update() {
        if(hasReset && playerSaveSO.isResetDay){
            playerSaveSO.isResetDay = false;
        }
    }
    public void HasReset(){
        hasReset = true;
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

    public void ResetDay_Sleep(){
        playerSaveSO.isResetDay = true;
        //restart scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public bool GetIsReset(){
        return playerSaveSO.isResetDay;
    }
}
