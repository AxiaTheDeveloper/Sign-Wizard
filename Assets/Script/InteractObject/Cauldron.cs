using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cauldron : MonoBehaviour
{
    [SerializeField]private PotionRecipeScriptableObject[] recipeList;
    [SerializeField]private InventoryUI cauldronUI_Inventory;
    [SerializeField]private CauldronUI cauldronUI_Cook;
    private InventoryCauldron inventCauldron;
    [SerializeField]private WitchGameManager gameManager;
    [SerializeField]private PlayerInventory playerInventory;
    
    private List<CauldronItem> cauldronItems;
    [SerializeField]private int CauldronSize;


    private void Start() {
        playerInventory.OnQuitCauldron += playerInventory_OnQuitCauldron;
        inventCauldron = cauldronUI_Inventory.GetInventoryCauldron();
        inventCauldron.OnItemCauldron += inventCauldron_OnItemCauldron;
        cauldronItems = new List<CauldronItem>();
        for(int i=0;i<CauldronSize;i++){
            CauldronItem newCauldron = new CauldronItem();
            cauldronItems.Add(newCauldron.EmptyItem());
        }
    }
    private void AddItemCauldron(int selectItem){
        InventorySlot item;
        CauldronItem cauldronItem;
        for(int i=0;i<CauldronSize;i++){
            if(cauldronItems[i].isEmpty){
                cauldronItem = new CauldronItem();
                item = playerInventory.GetPlayerInventory().TakeDataFromSlot(selectItem);
                cauldronItems[i] = cauldronItem.AddItem(item.itemSO, item.quantity, selectItem);
                cauldronUI_Cook.UpdateVisualInventorySlot(i, cauldronItems[i]);
                break;
                //updet visual
            }
        }
        // for(int i=0;i<CauldronSize;i++){
        //     Debug.Log(i + " " + cauldronItems[i].itemSO);
        // }
    }
    private void RemoveItemCauldron(int selectItem){
        CauldronItem cauldronItem;
        for(int i=0;i<CauldronSize;i++){
            if(!cauldronItems[i].isEmpty && cauldronItems[i].position_InInventory == selectItem){
                cauldronItem = new CauldronItem();
                // Debug.Log("di posisi "+ selectItem + " di urutan ke " + i);
                // Debug.Log("sebelum " + selectItem + " " + cauldronItems[i].itemSO);
                cauldronItems[i] = cauldronItem.EmptyItem();
                // Debug.Log("sesudah" + selectItem + " " + cauldronItems[i].itemSO);
                cauldronUI_Cook.UpdateVisualInventorySlot(i, cauldronItems[i]);
                break;
                //updet visual
            }
        }

    }

    private void CheckRecipe_ItemStage(){
        for(int i=0;i<recipeList.Length;i++){
            PotionRecipeScriptableObject recipe = recipeList[i];
            bool isContainMatch = true;
            if(cauldronItems.Count == recipe.Ingredients.Length){
                foreach(ItemScriptableObject itemRecipe in recipe.Ingredients){
                    bool isItemHere = false;
                    foreach(CauldronItem itemCauldron in cauldronItems){
                        if(itemRecipe == itemCauldron.itemSO){
                            isItemHere = true;
                            break;
                        }
                    }
                    if(!isItemHere){
                        isContainMatch = false;
                        break;
                    }
                }
                
            }
            if(isContainMatch){
                //ambil 
                break;
            }
        }
        
    }

    private void CheckRecipe_FireStage(){

    }

    private void inventCauldron_OnItemCauldron(object sender, InventoryCauldron.OnItemCauldronEventArgs e)
    {
        if(e.isAdd){
            AddItemCauldron(e.Position);
        }
        else{
            RemoveItemCauldron(e.Position);
        }
    }

    private void playerInventory_OnQuitCauldron(object sender, EventArgs e)
    {
        CloseWholeUI();
    }
    public void ShowWholeUI(){
        // wordInput.GetWordManager(wordManager);
        cauldronUI_Inventory.ShowInventoryUI();
        cauldronUI_Cook.ShowCookUI();
        gameManager.ChangeInterfaceType(3);
    }
    public void CloseWholeUI(){
        gameManager.ChangeToInGame();
        cauldronUI_Cook.HideCookUI();
        cauldronUI_Inventory.HideInventoryUI();
        //dan ui cauldronnya
    }
}
