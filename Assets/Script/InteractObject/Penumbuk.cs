using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Penumbuk : MonoBehaviour
{
    [SerializeField]private InventoryUI PenumbukUI_Inventory;
    [SerializeField]private PenumbukUI PenumbukUI_Tumbuk;
    private InventoryPenumbuk inventPenumbuk;
    [SerializeField]private WitchGameManager gameManager;
    [SerializeField]private QuestManager questManager;
    [SerializeField]private PlayerInventory playerInventory;
    [SerializeField]private Announcement_SuccesfullGetItem announcementUI;
    [SerializeField]private DialogueManager dialogueManager;

    [Header("Buat di tumbuk")]
    private CauldronItem itemTerpilih;
    [SerializeField]private TumbukRecipeScriptableObject[] recipeList;
    private TumbukRecipeScriptableObject chosenRecipe;
    private float progress_perTumbuk;
    [SerializeField]private float maxProgress = 100, penguranganProgress;
    private float progressNow;

    [SerializeField]private WordInput wordInput;
    [SerializeField]private WordManager[] wordManager;
    [SerializeField]private FinishWordDoFunction finishFunction1;

    public event EventHandler<OnChangeProgressEventArgs> OnChangeProgress;//kirim ke penumbukui
    public class OnChangeProgressEventArgs : EventArgs{
        public float progressFill;
    } 

    private void Start() {
        playerInventory.OnQuitPenumbuk += playerInventory_OnQuitPenumbuk;
        playerInventory.OnStopTumbuk += playerInventory_OnStopTumbuk;
        inventPenumbuk = PenumbukUI_Inventory.GetInventoryPenumbuk();
        inventPenumbuk.OnItemPenumbuk += inventPenumbuk_OnItemPenumbuk;

        finishFunction1.OnTumbuk += finishFunction_OnTumbuk;
        progressNow = 0;


        itemTerpilih = new CauldronItem().EmptyItem();
        PenumbukUI_Tumbuk.UpdateVisualInventorySlot(itemTerpilih);
        chosenRecipe = null;
        progressNow = 0; 
        OnChangeProgress?.Invoke(this, new OnChangeProgressEventArgs{
            progressFill = progressNow
        });
    }
    private void finishFunction_OnTumbuk(object sender, EventArgs e)
    {
        Tumbuk();
        SoundManager.Instance.PlayMortar();
    }

    private void playerInventory_OnQuitPenumbuk(object sender, EventArgs e)
    {
        CloseWholeUI();
    }
    private void playerInventory_OnStopTumbuk(object sender, EventArgs e)
    {
        
        PenumbukUI_Inventory.ShowInventory_PenumbukIsOpen();
        PenumbukUI_Tumbuk.CloseWordUI();
        itemTerpilih = new CauldronItem().EmptyItem();
        PenumbukUI_Tumbuk.UpdateVisualInventorySlot(itemTerpilih);
        chosenRecipe = null;
        progressNow = 0;
        OnChangeProgress?.Invoke(this, new OnChangeProgressEventArgs{
            progressFill = progressNow
        });
        //updet visual bar
    }
    private void inventPenumbuk_OnItemPenumbuk(object sender, InventoryPenumbuk.OnItemPenumbukEventArgs e)
    {
        
        if(e.isAdd){
            if(playerInventory.GetPlayerInventory().isFull){
                dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.tidakAdaTempat_Penumbuk);
            }
            else{
                InventorySlot item;
                item = playerInventory.GetPlayerInventory().TakeDataFromSlot(e.Position);
                itemTerpilih = new CauldronItem().AddItem(item.itemSO, item.quantity, e.Position);
                PenumbukUI_Tumbuk.UpdateVisualInventorySlot(itemTerpilih);
                PenumbukUI_Inventory.HideInventory_PenumbukIsOpen();
                CheckRecipe(item.itemSO);
            }

            
        }
        else{
            itemTerpilih = new CauldronItem().EmptyItem();
        }
        
    }
    private void CheckRecipe(ItemScriptableObject input_ItemSO){
        chosenRecipe = null;
        for(int i=0;i<recipeList.Length;i++){
            TumbukRecipeScriptableObject recipe = recipeList[i];
            
            bool isContainMatch = true;
            if(recipe.Ingredient != input_ItemSO){
                isContainMatch = false;
            }
            if(isContainMatch){
                chosenRecipe = recipe;
                break;
            }
        }
        if(chosenRecipe){
            progressNow = 0;
            progress_perTumbuk = questManager.GetProgressPerTumbuk_QuestNow();
            wordInput.ChangeAdaWord(true);
            foreach(WordManager wordMn in wordManager){
                wordMn.createWord();
            }
            PenumbukUI_Tumbuk.ShowTumbukUI();
            
        } 
        else{
            //hrsnya ini ga mungkin trjdi krn si player ga mungkin bs ambil item yg merupakan bahan tumbukkan yg resepnya sendiri blm kebuka
            PenumbukUI_Inventory.ShowInventory_PenumbukIsOpen();
            dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.tidakAdaResep_CauldronPenumbuk);
        }
        
    }

    private void Update() {
        if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.TumbukTime){
            if(progressNow > 0 && progressNow < maxProgress){
                progressNow-= (penguranganProgress * Time.deltaTime);
                OnChangeProgress?.Invoke(this, new OnChangeProgressEventArgs{
                    progressFill = progressNow/maxProgress
                });
            }
            if(progressNow < 0){
                progressNow = 0;
                OnChangeProgress?.Invoke(this, new OnChangeProgressEventArgs{
                    progressFill = progressNow
                });
            }
            // Debug.Log(progressNow);
        }   
    }

    public void Tumbuk(){

        progressNow += progress_perTumbuk;
        if(progressNow >= maxProgress){
            progressNow = maxProgress;
            playerInventory.GetPlayerInventory().TakeItemFromSlot(itemTerpilih.position_InInventory, 1);
            playerInventory.GetPlayerInventory().AddItemToSlot(chosenRecipe.output_Ingredient, 1);
            #if UNITY_EDITOR
            EditorUtility.SetDirty(playerInventory.GetPlayerInventory());
            #endif
            announcementUI.AddData(chosenRecipe.output_Ingredient);
            CloseWholeUI();
            gameManager.ChangeToCinematic();
            announcementUI.Show();
        }
        OnChangeProgress?.Invoke(this, new OnChangeProgressEventArgs{
            progressFill = progressNow/maxProgress
        });
        //updet visual
    }

    public void ShowWholeUI(){
        wordInput.GetWordManager(wordManager);
        PenumbukUI_Tumbuk.ShowPenumbukUI();
        PenumbukUI_Inventory.ShowInventoryUI();
        gameManager.ChangeInterfaceType(WitchGameManager.InterfaceType.InventoryAndPenumbuk);
        // change game state
    }
    public void CloseWholeUI(){
        gameManager.ChangeToInGame();
        PenumbukUI_Inventory.HideInventoryUI();
        PenumbukUI_Tumbuk.HidePenumbukUI();
        itemTerpilih = new CauldronItem().EmptyItem();
        PenumbukUI_Tumbuk.UpdateVisualInventorySlot(itemTerpilih);
        chosenRecipe = null;
        progressNow = 0; 
        OnChangeProgress?.Invoke(this, new OnChangeProgressEventArgs{
            progressFill = progressNow
        });
        //updet visual;

        //change game state
        //di sini delete semua gameobject ???
    }
}
