using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PlayerSaveManager : MonoBehaviour
{
    public static PlayerSaveManager Instance{get; private set;}
    [SerializeField]private PlayerSave playerSaveSO;
    private Transform playerPosition;
    [SerializeField]private Transform bedPlayerPosition;

    private void Awake() {
        Instance = this;
        if(playerSaveSO.modeLevel == levelMode.outside && playerSaveSO.level == 1){
            Transform visualPos = transform.GetChild(0);
            // Debug.Log(visualPos.gameObject);
            visualPos.localPosition = new Vector3(0,-2.65f,0);
        }
        
    }


    public void HasReset(){
        if(playerSaveSO.isResetDay){
            playerSaveSO.isResetDay = false;
        }
        else if(playerSaveSO.isFromOutside){
            playerSaveSO.isFromOutside = false;
        }
        #if UNITY_EDITOR
        EditorUtility.SetDirty(playerSaveSO);
        #endif
        
        // Debug.Log(hasReset);
    }
    public void player_HasReset_Place(){
        if(playerSaveSO.isResetDay){
            playerPosition = GetComponent<Transform>();
            playerPosition.position = bedPlayerPosition.position;
        }
        else if(playerSaveSO.isFromOutside){
            playerPosition = GetComponent<Transform>();
            playerPosition.position = new Vector3(-1f,-7.75f,0f);
        }
    }
    public void HasDone_GoOutDialogue(){
        
        playerSaveSO.isSubmitPotion = false;
        // Debug.Log(hasReset);
        #if UNITY_EDITOR
        EditorUtility.SetDirty(playerSaveSO);
        #endif
    }
    public void player_GoOutDialogue_Place(){
        playerPosition = GetComponent<Transform>();
        playerPosition.position = bedPlayerPosition.position;
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
            playerSaveSO.level = 6;
        }
        
        #if UNITY_EDITOR
        EditorUtility.SetDirty(playerSaveSO);
        #endif
        
    }
    public void ChangePlayerMode(levelMode mode){
        playerSaveSO.modeLevel = mode;
        #if UNITY_EDITOR
        EditorUtility.SetDirty(playerSaveSO);
        #endif
    }
    public void resetDay(){
        playerSaveSO.isResetDay = true;
        #if UNITY_EDITOR
        EditorUtility.SetDirty(playerSaveSO);
        #endif
    }

    public void ResetDay_Sleep(){
       
        playerSaveSO.isResetDay = true;
        if(playerSaveSO.isIndonesia){
            SceneManager.LoadScene("InDoor_ID");
        }
        else{
            SceneManager.LoadScene("InDoor_EN");
        }
        #if UNITY_EDITOR
        EditorUtility.SetDirty(playerSaveSO);
        #endif
    }
    public void ResetDay_SubmitPotion(){
        playerSaveSO.isSubmitPotion = true;
        if(playerSaveSO.isIndonesia){
            SceneManager.LoadScene("InDoor_ID");
        }
        else{
            SceneManager.LoadScene("InDoor_EN");
        }
        
        #if UNITY_EDITOR
        EditorUtility.SetDirty(playerSaveSO);
        #endif
    }
    public void Go_OutsideNow(){
        if(playerSaveSO.modeLevel == levelMode.outside){
            playerSaveSO.isFromOutside = true;
            #if UNITY_EDITOR
            EditorUtility.SetDirty(playerSaveSO);
            #endif
        }
        if(playerSaveSO.isIndonesia){
            SceneManager.LoadScene("OutDoor_ID");
        }
        else{
            SceneManager.LoadScene("OutDoor_EN");
        }
    }
    public void Go_InsideNow(){
        if(playerSaveSO.isIndonesia){
            SceneManager.LoadScene("InDoor_ID");
        }
        else{
            SceneManager.LoadScene("InDoor_EN");
        }
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
    public bool GetIsResetSave(){
        return playerSaveSO.isResetSave;
    }
    public void HasResetSave(){
        playerSaveSO.isResetSave = false;
        #if UNITY_EDITOR
            EditorUtility.SetDirty(playerSaveSO);
            #endif
    }

}
