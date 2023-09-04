using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitPotion : MonoBehaviour
{
    public enum CharacterHouseName{
        Chal, Vii, Eline, Cloter
    }
    [SerializeField]private CharacterHouseName charHouseName;
    [SerializeField]private bool isCharacterATravelingMerchant;
    private string charName
    {
        get
        {
            if(charHouseName == CharacterHouseName.Chal)
            {
                return "Chal";
            }
            else if(charHouseName == CharacterHouseName.Vii)
            {
                return "Vii";
            }
            else if(charHouseName == CharacterHouseName.Eline)
            {
                return "Eline";
            }
            else if(charHouseName == CharacterHouseName.Cloter)
            {
                return "Cloter";
            }
            return "";
        }
    }
    [SerializeField]private PlayerInventory playerInventory;
    [SerializeField]private WitchGameManager gameManager;
    [SerializeField]private InventoryUI submitPotionUI_Inventory;
    [SerializeField]private SubmitPotionUI submitPotionUI;
    private InventoryOnly inventOnly;
    [SerializeField]private DialogueManager dialogueManager;
    [SerializeField]private PlayerSaveManager playerSaveManager;

    [Header("Pengecekan potion")]

    private CauldronItem itemTerpilih;
    private List<CauldronItem> ListItemTerpilih;
    [SerializeField]private QuestManager questManager;
    private int totalPotion;
    private int item_Counter;


    private void Start() {
        playerInventory.OnQuitSubmitPotion += playerInventory_OnQuitSubmitPotion;
        playerInventory.OnSubmitPotionChoice += playerInventory_OnSubmitPotionChoice;
        inventOnly = submitPotionUI_Inventory.GetInventoryOnly();
        inventOnly.OnItemSubmitPotion += inventOnly_OnItemSubmitPotion;

        totalPotion = questManager.GetTotalPotionNeed();
        // Debug.Log(totalPotion);
        ListItemTerpilih = new List<CauldronItem>();
        item_Counter = 0;
        for(int i=0;i<totalPotion;i++){
            ListItemTerpilih.Add(new CauldronItem().EmptyItem());
        }
    }

    private void playerInventory_OnSubmitPotionChoice(object sender, EventArgs e)
    {
        if(questManager.GetSendername() == charName)
        {
            if(submitPotionUI.GetIsChosePotion()){

                bool isPotionMatch = questManager.CheckPotion(ListItemTerpilih);
                
                if(isPotionMatch){
                    foreach(CauldronItem item in ListItemTerpilih){
                        playerInventory.GetPlayerInventory().TakeItemFromSlot(item.position_InInventory, 1);
                    }
                    //ini jadiin ke finisquest dl ini semua yg dibwh dipindah jadi pas di pintu masuk rumah
                    // playerSave.ChangePlayerLevel();
                    // playerSave.ChangePlayerMode(levelMode.outside);
                    playerSaveManager.ChangePlayerMode(levelMode.finishQuest);
                    questManager.UpdateData_FinishQuest();
                    PauseUI.Instance.SaveData();

                }
                
                HideWHoleUI();
                playerInventory.ClosePlayerInventory();


                //kalo jd ada traveling merchant, ini dibikin aja ke 1 org itu, kalo si 1 org itu showdialoguenya beda(bikin baru_, kalo yg lain ya itu doang)
                //mulai timeline or anything
                if(!isCharacterATravelingMerchant)
                {
                    if(isPotionMatch){
                        Debug.Log(isCharacterATravelingMerchant + " " + gameObject);
                        dialogueManager.ShowDialogue_KirimPotion();
                    }
                    else{
                        dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.potionTidakSesuaiQuest_SubmitPotion);
                    }
                }
                else{
                    if(isPotionMatch){
                        //ntr ganti
                        dialogueManager.ShowDialogue_KirimPotionTravelingMerchant();
                    }
                    else{
                        //ntr ganti
                        dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.potionTidakSesuaiQuest_SubmitPotion);
                    }
                }
            
            
            
            
                //quest manager ngecheck apakah potionnya bener ato salah.
            
            }
            else{
                submitPotionUI_Inventory.DeselectItem_SubmitPotion();
                submitPotionUI.Show_AskingWhichPotion();
                gameManager.ChangeInterfaceType(WitchGameManager.InterfaceType.InventoryAndSubmit);
            }
        }
        
    }

    private void inventOnly_OnItemSubmitPotion(object sender, InventoryOnly.OnItemSubmitPotionkEventArgs e)
    {
        
        
        if(e.isAdd){
            
            AddItemSubmit(e.Position);

            if(item_Counter == totalPotion){
                // Debug.Log("harusnya not here ?" + item_Counter + " " + totalPotion);
                gameManager.ChangeInterfaceType(WitchGameManager.InterfaceType.SubmitPotion);
                submitPotionUI.Show_AreYouSure(itemTerpilih.itemSO.itemName);
            }
            
        }
        else{
            RemoveItemSubmit(e.Position);
        }
        
    }

    private void AddItemSubmit(int selectItem){
        InventorySlot item;
        for(int i=0;i<totalPotion;i++){
            if(ListItemTerpilih[i].isEmpty){
                item = playerInventory.GetPlayerInventory().TakeDataFromSlot(selectItem);
                itemTerpilih = new CauldronItem().AddItem(item.itemSO, item.quantity, selectItem);
                ListItemTerpilih[i] = itemTerpilih;
                item_Counter++;
                // Debug.Log("uda sampe sioni ?");
                break;
            }
        }
        
    }

    private void RemoveItemSubmit(int selectItem){
        for(int i=0;i<totalPotion;i++){
            if(!ListItemTerpilih[i].isEmpty && ListItemTerpilih[i].position_InInventory == selectItem){
                itemTerpilih = new CauldronItem().EmptyItem();
                ListItemTerpilih[i] = itemTerpilih;
                item_Counter--;
                break;
            }
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

    public string GetCharHouseName()
    {
        return charName;
    }
    public bool IsCharacterATravelingMerchant()
    {
        return isCharacterATravelingMerchant;
    }


}
