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
        playerInventoryFull_Chest, barangChestHabis_Chest, tidakBerhasilJadi_Cauldron, tidakAdaIngredientMasuk_Cauldron, tidakAdaTempatPotion_Cauldron, tidakAdaTempat_Penumbuk, tidakAdaResep_CauldronPenumbuk, sudahPenuh_Cauldron, ingredientKurang_Cauldron,bukanBahanPotion_InventoryUI, bukanBahanTumbukan_InventoryUI, bukanPotion_InventoryUI, potionTidakSesuaiQuest_SubmitPotion, sedangTidakAdaQuest_InteractObject, cekMailboxDulu_InteractObject,cekQuestDulu_InteractObject,sudahMenyelesaikanSemuaQuest_InteractObject, tidakBisaPakaiPenumbuk_InteractObject, SelesaikanQuestSekarang_InteractObject, belumAdaQuestYangDikirimTidur_InteractObject, tidakAdaBarangYangDiminta_InteractObject, belumMengecekKotakSuratLevel1_InteractObject, tidakBisaGerakKeArahSana_ForPuzzle_PlayerMovement, tidakPerluKeKota_GoingToOtherPlace, potionYangDibawaTidakSesuai_GoingToOtherPlace, belumMengantarkanPotionKeRumahPemesan_GoingToOtherPlace, sudahMenyelesaikanPuzzle_PembatasEnding
    }
    public enum DialogueTutorial{
        playerTutorialStart, playerCauldron, playerChest, playerDictionary, playerBed, playerTumbuk, playerSubmitPotion, playerStartMaking
    }
    public enum DialogueNerimaGift{
        gift1, gift2, gift3, gift4
    }
    
    // private DialogueType dialogueType;
    private WitchGameManager.InterfaceType interfaceType;
    private WitchGameManager.InGameType inGameType;
    [SerializeField]private TimelineManager timelineManager;
    [SerializeField]private WitchGameManager gameManager;
    [SerializeField]private DialogueSystem.DialogueHolder dialogueHolder_Intro, dialogueHolder_Intro2, dialogueHolder_AfterReadingMail,dialogueHolder_WrongChoice_Dialogue, dialogueHolder_KirimPotion, dialogueHolder_Go_Out_Dialogue, dialogueHolder_tutorial, dialogueHolder_NerimaGift, dialogueHolder_IstirahatHabisSelesaiQuest, dialogueHolder_PulangDariKota, dialogueHolder_MenujuKeKotaLevel6;
    //wrong choice itu kek ah inventory player penuh, ah itu bukan itemnya buat ditumbuk
    [Header("Dialogue Wrong Choice")]
    // [SerializeField]private GameObject dialogueWrongChoice_GameObject;
    private DialogueWrongChoice dialogueWrongChoice;
    private DialogueSystem.DialogueLine dialogueLines_WrongChoice, dialogueLines_Tutorial, dialogueLines_NerimaGift;


    [Header("Dialogue Kirim Potion")]
    [SerializeField]private FadeNight_StartEnd fadeNight;
    [SerializeField]private QuestBox questBoxUI;
    [SerializeField]private PlayerAnimator playerAnimator;

    [Header("Dialogue Tutorial")]

    private DialogueTutorial dialogueTutorial;

    [SerializeField]private TutorialUI tutorialCauldron, tutorialChest, tutorialDictionary, tutorialBed, tutorialTumbuk, tutorialSubmit;

    [Header("Dialogue Nerima Gift")]

    private DialogueNerimaGift dialogueNerimaGift;

    [SerializeField]private DialogueListScriptableObject dialogueList;
    [Header("Hubungan ama GoingToOtherPlace")]
    [SerializeField] private GoingToOtherPlace goingToOtherPlace;
    
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
        if(dialogueHolder_KirimPotion)dialogueHolder_KirimPotion.OnDialogueFinish += dialogueHolder_KirimPotion_OnDialogueFinish;
        if(dialogueHolder_Go_Out_Dialogue)dialogueHolder_Go_Out_Dialogue.OnDialogueFinish += dialogueHolder_Go_Out_Dialogue_OnDialogueFinish;
        if(dialogueHolder_tutorial) dialogueHolder_tutorial.OnDialogueFinish += dialogueHolder_tutorial_OnDialogueFinish;
        if(dialogueHolder_NerimaGift)dialogueHolder_NerimaGift.OnDialogueFinish += dialogueHolder_NerimaGift_OnDialogueFinish;
        if(dialogueHolder_IstirahatHabisSelesaiQuest)dialogueHolder_IstirahatHabisSelesaiQuest.OnDialogueFinish += dialogueHolder_IstirahatHabisSelesaiQuest_OnDialogueFinish;
        if(dialogueHolder_PulangDariKota) dialogueHolder_PulangDariKota.OnDialogueFinish += dialogueHolder_PulangDariKota_OnDialogueFinish;
        if(dialogueHolder_MenujuKeKotaLevel6) dialogueHolder_MenujuKeKotaLevel6.OnDialogueFinish += dialogueHolder_MenujuKeKotaLevel6_OnDialogueFinish;
    }

    private void dialogueHolder_MenujuKeKotaLevel6_OnDialogueFinish(object sender, EventArgs e)
    {
        goingToOtherPlace.StraightToTown();
    }

    private void dialogueHolder_PulangDariKota_OnDialogueFinish(object sender, EventArgs e)
    {
        goingToOtherPlace.StraightToHome();
    }

    private void dialogueHolder_IstirahatHabisSelesaiQuest_OnDialogueFinish(object sender, EventArgs e)
    {
        fadeNight.ShowUI_Potion();
    }
    private void dialogueHolder_AfterReadingMail_OnDialogueFinish(object sender, EventArgs e)
    {
        gameManager.ChangeToInGame(WitchGameManager.InGameType.normal);
    }

    private void dialogueHolder_NerimaGift_OnDialogueFinish(object sender, EventArgs e)
    {
        gameManager.ChangeToInGame(WitchGameManager.InGameType.normal);
    }

    private void dialogueHolder_tutorial_OnDialogueFinish(object sender, EventArgs e)
    {
        // Debug.Log("hi");
        
        if(dialogueTutorial == DialogueTutorial.playerTutorialStart){
            timelineManager.Start_Tutorials(TimelineManager.TimelineType.TutorialCauldron);
            
        }
        else if(dialogueTutorial == DialogueTutorial.playerCauldron){
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
        else if(dialogueTutorial == DialogueTutorial.playerSubmitPotion){
            tutorialSubmit.Show_Tutorial();
        }
        else if(dialogueTutorial == DialogueTutorial.playerStartMaking){
            gameManager.ChangeToInGame(WitchGameManager.InGameType.normal);
            PlayerSaveManager.Instance.ChangeFirstTimeTutorial();
        }
    }

    private void dialogueHolder_Intro_OnDialogueFinish(object sender, EventArgs e)
    {
        timelineManager.Start_IntroWalk();
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
            gameManager.ChangeToInGame(inGameType);
        }
        else{
            gameManager.ChangeInterfaceType(interfaceType);
        }
    }

    private void dialogueHolder_KirimPotion_OnDialogueFinish(object sender, EventArgs e)
    {
        // fadeNight.ShowUI_Potion();
        //jadi change to in game karena berarti uda selesai, dan masi bisa keliling
        gameManager.ChangeToInGame(WitchGameManager.InGameType.normal);
    }
    private void dialogueHolder_Go_Out_Dialogue_OnDialogueFinish(object sender, EventArgs e)
    {
        PlayerSaveManager.Instance.HasDone_GoOutDialogue();
        gameManager.ChangeToInGame(WitchGameManager.InGameType.normal);
        
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

    public void ShowDialogue_IstirahatHabisSelesaiQuest(){
        
        gameManager.ChangeToCinematic();
        dialogueHolder_IstirahatHabisSelesaiQuest.ShowDialogue();
    }
    public void ShowDialogue_PulangDariKota()
    {
        gameManager.ChangeToCinematic();
        dialogueHolder_PulangDariKota.ShowDialogue();
    }
    public void ShowDialogue_MenujuKotaLevel6()
    {
        gameManager.ChangeToCinematic();
        dialogueHolder_MenujuKeKotaLevel6.ShowDialogue();
    }

    public void ShowDialogue_Gift(DialogueNerimaGift dialogueNerimaGift){
        gameManager.ChangeToCinematic();
        if(dialogueNerimaGift == DialogueNerimaGift.gift1){
            dialogueLines_NerimaGift.ChangeInputText(dialogueList.dialogueNerimaGift_1);
        }
        else if(dialogueNerimaGift == DialogueNerimaGift.gift2){
            dialogueLines_NerimaGift.ChangeInputText(dialogueList.dialogueNerimaGift_2);
        }
        else if(dialogueNerimaGift == DialogueNerimaGift.gift3){
            dialogueLines_NerimaGift.ChangeInputText(dialogueList.dialogueNerimaGift_3);
        }
        else if(dialogueNerimaGift == DialogueNerimaGift.gift4){
            dialogueLines_NerimaGift.ChangeInputText(dialogueList.dialogueNerimaGift_4);
        }
        dialogueHolder_NerimaGift.ShowDialogue();
    }

    public void ShowDialogue_Tutorial(DialogueTutorial dialogueTutorials){
        gameManager.ChangeToCinematic();
        
        dialogueTutorial = dialogueTutorials;
        // Debug.Log(dialogueTutorial);
        if(dialogueTutorials == DialogueTutorial.playerTutorialStart){
            dialogueLines_Tutorial.ChangeInputText(dialogueList.dialogueTutorial_StartTutorial);
        }
        if(dialogueTutorials == DialogueTutorial.playerCauldron){
            dialogueLines_Tutorial.ChangeInputText(dialogueList.dialogueTutorial_Cauldron);
        }
        else if(dialogueTutorials == DialogueTutorial.playerChest){
            dialogueLines_Tutorial.ChangeInputText(dialogueList.dialogueTutorial_Chest);
        }
        else if(dialogueTutorials == DialogueTutorial.playerDictionary){
            dialogueLines_Tutorial.ChangeInputText(dialogueList.dialogueTutorial_Dictionary);
        }
        else if(dialogueTutorials == DialogueTutorial.playerBed){
            dialogueLines_Tutorial.ChangeInputText(dialogueList.dialogueTutorial_Bed);
        }
        else if(dialogueTutorials == DialogueTutorial.playerTumbuk){
            dialogueLines_Tutorial.ChangeInputText(dialogueList.dialogueTutorial_Tumbuk);
        }
        else if(dialogueTutorials == DialogueTutorial.playerSubmitPotion){
            dialogueLines_Tutorial.ChangeInputText(dialogueList.dialogueTutorial_SubmitPotion);
        }
        else if(dialogueTutorials == DialogueTutorial.playerStartMaking){
            dialogueLines_Tutorial.ChangeInputText(dialogueList.dialogueTutorial_StartMaking);
        }
        dialogueHolder_tutorial.ShowDialogue();
    }

    //gaperlu fungsi buat intro~~

    public void ShowDialogue_WrongChoice_WithoutBahan(DialogueWrongChoice dialogueWrongChoice){
        interfaceType = gameManager.IsInterfaceType();
        if(gameManager.IsInGameType() != WitchGameManager.InGameType.none)
        {
            inGameType = gameManager.IsInGameType();
        }
        gameManager.ChangeToCinematic();
        if(dialogueWrongChoice == DialogueWrongChoice.playerInventoryFull_Chest){
            dialogueLines_WrongChoice.ChangeInputText(dialogueList.dialogue_playerInventoryFull_Chest);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.barangChestHabis_Chest){
            dialogueLines_WrongChoice.ChangeInputText(dialogueList.dialogue_barangChestHabis_Chest);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.tidakBerhasilJadi_Cauldron){
            dialogueLines_WrongChoice.ChangeInputText(dialogueList.dialogue_tidakBerhasilJadi_Cauldron);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.tidakAdaIngredientMasuk_Cauldron){
            dialogueLines_WrongChoice.ChangeInputText(dialogueList.dialogue_tidakAdaIngredientMasuk_Cauldron);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.tidakAdaTempatPotion_Cauldron){
            dialogueLines_WrongChoice.ChangeInputText(dialogueList.dialogue_tidakAdaTempatPotion_Cauldron);
            
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.tidakAdaTempat_Penumbuk){
            dialogueLines_WrongChoice.ChangeInputText(dialogueList.dialogue_tidakAdaTempat_Penumbuk);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.tidakAdaResep_CauldronPenumbuk){
            dialogueLines_WrongChoice.ChangeInputText(dialogueList.dialogue_tidakAdaResep_CauldronPenumbuk);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.sudahPenuh_Cauldron){
            dialogueLines_WrongChoice.ChangeInputText(dialogueList.dialogue_sudahPenuh_Cauldron);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.ingredientKurang_Cauldron){
            dialogueLines_WrongChoice.ChangeInputText(dialogueList.dialogue_ingredientKurang_Cauldron);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.potionTidakSesuaiQuest_SubmitPotion){
            dialogueLines_WrongChoice.ChangeInputText(dialogueList.dialogue_potionTidakSesuaiQuest_SubmitPotion);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.sedangTidakAdaQuest_InteractObject){
            dialogueLines_WrongChoice.ChangeInputText(dialogueList.dialogue_sedangTidakAdaQuest_InteractObject);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.cekMailboxDulu_InteractObject){
            dialogueLines_WrongChoice.ChangeInputText(dialogueList.dialogue_cekMailboxDulu_InteractObject);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.cekQuestDulu_InteractObject){
            dialogueLines_WrongChoice.ChangeInputText(dialogueList.dialogue_cekQuestDulu_InteractObject);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.sudahMenyelesaikanSemuaQuest_InteractObject){
            dialogueLines_WrongChoice.ChangeInputText(dialogueList.dialogue_sudahMenyelesaikanSemuaQuest_InteractObject);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.tidakBisaPakaiPenumbuk_InteractObject){
            dialogueLines_WrongChoice.ChangeInputText(dialogueList.dialogue_tidakBisaPakaiPenumbuk);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.SelesaikanQuestSekarang_InteractObject)
        {
            dialogueLines_WrongChoice.ChangeInputText(dialogueList.dialogue_SelesaikanQuestSekarang_InteractObject);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.belumAdaQuestYangDikirimTidur_InteractObject)
        {
            dialogueLines_WrongChoice.ChangeInputText(dialogueList.dialogue_belumAdaQuestYangDikirimTidur_InteractObject);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.belumMengecekKotakSuratLevel1_InteractObject)
        {
            
            dialogueLines_WrongChoice.ChangeInputText(dialogueList.dialogue_belumMengecekKotakSuratLevel1_InteractObject);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.tidakBisaGerakKeArahSana_ForPuzzle_PlayerMovement)
        {
            dialogueLines_WrongChoice.ChangeInputText(dialogueList.dialogue_tidakBisaGerakKeArahSana_ForPuzzle_PlayerMovement);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.tidakPerluKeKota_GoingToOtherPlace)
        {
            dialogueLines_WrongChoice.ChangeInputText(dialogueList.dialogue_tidakPerluKeKota_GoingToOtherPlace);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.potionYangDibawaTidakSesuai_GoingToOtherPlace)
        {
            dialogueLines_WrongChoice.ChangeInputText(dialogueList.dialogue_potionYangDibawaTidakSesuai_GoingToOtherPlace);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.sudahMenyelesaikanPuzzle_PembatasEnding)
        {
            dialogueLines_WrongChoice.ChangeInputText(dialogueList.dialogue_sudahMenyelesaikanPuzzle_PembatasEnding);
        }
        dialogueHolder_WrongChoice_Dialogue.ShowDialogue();
    }
    public void ShowDialogue_WrongChoice_WithBahan(DialogueWrongChoice dialogueWrongChoice, string itemName){
        interfaceType = gameManager.IsInterfaceType();
        if(gameManager.IsInGameType() != WitchGameManager.InGameType.none)
        {
            inGameType = gameManager.IsInGameType();
        }
        gameManager.ChangeToCinematic();
        // dialogueWrongChoice_GameObject.SetActive(true);
        if(dialogueWrongChoice == DialogueWrongChoice.bukanBahanPotion_InventoryUI){
            dialogueLines_WrongChoice.ChangeInputText(itemName + dialogueList.dialogue_bukanBahanPotion_InventoryUI);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.bukanBahanTumbukan_InventoryUI){
            dialogueLines_WrongChoice.ChangeInputText(itemName + dialogueList.dialogue_bukanBahanTumbukan_InventoryUI);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.bukanPotion_InventoryUI){
            dialogueLines_WrongChoice.ChangeInputText(itemName + dialogueList.dialogue_bukanPotion_InventoryUI);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.tidakAdaBarangYangDiminta_InteractObject){
            dialogueLines_WrongChoice.ChangeInputText(dialogueList.dialogue_tidakAdaBarangYangDiminta1_InteractObject + itemName + ". " + dialogueList.dialogue_tidakAdaBarangYangDiminta2_InteractObject);
        }
        else if(dialogueWrongChoice == DialogueWrongChoice.belumMengantarkanPotionKeRumahPemesan_GoingToOtherPlace)
        {
            dialogueLines_WrongChoice.ChangeInputText(dialogueList.dialogue_belumMengantarkanPotionKeRumahPemesan_GoingToOtherPlace + itemName + ". ");
        }
        
        dialogueHolder_WrongChoice_Dialogue.ShowDialogue();
    }
}
