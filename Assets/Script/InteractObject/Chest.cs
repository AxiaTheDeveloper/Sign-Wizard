using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Contoh salah satu anakkan interact object~~~
public class Chest : MonoBehaviour
{
    [SerializeField]private WordInput wordInput;
    [SerializeField]private PlayerInventory playerInventory;
    [SerializeField]private InventoryScriptableObject chestInventory;
    private int chestInventorySize;
    [SerializeField]private InventoryUI ChestUI;
    // private List<GameObject>
    private int chosenWord;
    
    [SerializeField]private WordManager[] wordManager;
    [SerializeField]private FinishWordDoFunction finishFunction;
    private WitchGameManager gameManager;

    private void Awake(){
        gameManager = WitchGameManager.Instance;
        chestInventorySize = chestInventory.size;
    }

    private void Start(){
        playerInventory.OnQuitChest += playerInventory_OnQuitChest;
        playerInventory.OnClearPlayerInventory += playerInventory_OnClearPlayerInventory;
        finishFunction.OnFinishChestWord += finishWord_OnFinishChestWord;
    }
    private void finishWord_OnFinishChestWord(object sender, EventArgs e)
    {
        
        if(playerInventory.GetPlayerInventory().isFull){
            Debug.Log("Keluarkan UI playerInventoryFull");
        }
        else{
            int selectItem = ChestUI.GetSelectedItem();
            int quantityWant = ChestUI.GetQuantityWant();
            if(quantityWant == 1 && chestInventory.inventSlot[selectItem].quantity == 0){
                    Debug.Log("Keluarkan UI barang habis, besok ambil kembali");
                
            }
            else{
                chestInventory.TakeItemFromSlot(selectItem, quantityWant);
                playerInventory.GetPlayerInventory().AddItemToSlot(chestInventory.inventSlot[selectItem].itemSO, quantityWant);
            }
        }
        ChestUI.ResetQuantityWant();
    }

    private void playerInventory_OnQuitChest(object sender, EventArgs e)
    {
        CloseWholeUI();
    }

    private void playerInventory_OnClearPlayerInventory(object sender, EventArgs e)
    {
        ClearInventoryPlayer();
        // Debug.Log("hi");
    }

    


    public void ShowWholeUI(){
        //nyalain UI yg isinya kek gambar doang blm tulisan
        wordInput.GetWordManager(wordManager);
        // wordInput.ChangeisOnlyOneWord(false);
        ChestUI.ShowInventoryUI();
        // wordUI.SetActive(true);
        gameManager.ChangeInterfaceType(4);
        // change game state
    }
    public void CloseWholeUI(){
        gameManager.ChangeToInGame();
        ChestUI.HideInventoryUI();
        // wordUI.SetActive(true);
        //change game state
        //di sini delete semua gameobject ???
    }

    public InventoryScriptableObject GetChestInventory(){
        return chestInventory;
    }
    public int GetChestSize(){
        return chestInventorySize;
    }
    private void ClearInventoryPlayer(){
        if(playerInventory.GetPlayerInventory().isFullyEmpty > 0){
            for(int i=0;i<playerInventory.GetPlayerInventory().size;i++){
                // Debug.Log(i);
                int quantity = playerInventory.GetPlayerInventory().inventSlot[i].quantity;
                // Debug.Log(quantity);
                ItemScriptableObject item = playerInventory.GetPlayerInventory().inventSlot[i].itemSO;
                if(quantity == 0){
                    // Debug.Log("kosong");
                    continue;
                }
                else{
                    // Debug.Log("berhasil");
                    playerInventory.GetPlayerInventory().TakeItemFromSlot(i, quantity);
                    playerInventory.GetPlayerInventory().TakeDataFromSlot(i);
                    chestInventory.AddItemToSlot(item, quantity);
                }
                
            }
        }
        
    }
}
