using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Contoh salah satu anakkan interact object~~~
public class Chest : MonoBehaviour
{
    public enum ItemType{
        bahanPotion, bahanHarusDigerus, potion
    }
    [SerializeField]private WordInput wordInput;
    [SerializeField]private PlayerInventory playerInventory;
    [SerializeField]private InventoryScriptableObject chestInventory, chestMain;
    private int chestInventorySize;
    [SerializeField]private InventoryUI ChestUI;
    // private List<GameObject>
    
    [SerializeField]private WordManager[] wordManager;
    [SerializeField]private FinishWordDoFunction finishFunction;
    [SerializeField]private WitchGameManager gameManager;
    [SerializeField]private DialogueManager dialogueManager;

    private void Awake(){
        // gameManager = WitchGameManager.Instance;
        //ntr dikasih syarat kalo bangun/hari baru reset chestinventory jd chestmain, playerinventory juga direset;
        chestInventory.inventSlot = CopyInventorySlot(chestMain.inventSlot);
        chestInventorySize = chestInventory.size;
        EditorUtility.SetDirty(chestInventory);
    }

    private List<InventorySlot> CopyInventorySlot(List<InventorySlot> source){
        List<InventorySlot> newList = new List<InventorySlot>();
        foreach(InventorySlot inventSlot in source){
            InventorySlot newInvent = new InventorySlot();
            newList.Add(newInvent.ChangeQuantity(inventSlot.itemSO,inventSlot.quantity));
        }

        return newList;
    }
    private void Start(){
        playerInventory.OnQuitChest += playerInventory_OnQuitChest;
        playerInventory.OnClearPlayerInventory += playerInventory_OnClearPlayerInventory;
        finishFunction.OnFinishChestWord += finishWord_OnFinishChestWord;
    }
    private void finishWord_OnFinishChestWord(object sender, EventArgs e)
    {
        
        if(playerInventory.GetPlayerInventory().isFull){
            dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.playerInventoryFull_Chest);
            // Debug.Log("Keluarkan UI playerInventoryFull");
        }
        else{
            int selectItem = ChestUI.GetSelectedItem();
            int quantityWant = ChestUI.GetQuantityWant();
            if(quantityWant == 1 && chestInventory.inventSlot[selectItem].quantity == 0){
                dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.barangChestHabis_Chest);
                    // Debug.Log("Keluarkan UI barang habis, besok ambil kembali");
                
            }
            else{
                chestInventory.TakeItemFromSlot(selectItem, quantityWant);
                playerInventory.GetPlayerInventory().AddItemToSlot(chestInventory.inventSlot[selectItem].itemSO, quantityWant);
                EditorUtility.SetDirty(chestInventory);
                EditorUtility.SetDirty(playerInventory.GetPlayerInventory());
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
        gameManager.ChangeInterfaceType(WitchGameManager.InterfaceType.InventoryAndChest);
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
                InventorySlot inventSlot = playerInventory.GetPlayerInventory().inventSlot[i];
                if(inventSlot.isEmpty){
                    continue;
                }
                else if(!inventSlot.itemSO.isFromChest){
                    continue;
                }
                // Debug.Log(i);
                int quantity = inventSlot.quantity;
                // Debug.Log(quantity);
                ItemScriptableObject item = inventSlot.itemSO;
                if(quantity == 0){
                    // Debug.Log("kosong");
                    continue;
                }
                else{
                    // Debug.Log("berhasil");
                    playerInventory.GetPlayerInventory().TakeItemFromSlot(i, quantity);
                    playerInventory.GetPlayerInventory().TakeDataFromSlot(i);
                    chestInventory.AddItemToSlot(item, quantity);
                    EditorUtility.SetDirty(chestInventory);
                    EditorUtility.SetDirty(playerInventory.GetPlayerInventory());
                }
                
            }
        }
        
    }
}
