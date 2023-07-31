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
        playerInventoryFull_Chest, barangChestHabis_Chest, tidakBerhasilJadi_Cauldron, tidakAdaIngredientMasuk_Cauldron, tidakAdaTempatPotion_Cauldron, tidakAdaTempat_Penumbuk, tidakAdaResep_CauldronPenumbuk, sudahPenuh_Cauldron, ingredientKurang_Cauldron,bukanBahanPotion_InventoryUI, bukanBahanTumbukan_InventoryUI, bukanPotion_InventoryUI, potionTidakSesuaiQuest_SubmitPotion, sedangTidakAdaQuest_InteractObject, cekMailboxDulu_InteractObject,cekQuestDulu_InteractObject,sudahMenyelesaikanSemuaQuest, tidakBisaPakaiPenumbuk
    }
    public enum DialogueTutorial{
        playerTutorialStart, playerCauldron, playerChest, playerDictionary, playerBed, playerTumbuk, playerStartMaking
    }
    public enum DialogueNerimaGift{
        gift1, gift2, gift3, gift4
    }
    
    // private DialogueType dialogueType;
    private WitchGameManager.InterfaceType interfaceType;
    [SerializeField]private WitchGameManager gameManager;
    [SerializeField]private DialogueSystem.DialogueHolder dialogueHolder_Intro, dialogueHolder_Intro2, dialogueHolder_AfterReadingMail,dialogueHolder_WrongChoice_Dialogue, dialogueHolder_KirimPotion, dialogueHolder_Go_Out_Dialogue, dialogueHolder_tutorial, dialogueHolder_NerimaGift;
    //wrong choice itu kek ah inventory player penuh, ah itu bukan itemnya buat ditumbuk
    [Header("Dialogue Wrong Choice")]
    // [SerializeField]private GameObject dialogueWrongChoice_GameObject;
    private DialogueWrongChoice dialogueWrongChoice;
    private DialogueSystem.DialogueLine dialogueLines_WrongChoice, dialogueLines_Tutorial, dialogueLines_NerimaGift;
    private string mainDialogue_WrongChoice;

    [SerializeField]
    [field : TextArea]
    private string dialogue_playerInventoryFull_Chest, dialogue_barangChestHabis_Chest, dialogue_tidakBerhasilJadi_Cauldron, dialogue_tidakAdaIngredientMasuk_Cauldron, dialogue_tidakAdaTempatPotion_Cauldron, dialogue_tidakAdaTempat_Penumbuk,dialogue_tidakAdaResep_CauldronPenumbuk, dialogue_sudahPenuh_Cauldron, dialogue_ingredientKurang_Cauldron,dialogue_bukanBahanPotion_InventoryUI, dialogue_bukanBahanTumbukan_InventoryUI, dialogue_bukanPotion_InventoryUI, dialogue_potionTidakSesuaiQuest_SubmitPotion, dialogue_sedangTidakAdaQuest_InteractObject, dialogue_cekMailboxDulu_InteractObject,dialogue_cekQuestDulu_InteractObject, dialogue_sudahMenyelesaikanSemuaQuest_InteractObject, dialogue_tidakBisaPakaiPenumbuk;

    [Header("Dialogue Kirim Potion")]
    [SerializeField]private FadeNight_StartEnd fadeNight;
    [SerializeField]private QuestBox questBoxUI;
    [SerializeField]private PlayerAnimator playerAnimator;

    [Header("Dialogue Tutorial")]
    [SerializeField]
    [field : TextArea]
    private string dialogueTutorial_StartTutorial,dialogueTutorial_Cauldron, dialogueTutorial_Chest, dialogueTutorial_Dictionary, dialogueTutorial_Bed, dialogueTutorial_Tumbuk, dialogueTutorial_StartMaking;
    private DialogueTutorial dialogueTutorial;

    [SerializeField]private TutorialUI tutorialCauldron, tutorialChest, tutorialDictionary, tutorialBed, tutorialTumbuk;

    [Header("Dialogue Nerima Gift")]
    [SerializeField]
    [field : TextArea]
    private string dialogueNerimaGift_1, dialogueNerimaGift_2, dialogueNerimaGift_3, dialogueNerimaGift_4;
    private DialogueNerimaGift dialogueNerimaGift;


    
    private void Awake() {
        Instance = this;
        dialogueLines_WrongChoice = dialogueHolder_WrongChoice_Dialogue.GetComponentInChildren<DialogueSystem.DialogueLine>();
        if(dialogueHolder_tutorial) dialogueLines_Tutorial = dialogueHolder_tutorial.GetComponentInChildren<DialogueSystem.DialogueLine>();
        if(dialogueHolder_NerimaGift) dialogueLines_NerimaGift = dialogueHolder_NerimaGift.GetComponentInChildren<DialogueSystem.DialogueLine>();
        
        // Debug.Log(dialogueLines_WrongChoice);
    }
    
    private void Start() {
        if(dialogueHolder_Intro){
            dialogueHolder_Intro.OnDialogueFinish += dialogueHolder_Intro_OnDialogueFinish;
        }
        
        if(dialogueHolder_Intro2){
            dialogueHolder_Intro2.OnDialogueFinish += dialogueHolder_Intro2_OnDialogueFinish;
        }
        if(dialogueHolder_AfterReadingMail)dialogueHolder_AfterReadingMail.OnDialogueFinish += dialogueHolder_AfterReadingMail_OnDialogueFinish;
        dialogueHolder_WrongChoice_Dialogue.OnDialogueFinish += dialogueHolder_WrongChoice_OnDialogueFinish;
        dialogueHolder_KirimPotion.OnDialogueFinish += dialogueHolder_KirimPotion_OnDialogueFinish;
        dialogueHolder_Go_Out_Dialogue.OnDialogueFinish += dialogueHolder_Go_Out_Dialogue_OnDialogueFinish;
        if(dialogueHolder_tutorial) dialogueHolder_tutorial.OnDialogueFinish += dialogueHolder_tutorial_OnDialogueFinish;
        if(dialogueHolder_NerimaGift)dialogueHolder_NerimaGift.OnDialogueFinish += dialogueHolder_NerimaGift_OnDialogueFinish;
    }
    private void dialogueHolder_AfterReadingMail_OnDialogueFinish(object sender, EventArgs e)
    {
        gameManager.ChangeToInGame();
    }

    private void dialogueHolder_NerimaGift_OnDialogueFinish(object sender, EventArgs e)
    {
        gameManager.ChangeToInGame();
    }

    private void dialogueHolder_tutorial_OnDialogueFinish(object sender, EventArgs e)
    {
        // Debug.Log("hi");
        
        if(dialogueTutorial == DialogueTutorial.playerTutorialStart){
            // gameManager.ChangeToInGame();
            ShowDialogue_Tutorial(DialogueManager.DialogueTutorial.playerCauldron);
        }
        else if(dialogueTutorial == DialogueTutorial.playerCauldron){
            // gameManager.ChangeToInGame();
            tutorialCauldron.Show_Tutorial();
            
        }
        else if(dialogueTutorial == DialogueTutorial.playerChest){
            // gameManager.ChangeToInGame();
            tutorialChest.Show_Tutorial();
        }
        else if(dialogueTutorial == DialogueTutorial.playerDictionary){
            // gameManager.ChangeToInGame();
            tutorialDictionary.Show_Tutorial();
        }
        else if(dialogueTutorial == DialogueTutorial.playerBed){
            // gameManager.ChangeToInGame();
            tutorialBed.Show_Tutorial();
        }
        else if(dialogueTutorial == DialogueTutorial.playerTumbuk){
            // gameManager.ChangeToInGame();
            tutorialTumbuk.Show_Tutorial();
        }
        else if(dialogueTutorial == DialogueTutorial.playerStartMaking){
            gameManager.ChangeToInGame();
            PlayerSaveManager.Instance.ChangeFirstTimeTutorial();
        }
    }

    private void dialogueHolder_Intro_OnDialogueFinish(object sender, EventArgs e)
    {
        TimelineManager.Instance.Start_IntroWalk();
    }
    private void dialogueHolder_Intro2_OnDialogueFinish(object sender, EventArgs e)
    {
        
        questBoxUI.ShowUI_MainLetter();
        if(playerAnimator){
            playerAnimator.animator.Play("Player_Idle_Down");
            playerAnimator.animator.SetBool("idle", true);
        }
        
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
    public void ShowDialogue_AfterReadingMainLetter(){
        gameManager.ChangeToCinematic();
        dialogueHolder_AfterReadingMail.ShowDialogue();
    }
    public void ShowDialogue_Intro(){
        gameManager.ChangeToCinematic();
        dialogueHolder_Intro.ShowDialogue();
    }
    public void ShowDialogue_Intro2(){
        if(playerAnimator){
            playerAnimator.animator.Play("Player_Idle_Up");
            playerAnimator.animator.SetBool("idle", true);
        }
        
        gameManager.ChangeToCinematic();
        dialogueHolder_Intro2.ShowDialogue();
    }

    public void ShowDialogue_KirimPotion(){
        gameManager.ChangeToCinematic();
        dialogueHolder_KirimPotion.ShowDialogue();
    }
    public void ShowDialogue_Go_Out_Dialogue(){
        
        gameManager.ChangeToCinematic();
        dialogueHolder_Go_Out_Dialogue.ShowDialogue();
    }

    public void ShowDialogue_Gift(DialogueNerimaGift dialogueNerimaGift){
        gameManager.ChangeToCinematic();
        if(dialogueNerimaGift == DialogueNerimaGift.gift1){
            dialogueLines_NerimaGift.ChangeInputText(dialogueNerimaGift_1);
        }
        else if(dialogueNerimaGift == DialogueNerimaGift.gift2){
            dialogueLines_NerimaGift.ChangeInputText(dialogueNerimaGift_2);
        }
        else if(dialogueNerimaGift == DialogueNerimaGift.gift3){
            dialogueLines_NerimaGift.ChangeInputText(dialogueNerimaGift_3);
        }
        else if(dialogueNerimaGift == DialogueNerimaGift.gift4){
            dialogueLines_NerimaGift.ChangeInputText(dialogueNerimaGift_4);
        }
        dialogueHolder_NerimaGift.ShowDialogue();
    }

    public void ShowDialogue_Tutorial(DialogueTutorial dialogueTutorials){
        gameManager.ChangeToCinematic();
        
        dialogueTutorial = dialogueTutorials;
        // Debug.Log(dialogueTutorial);
        if(dialogueTutorials == DialogueTutorial.playerTutorialStart){
            dialogueLines_Tutorial.ChangeInputText(dialogueTutorial_StartTutorial);
        }
        if(dialogueTutorials == DialogueTutorial.playerCauldron){
            dialogueLines_Tutorial.ChangeInputText(dialogueTutorial_Cauldron);
        }
        else if(dialogueTutorials == DialogueTutorial.playerChest){
            dialogueLines_Tutorial.ChangeInputText(dialogueTutorial_Chest);
        }
        else if(dialogueTutorials == DialogueTutorial.playerDictionary){
            dialogueLines_Tutorial.ChangeInputText(dialogueTutorial_Dictionary);
        }
        else if(dialogueTutorials == DialogueTutorial.playerBed){
            dialogueLines_Tutorial.ChangeInputText(dialogueTutorial_Bed);
        }
        else if(dialogueTutorials == DialogueTutorial.playerTumbuk){
            dialogueLines_Tutorial.ChangeInputText(dialogueTutorial_Tumbuk);
        }
        else if(dialogueTutorials == DialogueTutorial.playerStartMaking){
            dialogueLines_Tutorial.ChangeInputText(dialogueTutorial_StartMaking);
        }
        dialogueHolder_tutorial.ShowDialogue();
    }

    //gaperlu fungsi buat intro~~

    public void ShowDialogue_WrongChoice_WithoutBahan(DialogueWrongChoice dialogueWrongChoice){
        interfaceType = gameManager.IsInterfaceType();
        gameManager.ChangeToCinematic();
        if(dialogueWrongChoice == DialogueWrongChoice.playerInventoryFull_Chest){
            dialogueLines_WrongChoice.ChangeInputText(dialogue_playerInventoryFull_Chest);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.barangChestHabis_Chest){
            dialogueLines_WrongChoice.ChangeInputText(dialogue_barangChestHabis_Chest);
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
        else if(dialogueWrongChoice == DialogueWrongChoice.cekMailboxDulu_InteractObject){
            dialogueLines_WrongChoice.ChangeInputText(dialogue_cekMailboxDulu_InteractObject);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.cekQuestDulu_InteractObject){
            dialogueLines_WrongChoice.ChangeInputText(dialogue_cekQuestDulu_InteractObject);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.sudahMenyelesaikanSemuaQuest){
            dialogueLines_WrongChoice.ChangeInputText(dialogue_sudahMenyelesaikanSemuaQuest_InteractObject);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.tidakBisaPakaiPenumbuk){
            dialogueLines_WrongChoice.ChangeInputText(dialogue_tidakBisaPakaiPenumbuk);
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
