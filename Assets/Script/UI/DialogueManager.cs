using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    // private enum DialogueType{
    //     inGame, Interface
    // }
    public static DialogueManager Instance{get; private set;}
    public enum DialogueWrongChoice{
        playerInventoryFull_Chest, barangChestHabis_Chest, tidakBerhasilJadi_Cauldron, tidakAdaIngredientMasuk_Cauldron, tidakAdaTempatPotion_Cauldron, tidakAdaTempat_Penumbuk, tidakAdaResep_CauldronPenumbuk, sudahPenuh_Cauldron, ingredientKurang_Cauldron,bukanBahanPotion_InventoryUI, bukanBahanTumbukan_InventoryUI, bukanPotion_InventoryUI, potionTidakSesuaiQuest_SubmitPotion, sedangTidakAdaQuest_InteractObject, cekQuestDulu_InteractObject
    }

    // private DialogueType dialogueType;
    private WitchGameManager.InterfaceType interfaceType;
    [SerializeField]private WitchGameManager gameManager;
    [SerializeField]private DialogueSystem.DialogueHolder dialogueHolder_Intro, dialogueHolder_WrongChoice_Dialogue, dialogueHolder_KirimPotion, dialogueHolder_Go_Out_Dialogue; 
    //wrong choice itu kek ah inventory player penuh, ah itu bukan itemnya buat ditumbuk
    [Header("Dialogue Wrong Choice")]
    // [SerializeField]private GameObject dialogueWrongChoice_GameObject;
    private DialogueWrongChoice dialogueWrongChoice;
    private DialogueSystem.DialogueLine dialogueLines_WrongChoice;
    private string mainDialogue_WrongChoice;

    [SerializeField]
    [field : TextArea]
    private string dialogue_playerInventoryFull_Chest, dialogue_barangChestHabis_Chest, dialogue_tidakBerhasilJadi_Cauldron, dialogue_tidakAdaIngredientMasuk_Cauldron, dialogue_tidakAdaTempatPotion_Cauldron, dialogue_tidakAdaTempat_Penumbuk,dialogue_tidakAdaResep_CauldronPenumbuk, dialogue_sudahPenuh_Cauldron, dialogue_ingredientKurang_Cauldron,dialogue_bukanBahanPotion_InventoryUI, dialogue_bukanBahanTumbukan_InventoryUI, dialogue_bukanPotion_InventoryUI, dialogue_potionTidakSesuaiQuest_SubmitPotion, dialogue_sedangTidakAdaQuest_InteractObject, dialogue_cekQuestDulu_InteractObject;

    [Header("Dialogue Kirim Potion")]
    [SerializeField]private FadeNight_StartEnd fadeNight;
    private void Awake() {
        Instance = this;
        dialogueLines_WrongChoice = dialogueHolder_WrongChoice_Dialogue.GetComponentInChildren<DialogueSystem.DialogueLine>();
        // Debug.Log(dialogueLines_WrongChoice);
    }
    
    private void Start() {
        dialogueHolder_Intro.OnDialogueFinish += dialogueHolder_Intro_OnDialogueFinish;
        dialogueHolder_WrongChoice_Dialogue.OnDialogueFinish += dialogueHolder_WrongChoice_OnDialogueFinish;
        dialogueHolder_KirimPotion.OnDialogueFinish += dialogueHolder_KirimPotion_OnDialogueFinish;
        dialogueHolder_Go_Out_Dialogue.OnDialogueFinish += dialogueHolder_Go_Out_Dialogue_OnDialogueFinish;
    }

    private void dialogueHolder_Intro_OnDialogueFinish(object sender, EventArgs e)
    {
        gameManager.ChangeToInGame();
    }
    private void dialogueHolder_WrongChoice_OnDialogueFinish(object sender, EventArgs e)
    {
        if(interfaceType == WitchGameManager.InterfaceType.none){
            gameManager.ChangeToInGame();
        }
        else{
            gameManager.ChangeInterfaceType(interfaceType);
        }
    }

    private void dialogueHolder_KirimPotion_OnDialogueFinish(object sender, EventArgs e)
    {
        fadeNight.ShowUI_Potion();
    }
    private void dialogueHolder_Go_Out_Dialogue_OnDialogueFinish(object sender, EventArgs e)
    {
        PlayerSaveManager.Instance.HasDone_GoOutDialogue();
        gameManager.ChangeToInGame();
        // TimelineManager.Instance.Start_GoOutside();
    }

    public void ShowDialogue_KirimPotion(){
        gameManager.ChangeToCinematic();
        dialogueHolder_KirimPotion.ShowDialogue();
    }
    public void ShowDialogue_Go_Out_Dialogue(){
        gameManager.ChangeToCinematic();
        dialogueHolder_Go_Out_Dialogue.ShowDialogue();
    }

    //gaperlu fungsi buat intro~~

    public void ShowDialogue_WrongChoice_WithoutBahan(DialogueWrongChoice dialogueWrongChoice){
        interfaceType = gameManager.IsInterfaceType();
        gameManager.ChangeToCinematic();
        
        if(dialogueWrongChoice == DialogueWrongChoice.playerInventoryFull_Chest){
            dialogueLines_WrongChoice.ChangeInputText(dialogue_playerInventoryFull_Chest);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.barangChestHabis_Chest){
            // Debug.Log("Salahdisini" + dialogueLines_WrongChoice);
            dialogueLines_WrongChoice.ChangeInputText(dialogue_barangChestHabis_Chest);
            // Debug.Log("Salahdisiniya?");
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.tidakBerhasilJadi_Cauldron){
            dialogueLines_WrongChoice.ChangeInputText(dialogue_tidakBerhasilJadi_Cauldron);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.tidakAdaIngredientMasuk_Cauldron){
            dialogueLines_WrongChoice.ChangeInputText(dialogue_tidakAdaIngredientMasuk_Cauldron);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.tidakAdaTempatPotion_Cauldron){
            dialogueLines_WrongChoice.ChangeInputText(dialogue_tidakAdaTempatPotion_Cauldron);
            
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.tidakAdaTempat_Penumbuk){
            dialogueLines_WrongChoice.ChangeInputText(dialogue_tidakAdaTempat_Penumbuk);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.tidakAdaResep_CauldronPenumbuk){
            dialogueLines_WrongChoice.ChangeInputText(dialogue_tidakAdaResep_CauldronPenumbuk);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.sudahPenuh_Cauldron){
            dialogueLines_WrongChoice.ChangeInputText(dialogue_sudahPenuh_Cauldron);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.ingredientKurang_Cauldron){
            dialogueLines_WrongChoice.ChangeInputText(dialogue_ingredientKurang_Cauldron);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.potionTidakSesuaiQuest_SubmitPotion){
            dialogueLines_WrongChoice.ChangeInputText(dialogue_potionTidakSesuaiQuest_SubmitPotion);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.sedangTidakAdaQuest_InteractObject){
            dialogueLines_WrongChoice.ChangeInputText(dialogue_sedangTidakAdaQuest_InteractObject);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.cekQuestDulu_InteractObject){
            dialogueLines_WrongChoice.ChangeInputText(dialogue_cekQuestDulu_InteractObject);
        }
        dialogueHolder_WrongChoice_Dialogue.ShowDialogue();
    }
    public void ShowDialogue_WrongChoice_WithBahan(DialogueWrongChoice dialogueWrongChoice, string itemName){
        interfaceType = gameManager.IsInterfaceType();
        gameManager.ChangeToCinematic();
        // dialogueWrongChoice_GameObject.SetActive(true);
        if(dialogueWrongChoice == DialogueWrongChoice.bukanBahanPotion_InventoryUI){
            dialogueLines_WrongChoice.ChangeInputText(itemName + dialogue_bukanBahanPotion_InventoryUI);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.bukanBahanTumbukan_InventoryUI){
            dialogueLines_WrongChoice.ChangeInputText(itemName + dialogue_bukanBahanTumbukan_InventoryUI);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.bukanPotion_InventoryUI){
            dialogueLines_WrongChoice.ChangeInputText(itemName + dialogue_bukanPotion_InventoryUI);
        }
        
        dialogueHolder_WrongChoice_Dialogue.ShowDialogue();
    }
}
