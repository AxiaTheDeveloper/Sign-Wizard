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
        playerInventoryFull_Chest, barangChestHabis_Chest, tidakBerhasilJadi_Cauldron, tidakAdaIngredientMasuk_Cauldron, tidakAdaTempatPotion_Cauldron, tidakAdaResep_CauldronPenumbuk, sudahPenuh_Cauldron, bukanBahanPotion_InventoryUI, bukanBahanTumbukan_InventoryUI
    }

    // private DialogueType dialogueType;
    private WitchGameManager.InterfaceType interfaceType;
    [SerializeField]private WitchGameManager gameManager;
    [SerializeField]private DialogueSystem.DialogueHolder dialogueHolder_Intro, dialogueHolder_WrongChoice_Dialogue; 
    //wrong choice itu kek ah inventory player penuh, ah itu bukan itemnya buat ditumbuk
    [Header("Dialogue Wrong Choice")]
    [SerializeField]private GameObject dialogueWrongChoice_GameObject;
    private DialogueWrongChoice dialogueWrongChoice;
    private DialogueSystem.DialogueLine dialogueLines_WrongChoice;
    private string mainDialogue_WrongChoice;

    [SerializeField]
    [field : TextArea]
    private string dialogue_playerInventoryFull_Chest, dialogue_barangChestHabis_Chest, dialogue_tidakBerhasilJadi_Cauldron, dialogue_tidakAdaIngredientMasuk_Cauldron, dialogue_tidakAdaTempatPotion_Cauldron, dialogue_tidakAdaResep_CauldronPenumbuk, dialogue_sudahPenuh_Cauldron, dialogue_bukanBahanPotion_InventoryUI, dialogue_bukanBahanTumbukan_InventoryUI;
    private void Awake() {
        Instance = this;
        dialogueLines_WrongChoice = dialogueHolder_WrongChoice_Dialogue.GetComponentInChildren<DialogueSystem.DialogueLine>();
        Debug.Log(dialogueLines_WrongChoice);
    }
    
    private void Start() {
        dialogueHolder_Intro.OnDialogueFinish += dialogueHolder_Intro_OnDialogueFinish;
        dialogueHolder_WrongChoice_Dialogue.OnDialogueFinish += dialogueHolder_WrongChoice_OnDialogueFinish;
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
        // dialogueWrongChoice_GameObject.SetActive(false);
        
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
        else if(dialogueWrongChoice == DialogueWrongChoice.tidakAdaResep_CauldronPenumbuk){
            dialogueLines_WrongChoice.ChangeInputText(dialogue_tidakAdaResep_CauldronPenumbuk);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.sudahPenuh_Cauldron){
            dialogueLines_WrongChoice.ChangeInputText(dialogue_sudahPenuh_Cauldron);
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
        dialogueHolder_WrongChoice_Dialogue.ShowDialogue();
    }
}
