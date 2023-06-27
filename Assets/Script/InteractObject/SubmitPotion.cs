using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitPotion : MonoBehaviour
{
    [SerializeField]private PlayerInventory playerInventory;
    [SerializeField]private WitchGameManager gameManager;
    [SerializeField]private InventoryUI submitPotionUI_Inventory;
    [SerializeField]private SubmitPotionUI submitPotionUI;
    private InventoryOnly inventOnly;

    private CauldronItem itemTerpilih;

    private void Start() {
        playerInventory.OnQuitSubmitPotion += playerInventory_OnQuitSubmitPotion;
        playerInventory.OnSubmitPotionChoice += playerInventory_OnSubmitPotionChoice;
        inventOnly = submitPotionUI_Inventory.GetInventoryOnly();
        inventOnly.OnItemSubmitPotion += inventOnly_OnItemSubmitPotion;
    }

    private void playerInventory_OnSubmitPotionChoice(object sender, EventArgs e)
    {
        if(submitPotionUI.GetIsChosePotion()){
            playerInventory.GetPlayerInventory().TakeItemFromSlot(itemTerpilih.position_InInventory, 1);
            //quest manager ngecheck apakah potionnya bener ato salah.
            HideWHoleUI();
            playerInventory.ClosePlayerInventory();
        }
        else{
            submitPotionUI_Inventory.DeselectItem_SubmitPotion();
            submitPotionUI.Show_AskingWhichPotion();
            gameManager.ChangeInterfaceType(WitchGameManager.InterfaceType.InventoryAndSubmit);
        }
    }

    private void inventOnly_OnItemSubmitPotion(object sender, InventoryOnly.OnItemSubmitPotionkEventArgs e)
    {
        
        
        if(e.isAdd){
            gameManager.ChangeInterfaceType(WitchGameManager.InterfaceType.SubmitPotion);
            InventorySlot item;
            item = playerInventory.GetPlayerInventory().TakeDataFromSlot(e.Position);
            itemTerpilih = new CauldronItem().AddItem(item.itemSO, item.quantity, e.Position);
            submitPotionUI.Show_AreYouSure(itemTerpilih.itemSO.itemName);
        }
        else{
            itemTerpilih = new CauldronItem().EmptyItem();
        }
        
    }



    private void playerInventory_OnQuitSubmitPotion(object sender, EventArgs e)
    {
        HideWHoleUI();
    }

    public void ShowWholeUI(){

        playerInventory.ShowPlayerInventory();
        submitPotionUI.ShowAllUI();
        gameManager.ChangeInterfaceType(WitchGameManager.InterfaceType.InventoryAndSubmit);
    }

    public void HideWHoleUI(){
        submitPotionUI.HideAllUI();
        submitPotionUI_Inventory.HideInventoryUI_SubmitPotion();
        
    }
}
