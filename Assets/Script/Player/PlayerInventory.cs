using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

//ngatur inventory intinya

public class PlayerInventory : MonoBehaviour
{
    [Header("This is for Player Inventory")]
    public static PlayerInventory Instance;
    [SerializeField]private InventoryUI inventoryUI;
    [SerializeField]private InventoryUI ChestInventoryUI, CauldronUI, PenumbukUI; //ini misal buka di chest ato buka di mana gitu dr interactable object, trus dibuat code lg yg ngatur show hide nya, trus pas show dikasih ke sini, pas hide d null
    [SerializeField]private InventoryScriptableObject inventory;

    private int inventorySize;


    [Header("This is for Player Input")]
    [SerializeField]private GameInput gameInput;
    [SerializeField]private WitchGameManager gameManager;
    public event EventHandler OnQuitInventory, OnQuitChest, OnClearPlayerInventory, OnQuitCauldron, OnStartCookingCauldron, OnQuitPenumbuk, OnStopTumbuk, OnQuitSubmitPotion, OnSubmitPotionChoice, OnQuitBed, OnSubmitBed, OnQuitDoor, OnSubmitDoor, OnQuitQuestBox; //OnQuitInventory nyambung ke InventoryUI, OnQuitChest ke function ExampleChest, OnClearPlayerInventory masuk ke Chest, OnQuit dan OnstartCookingCauldroin ke function Cauldron, OnQuitPenumbuk di Penumbuk, OnQuitSubmitPotion & OnSubmitPotionChoice di submitPotion, OnQuitBed & OnSubmitBed buat Bed, OnQuitDoor & OnSubmitDoor buat Door_Outside, OnQuitQuestBox di QuestBox
    private bool isInventoryOpen, isChestOpen, isCauldronOpen;

    private Vector2 keyInputArrowUI;

    [SerializeField]private SubmitPotionUI submitUI;
    [SerializeField]private Bed bed;
    [SerializeField]private Door_Outside door;
    
    [SerializeField]private float inputCooldownTimerMax;
    private float inputCooldownTimer, inputCooldownForInventoryOnly;
    [SerializeField]private PlayerSaveManager playerSave;
    [SerializeField]private WantToSeeTutorialUI want;
    [SerializeField]private WantToResetPuzzle wantReset;

    private bool canStartOpen = false;
    private void Awake() {
        Instance = this;
        inventorySize = inventory.size;
        if(playerSave.GetIsResetSave()){
            playerSave.HasResetSave();
            inventory.RemoveAllItem();
            #if UNITY_EDITOR
            EditorUtility.SetDirty(inventory);
            #endif
        }
    }
    private void Start(){
        isInventoryOpen = false;
        isCauldronOpen = false;
        if(playerSave.GetIsPlayerFromOutside() || playerSave.GetIsSubmitPotion())
        {
            inputCooldownForInventoryOnly = 1.25f;
        }
        else{
            inputCooldownForInventoryOnly = 0.95f;
        }
        
        if(inventory.inventSlot.Count != inventorySize){
            inventory.CreateInventory();
        }
        inputCooldownTimer = 0;

    }


    private void Update()
    {
        Inventory_Input();
        if(!canStartOpen)
        {
            if(inputCooldownForInventoryOnly > 0)
            {
                inputCooldownForInventoryOnly -= Time.deltaTime;
            }
            else{
                canStartOpen = true;
            }
        }
        // Debug.Log(inputCooldownTimer);
    }

    private void Inventory_Input(){
        //Open Inventory
        if(gameManager.IsInGame()){
            
            if(gameManager.IsInGameType() == WitchGameManager.InGameType.normal)
            {
                if(gameInput.GetInputOpenInventory() && !isInventoryOpen && canStartOpen){
                    // Debug.Log("Hi Open");
                    inventoryUI.ShowInventoryUI();
                    isInventoryOpen = true;
                    inputCooldownTimer = inputCooldownTimerMax;
                }
            }
            else if(gameManager.IsInGameType() == WitchGameManager.InGameType.puzzle)
            {
                if(gameInput.GetInputRun())
                {
                    if(playerSave.GetIsMagicalPuzzleThisLevelSolved())
                    {
                        DialogueManager.Instance.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.SudahMenyelesaikanPuzzle_PlayerInventory);
                    }
                    else
                    {
                        gameManager.ChangeInterfaceType(WitchGameManager.InterfaceType.InterfaceYesNoResetPuzzle);
                        wantReset.ShowWantReset();
                    }
                    
                }
            }
            
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.InventoryTime){
            if(isInventoryOpen){
                if(isChestOpen){
                    if((gameInput.GetInputEscape() || gameInput.GetInputOpenInventory_ChestOpen() || gameInput.GetInputEscapeMainMenu()) && inputCooldownTimer <= 0){
                        inputCooldownTimer = inputCooldownTimerMax;
                        OnQuitInventory?.Invoke(this,EventArgs.Empty);
                        ChestInventoryUI.moveChestUI(false);
                        isInventoryOpen = false;
                        gameManager.ChangeInterfaceType(WitchGameManager.InterfaceType.InventoryAndChest); 
                        
                    }
                    
                    else if(gameInput.GetInputClearInventoryPlayer() && inputCooldownTimer <= 0){
                        inputCooldownTimer = inputCooldownTimerMax;
                        OnClearPlayerInventory?.Invoke(this,EventArgs.Empty);
                    }
                    // Debug.Log(gameInput.InputClearInventoryPlayer());
                }
                else{
                    if((gameInput.GetInputEscape() || gameInput.GetInputEscapeMainMenu()) && inputCooldownTimer <= 0){
                        inputCooldownTimer = inputCooldownTimerMax;
                        OnQuitInventory?.Invoke(this,EventArgs.Empty);
                        isInventoryOpen = false;
                    }
                }
            }
            if(inputCooldownTimer <= 0)InputArrowInventory(inventoryUI);
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.InventoryAndCauldron){
            
            if(!isCauldronOpen){
                isCauldronOpen = true;
            }
            else if(isCauldronOpen){
                if((gameInput.GetInputEscape() ||gameInput.GetInputEscapeMainMenu())&& inputCooldownTimer <= 0){
                    inputCooldownTimer = inputCooldownTimerMax;
                    OnQuitCauldron?.Invoke(this,EventArgs.Empty);
                    // isChestOpen = false;
                    isCauldronOpen = false;
                }
                else if(gameInput.GetInputSelectItemForCauldron() && inputCooldownTimer <= 0){
                    inputCooldownTimer = inputCooldownTimerMax;
                    CauldronUI.SelectItem_Cauldron();
                }
                else if(gameInput.GetInputStartCookingForCauldron() && inputCooldownTimer <= 0){
                    inputCooldownTimer = inputCooldownTimerMax;
                    OnStartCookingCauldron?.Invoke(this,EventArgs.Empty);
                }
                if(inputCooldownTimer <= 0)InputArrowInventory(CauldronUI);
            }
            
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.CauldronFire){
            if(gameInput.GetInputEscape() && inputCooldownTimer <= 0){
                inputCooldownTimer = inputCooldownTimerMax;
                OnQuitCauldron?.Invoke(this,EventArgs.Empty);
                // isChestOpen = false;
            }
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.InventoryAndChest){
            isChestOpen = true;
            if(gameInput.GetInputEscape() && inputCooldownTimer <= 0){
                inputCooldownTimer = inputCooldownTimerMax;
                OnQuitChest?.Invoke(this,EventArgs.Empty);
                isChestOpen = false;
            }
            if(inputCooldownTimer <= 0)InputArrowInventory(ChestInventoryUI);
            if(gameInput.GetInputGetKeyQuantity() && inputCooldownTimer <= 0){
                inputCooldownTimer = inputCooldownTimerMax;
                // WordInput.Instance.UndoInputLetterManyWords();
                if(!ChestInventoryUI.isUIEmpty()){
                    gameManager.ChangeInterfaceType(WitchGameManager.InterfaceType.QuantityTime);
                }
                
            }
            if(gameInput.GetInputOpenInventory_ChestOpen() && !isInventoryOpen && inputCooldownTimer <= 0){
                inputCooldownTimer = inputCooldownTimerMax;
                inventoryUI.ShowInventoryUI();
                ChestInventoryUI.moveChestUI(true);
                isInventoryOpen = true;
                WordInput.Instance.UndoInputLetterManyWords();
                
            }
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.QuantityTime){
            if(inputCooldownTimer <= 0)InputArrowInventory_AddQuantity(ChestInventoryUI);
            if(gameInput.GetInputGetKeyQuantity() && inputCooldownTimer <= 0){
                inputCooldownTimer = inputCooldownTimerMax;
                gameManager.ChangeInterfaceType(WitchGameManager.InterfaceType.InventoryAndChest);
            }
            if(gameInput.GetInputEscape() && inputCooldownTimer <= 0){
                inputCooldownTimer = inputCooldownTimerMax;
                OnQuitChest?.Invoke(this,EventArgs.Empty);
                isChestOpen = false;
            }
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.InventoryAndPenumbuk){
            if((gameInput.GetInputEscape() || gameInput.GetInputEscapeMainMenu()) && inputCooldownTimer <= 0){
                inputCooldownTimer = inputCooldownTimerMax;
                OnQuitPenumbuk?.Invoke(this,EventArgs.Empty);
                
                // isChestOpen = false;
            }
            else if(gameInput.GetInputSelectItemForCauldron() && inputCooldownTimer <= 0){
                inputCooldownTimer = inputCooldownTimerMax;
                PenumbukUI.SelectItem_Cauldron();
            }
            if(inputCooldownTimer <= 0)InputArrowInventory(PenumbukUI);
        
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.TumbukTime){
            if(gameInput.GetInputEscape() && inputCooldownTimer <= 0){
                inputCooldownTimer = inputCooldownTimerMax;
                OnQuitPenumbuk?.Invoke(this,EventArgs.Empty);
                // isChestOpen = false;
            }
            if(gameInput.GetInputOpenInventory_ChestOpen() && inputCooldownTimer <= 0){
                // Debug.Log("sini");
                inputCooldownTimer = inputCooldownTimerMax;
                // PenumbukUI.ShowInventory_PenumbukIsOpen();
                WordInput.Instance.UndoInputLetterManyWords();
                OnStopTumbuk?.Invoke(this,EventArgs.Empty); // selanjutnya coba buka penumuk UI, trus bikin ui nya + word manager + progress bar etc etc
            }
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.InventoryAndSubmit){
            if(inputCooldownTimer <= 0)InputArrowInventory(inventoryUI);
            if((gameInput.GetInputEscape()|| gameInput.GetInputEscapeMainMenu())&& inputCooldownTimer <= 0){
                inputCooldownTimer = inputCooldownTimerMax;
                OnQuitSubmitPotion?.Invoke(this, EventArgs.Empty);
                isInventoryOpen = false;
            }
            else if(gameInput.GetInputSelectItemForCauldron() && inputCooldownTimer <= 0){
                inputCooldownTimer = inputCooldownTimerMax;
                inventoryUI.SelectItem_Cauldron();
            }
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.SubmitPotion){
            if((gameInput.GetInputEscape()|| gameInput.GetInputEscapeMainMenu())&& inputCooldownTimer <= 0){
                inputCooldownTimer = inputCooldownTimerMax;
                OnQuitSubmitPotion?.Invoke(this, EventArgs.Empty);
                isInventoryOpen = false;
            }
            else if(gameInput.GetInputSelectItemForCauldron() && inputCooldownTimer <= 0){
                inputCooldownTimer = inputCooldownTimerMax;
                OnSubmitPotionChoice?.Invoke(this,EventArgs.Empty);
            }
            if(inputCooldownTimer <= 0)InputArrowInventory_SubmitPotionChoice();
            
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.InterfaceBed){
            if(gameInput.GetInputEscape() && inputCooldownTimer <= 0){
                inputCooldownTimer = inputCooldownTimerMax;
                OnQuitBed?.Invoke(this, EventArgs.Empty);
            }
            else if(gameInput.GetInputSelectItemForCauldron() && inputCooldownTimer <= 0){
                inputCooldownTimer = inputCooldownTimerMax;
                OnSubmitBed?.Invoke(this,EventArgs.Empty);
            }
            if(inputCooldownTimer <= 0)InputArrowInventory_SubmitBedChoice();
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.InterfaceDoor){
            if(gameInput.GetInputEscape() && inputCooldownTimer <= 0){
                inputCooldownTimer = inputCooldownTimerMax;
                OnQuitDoor?.Invoke(this, EventArgs.Empty);
            }
            else if(gameInput.GetInputSelectItemForCauldron() && inputCooldownTimer <= 0){
                inputCooldownTimer = inputCooldownTimerMax;
                OnSubmitDoor?.Invoke(this,EventArgs.Empty);
            }
            if(inputCooldownTimer <= 0)InputArrowInventory_DoorChoice();
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.InterfaceQuestBox){
            if((gameInput.GetInputQuit_Announcement()) && inputCooldownTimer <= 0){
                inputCooldownTimer = inputCooldownTimerMax;
                OnQuitQuestBox?.Invoke(this, EventArgs.Empty);
            }
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.InterfaceYesNoTutorial)
        {
            if(gameInput.GetInputSelectItemForCauldron() && inputCooldownTimer <= 0){
                inputCooldownTimer = inputCooldownTimerMax;
                want.Choose();
            }
            if(inputCooldownTimer <= 0)InputArrowInventory_TutorialChoice();
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.InterfaceYesNoResetPuzzle)
        {
            if(gameInput.GetInputSelectItemForCauldron() && inputCooldownTimer <= 0){
                inputCooldownTimer = inputCooldownTimerMax;
                wantReset.Choose();
            }
            if(inputCooldownTimer <= 0)InputArrowInventory_ResetPuzzleChoice();
        }
        if(inputCooldownTimer > 0 && !gameManager.IsInGame()){
            inputCooldownTimer -= Time.deltaTime;
        }
        if(gameManager.IsInGame() && inputCooldownTimer <= 0 ){
            inputCooldownTimer = inputCooldownTimerMax;
        }
        
        
        
    }

    public void ShowPlayerInventory(){
        inventoryUI.ShowInventoryUI();
        isInventoryOpen = true;
    }
    private void InputArrowInventory(InventoryUI theInventoryUI){
        keyInputArrowUI = gameInput.GetInputArrow();
        if(keyInputArrowUI.y == 1){
            theInventoryUI.SelectItemUp();
        }
        else if(keyInputArrowUI.y == -1){
            theInventoryUI.SelectItemDown();
        }
        else if(keyInputArrowUI.x == -1){
            theInventoryUI.SelectItemLeft();
        }
        else if(keyInputArrowUI.x == 1){
            theInventoryUI.SelectItemRight();
        }
    }
    private void InputArrowInventory_AddQuantity(InventoryUI theInventoryUI){
        keyInputArrowUI = gameInput.GetInputArrow();
        if(keyInputArrowUI.y == 1){
            theInventoryUI.ChangeQuantityWant(1);
        }
        else if(keyInputArrowUI.y == -1){
            theInventoryUI.ChangeQuantityWant(-1);
        }

    }
    private void InputArrowInventory_SubmitPotionChoice(){
        keyInputArrowUI = gameInput.GetInputArrow();
        if(keyInputArrowUI.x == 1){
            if(submitUI.GetIsChosePotion()){
                submitUI.Change_YesNo();
            }
        }
        else if(keyInputArrowUI.x == -1){
            if(!submitUI.GetIsChosePotion()){
                submitUI.Change_YesNo();
            }
        }

    }
    private void InputArrowInventory_SubmitBedChoice(){
        keyInputArrowUI = gameInput.GetInputArrow();
        if(keyInputArrowUI.x == -1){
            if(!bed.GetIsResetDay()){
                bed.Change_YesNo();
            }
        }
        else if(keyInputArrowUI.x == 1){
            if(bed.GetIsResetDay()){
                bed.Change_YesNo();
            }
        }

    }

    private void InputArrowInventory_DoorChoice(){
        keyInputArrowUI = gameInput.GetInputArrow();
        if(keyInputArrowUI.x == -1){
            if(!door.GetWantToGoIn()){
                door.Change_YesNo();
            }
        }
        else if(keyInputArrowUI.x == 1){
            if(door.GetWantToGoIn()){
                door.Change_YesNo();
            }
        }

    }
    private void InputArrowInventory_TutorialChoice(){
        keyInputArrowUI = gameInput.GetInputArrow();
        want.Change_YesNoTutorial(keyInputArrowUI.x);

    }
    private void InputArrowInventory_ResetPuzzleChoice(){
        keyInputArrowUI = gameInput.GetInputArrow();
        wantReset.Change_YesNoTutorial(keyInputArrowUI.x);

    }

    
    public InventoryScriptableObject GetPlayerInventory(){
        return inventory;
    }
    public int GetInventorySize(){
        return inventorySize;
    }

    public void ClosePlayerInventory(){
        isInventoryOpen = false;
    }
    public bool GetIsChestOpen(){
        return isChestOpen;
    }

}
