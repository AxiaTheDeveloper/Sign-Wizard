using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
class PlayerData
{
    public int level;
    public levelMode modeLevel;
    public WitchGameManager.Place placePlayerNow;
    public WitchGameManager.OutDoorType outDoorTypeNow;
    public bool isResetDay;
    public bool isFromOutside;
    public bool isSubmitPotion;
    public bool isResetSave;

    public bool isFirstTime_Tutorial;
    public bool isMagicalBridgeSolved;
}

[System.Serializable]
class Inventory
{
    // public string[] itemSOListinString;
    public ItemScriptableObject[] itemSO;
    public int[] quantity;
    public bool isFull;
    public int isFullyEmpty;

    
}



public class GameSaveManager : MonoBehaviour
{
    public List<ItemScriptableObject> listItem;
    // private void Start() {
    //     Debug.Log(listItem.Count);
    //     Debug.Log(this.gameObject);
    //     foreach(ItemScriptableObject item in listItem)
    //     {
    //         Debug.Log(item.name);
    //     }
    // }
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
        psData.placePlayerNow = playerSaveSO.placePlayerNow;
        psData.outDoorTypeNow = playerSaveSO.outDoorTypeNow;
        psData.isResetDay = playerSaveSO.isResetDay;
        psData.isFromOutside = playerSaveSO.isFromOutside;
        psData.isSubmitPotion = playerSaveSO.isSubmitPotion;
        psData.isResetSave = playerSaveSO.isResetSave;
        psData.isFirstTime_Tutorial = playerSaveSO.isFirstTime_Tutorial;
        psData.isMagicalBridgeSolved = playerSaveSO.isMagicalBridgeSolved;

        Inventory piData = new Inventory();
        piData.isFull = playerInventSO.isFull;
        piData.isFullyEmpty = playerInventSO.isFullyEmpty;
        piData.itemSO = new ItemScriptableObject[20];
        piData.quantity = new int[20];
        
        
        for(int i = 0; i < 20; i++){
            piData.quantity[i] = playerInventSO.inventSlot[i].quantity;
            piData.itemSO[i] = playerInventSO.inventSlot[i].itemSO;
            // piData.itemSOListinString[i] = JsonConvert.SerializeObject(playerInventSO.inventSlot[i].itemSO);
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
            // cData.itemSOListinString[i] = JsonConvert.SerializeObject(chestSO.inventSlot[i].itemSO);
        }

        File.WriteAllText(playerSavePath, JsonUtility.ToJson(psData));
        File.WriteAllText(playerInventPath, JsonUtility.ToJson(piData));
        File.WriteAllText(chestPath, JsonUtility.ToJson(cData));

    }
    public void LoadData(PlayerSave playerSaveSO, InventoryScriptableObject playerInventSO, InventoryScriptableObject chestSO){
        string basePath = Application.persistentDataPath;
        Debug.Log(basePath);
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
        playerSaveSO.isMagicalBridgeSolved = psData.isMagicalBridgeSolved;

        playerInventSO.isFull = piData.isFull;
        playerInventSO.isFullyEmpty = piData.isFullyEmpty;

        for(int i = 0; i < 20; i++){
            // Debug.Log("save " +piData.itemSO[i] +" "+ piData.quantity[i]);

            // playerInventSO.inventSlot[i].itemSO = piData.itemSO[i]; 
            // Debug.Log(listItem[1]);
            foreach(ItemScriptableObject item in listItem)
            {
                // Debug.Log(piData.itemSO[i] + " " + item + " hmm");
                if(piData.itemSO[i] == null)
                {
                    playerInventSO.inventSlot[i].itemSO = null;
                    break;
                }
                if(item.name == piData.itemSO[i].name)
                {
                    // Debug.Log(item);
                    playerInventSO.inventSlot[i].itemSO = item;
                    break;
                }
            }
            playerInventSO.inventSlot[i].quantity = piData.quantity[i];
            // Debug.Log("isi " +playerInventSO.inventSlot[i].itemSO +" "+ playerInventSO.inventSlot[i].quantity);
        }
        chestSO.isFull = cData.isFull;
        chestSO.isFullyEmpty = cData.isFullyEmpty;

        for(int i = 0; i < 27; i++){
            // Debug.Log("save " + cData.itemSO[i] +" "+ cData.quantity[i]);
            
            // chestSO.inventSlot[i].itemSO = cData.itemSO[i]; 
            foreach(ItemScriptableObject item in listItem)
            {
                if(cData.itemSO[i] == null)
                {
                    chestSO.inventSlot[i].itemSO = null;
                    break;
                }
                if(item.name == cData.itemSO[i].name)
                {
                    chestSO.inventSlot[i].itemSO = item;
                    break;
                }
            }
            chestSO.inventSlot[i].quantity = cData.quantity[i];
            // Debug.Log("isi " + chestSO.inventSlot[i].itemSO +" "+ chestSO.inventSlot[i].quantity);
        }
    }
}
