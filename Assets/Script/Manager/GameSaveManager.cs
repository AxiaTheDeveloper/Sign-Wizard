using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
class PlayerData
{
    public int level;
    public levelMode modeLevel;
    public bool isResetDay;
    public bool isFromOutside;
    public bool isSubmitPotion;
    public bool isResetSave;

    public bool isFirstTime_Tutorial;
}

[System.Serializable]
class Inventory
{
    public ItemScriptableObject[] itemSO;
    public int[] quantity;
    public bool isFull;
    public int isFullyEmpty;

    
}



public class GameSaveManager : MonoBehaviour
{
    public void SaveData(PlayerSave playerSaveSO, InventoryScriptableObject playerInventSO, InventoryScriptableObject chestSO){
        // if(PlayerPrefs.GetInt("PlayerHasSaveFile", 0) == 0){
        //     PlayerPrefs.SetInt("PlayerHasSaveFile", 1);
        // }
        
        string basePath = Application.persistentDataPath;
        
        string playerSavePath = Path.Combine(basePath, "ps.dat");
        string playerInventPath = Path.Combine(basePath, "pi.dat");
        string chestPath = Path.Combine(basePath, "c.dat");

        PlayerData psData = new PlayerData();
        psData.level = playerSaveSO.level;
        psData.modeLevel = playerSaveSO.modeLevel;
        psData.isResetDay = playerSaveSO.isResetDay;
        psData.isFromOutside = playerSaveSO.isFromOutside;
        psData.isSubmitPotion = playerSaveSO.isSubmitPotion;
        psData.isResetSave = playerSaveSO.isResetSave;
        psData.isFirstTime_Tutorial = playerSaveSO.isFirstTime_Tutorial;

        Inventory piData = new Inventory();
        piData.isFull = playerInventSO.isFull;
        piData.isFullyEmpty = playerInventSO.isFullyEmpty;
        piData.itemSO = new ItemScriptableObject[20];
        piData.quantity = new int[20];
        
        
        for(int i = 0; i < 20; i++){
            // Debug.Log(i);
            // Debug.Log("integer");
            piData.quantity[i] = playerInventSO.inventSlot[i].quantity;
            // Debug.Log("item");
            piData.itemSO[i] = playerInventSO.inventSlot[i].itemSO;
            // Debug.Log("done");
            
        }

        // Debug.Log("chest");
        Inventory cData = new Inventory();
        cData.isFull = chestSO.isFull;
        cData.isFullyEmpty = chestSO.isFullyEmpty;
        cData.itemSO = new ItemScriptableObject[27];
        cData.quantity = new int[27];
        // Debug.Log("salah?");

        for(int i = 0; i < 27; i++){
            cData.itemSO[i] = chestSO.inventSlot[i].itemSO;
            
            cData.quantity[i] = chestSO.inventSlot[i].quantity;
            Debug.Log(cData.itemSO[i] +" "+ cData.quantity[i]);
            // Debug.Log("done" + i);
        }

        File.WriteAllText(playerSavePath, JsonUtility.ToJson(psData));
        File.WriteAllText(playerInventPath, JsonUtility.ToJson(piData));
        File.WriteAllText(chestPath, JsonUtility.ToJson(cData));

    }
    public void LoadData(PlayerSave playerSaveSO, InventoryScriptableObject playerInventSO, InventoryScriptableObject chestSO){
        string basePath = Application.persistentDataPath;
        // Debug.Log(basePath + "Hi");
        string playerSavePath = Path.Combine(basePath, "ps.dat");
        string playerInventPath = Path.Combine(basePath, "pi.dat");
        string chestPath = Path.Combine(basePath, "c.dat");

        if(!File.Exists(playerSavePath)){
            return;
        }
        PlayerData psData = JsonUtility.FromJson<PlayerData>(File.ReadAllText(playerSavePath));
        Inventory piData = JsonUtility.FromJson<Inventory>(File.ReadAllText(playerInventPath));
        Inventory cData = JsonUtility.FromJson<Inventory>(File.ReadAllText(chestPath));

        playerSaveSO.level = psData.level;
        playerSaveSO.modeLevel = psData.modeLevel;
        playerSaveSO.isResetDay = psData.isResetDay;
        playerSaveSO.isFromOutside = psData.isFromOutside;
        playerSaveSO.isSubmitPotion = psData.isSubmitPotion;
        playerSaveSO.isResetSave = psData.isResetSave;
        playerSaveSO.isFirstTime_Tutorial = psData.isFirstTime_Tutorial;

        playerInventSO.isFull = piData.isFull;
        playerInventSO.isFullyEmpty = piData.isFullyEmpty;

        for(int i = 0; i < 20; i++){
            
            playerInventSO.inventSlot[i].itemSO = piData.itemSO[i]; 
            playerInventSO.inventSlot[i].quantity = piData.quantity[i];
        }

        chestSO.isFull = cData.isFull;
        chestSO.isFullyEmpty = cData.isFullyEmpty;

        for(int i = 0; i < 27; i++){
            Debug.Log(cData.itemSO[i] +" "+ cData.quantity[i]);
            chestSO.inventSlot[i].itemSO = cData.itemSO[i]; 
            chestSO.inventSlot[i].quantity = cData.quantity[i];
        }
    }
}
