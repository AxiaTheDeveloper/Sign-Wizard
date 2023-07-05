using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSaveManager : MonoBehaviour
{
    public static PlayerSaveManager Instance{get; private set;}
    [SerializeField]private PlayerSave playerSaveSO;
    private Transform playerPosition;
    [SerializeField]private Transform bedPlayerPosition;

    private bool hasReset = false, hasDone_Go_Out_Dialogue = false;
    private void Awake() {
        Instance = this;
        // hasReset = false;
        if(playerSaveSO.isResetDay || playerSaveSO.isSubmitPotion){
            playerPosition = GetComponent<Transform>();
            playerPosition.position = bedPlayerPosition.position;
        }
    }
    private void Update() {
        if(hasReset){
            if(playerSaveSO.isResetDay){
                playerSaveSO.isResetDay = false;
            }
            else if(playerSaveSO.isFromOutside){
                playerSaveSO.isFromOutside = false;
            }
            
        }
        if(hasDone_Go_Out_Dialogue && playerSaveSO.isSubmitPotion){
            playerSaveSO.isSubmitPotion = false;
        }
    }
    public void HasReset(){
        hasReset = true;
        Debug.Log(hasReset);
    }
    public void HasDone_GoOutDialogue(){
        hasDone_Go_Out_Dialogue = true;
        Debug.Log(hasReset);
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
        SceneManager.LoadScene("InDoor");
    }
    public void ResetDay_SubmitPotion(){
        playerSaveSO.isSubmitPotion = true;
        SceneManager.LoadScene("InDoor"); // ntr kuganti jd nama scene saja
    }
    public void Go_OutsideNow(){
        playerSaveSO.isFromOutside = true;
        SceneManager.LoadScene("OutDoor");
    }
    public bool GetIsReset(){
        return playerSaveSO.isResetDay;
    }
    public bool GetIsPlayerFromOutside(){
        return playerSaveSO.isFromOutside;
    }
    public bool GetIsSubmitPotion(){
        return playerSaveSO.isSubmitPotion;
    }

}
