using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    public event EventHandler OnQuitInventory, OnQuitChest, OnClearPlayerInventory, OnQuitCauldron, OnStartCookingCauldron, OnQuitPenumbuk, OnStopTumbuk; //OnQuitInventory nyambung ke InventoryUI, OnQuitChest ke function ExampleChest, OnClearPlayerInventory masuk ke Chest, OnQuit dan OnstartCookingCauldroin ke function Cauldron, OnQuitPenumbuk di Penumbuk
    private bool isInventoryOpen, isChestOpen;

    private Vector2 keyInputArrowUI;
    

    private void Awake() {
        Instance = this;
        inventorySize = inventory.size;
    }
    private void Start(){
        isInventoryOpen = false;


        if(inventory.inventSlot.Count != inventorySize){
            inventory.CreateInventory();
        }

    }


    private void Update()
    {
        Inventory_Input();
        
    }

    private void Inventory_Input(){
        //Open Inventory
        if(gameManager.IsInGame()){
            
            if(gameInput.GetInputOpenInventory() && !isInventoryOpen){
                // Debug.Log("Hi Open");
                inventoryUI.ShowInventoryUI();
                isInventoryOpen = true;
            }
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.InventoryTime){
            if(isInventoryOpen){
                if(isChestOpen){
                    if(gameInput.GetInputEscape() || gameInput.GetInputOpenInventory_ChestOpen()){
                        OnQuitInventory?.Invoke(this,EventArgs.Empty);
                        isInventoryOpen = false;
                        gameManager.ChangeInterfaceType(WitchGameManager.InterfaceType.InventoryAndChest);
                    }
                    
                    else if(gameInput.GetInputClearInventoryPlayer()){
                        OnClearPlayerInventory?.Invoke(this,EventArgs.Empty);
                    }
                    // Debug.Log(gameInput.InputClearInventoryPlayer());
                }
                else{
                    if(gameInput.GetInputEscape() || gameInput.GetInputOpenInventory()){
                        OnQuitInventory?.Invoke(this,EventArgs.Empty);
                        isInventoryOpen = false;
                    }
                }
            }
            InputArrowInventory(inventoryUI);
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.InventoryAndCauldron){
            if(gameInput.GetInputEscape()){
                OnQuitCauldron?.Invoke(this,EventArgs.Empty);
                // isChestOpen = false;
            }
            else if(gameInput.GetInputSelectItemForCauldron()){
                CauldronUI.SelectItem_Cauldron();
            }
            else if(gameInput.GetInputStartCookingForCauldron()){
                OnStartCookingCauldron?.Invoke(this,EventArgs.Empty);
            }
            InputArrowInventory(CauldronUI);
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.CauldronFire){
            if(gameInput.GetInputEscape()){
                OnQuitCauldron?.Invoke(this,EventArgs.Empty);
                // isChestOpen = false;
            }
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.InventoryAndChest){
            isChestOpen = true;
            if(gameInput.GetInputEscape()){
                OnQuitChest?.Invoke(this,EventArgs.Empty);
                isChestOpen = false;
            }
            InputArrowInventory(ChestInventoryUI);
            if(gameInput.GetInputGetKeyTabDown()){
                gameManager.ChangeInterfaceType(WitchGameManager.InterfaceType.QuantityTime);
            }
            if(gameInput.GetInputOpenInventory_ChestOpen() && !isInventoryOpen){
                inventoryUI.ShowInventoryUI();
                isInventoryOpen = true;
                WordInput.Instance.UndoInputLetterManyWords();
                
            }
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.QuantityTime){
            InputArrowInventory_AddQuantity(ChestInventoryUI);
            if(gameInput.GetInputGetKeyTabDown()){
                gameManager.ChangeInterfaceType(WitchGameManager.InterfaceType.InventoryAndChest);
            }
            if(gameInput.GetInputEscape()){
                OnQuitChest?.Invoke(this,EventArgs.Empty);
                isChestOpen = false;
            }
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.InventoryAndPenumbuk){
            if(gameInput.GetInputEscape()){
                OnQuitPenumbuk?.Invoke(this,EventArgs.Empty);
                // isChestOpen = false;
            }
            else if(gameInput.GetInputSelectItemForCauldron()){
                PenumbukUI.SelectItem_Cauldron();
            }
            InputArrowInventory(PenumbukUI);
        
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.TumbukTime){
            if(gameInput.GetInputEscape()){
                OnQuitPenumbuk?.Invoke(this,EventArgs.Empty);
                // isChestOpen = false;
            }
            if(gameInput.GetInputOpenInventory_ChestOpen()){
                // PenumbukUI.ShowInventory_PenumbukIsOpen();
                WordInput.Instance.UndoInputLetterManyWords();
                OnStopTumbuk?.Invoke(this,EventArgs.Empty); // selanjutnya coba buka penumuk UI, trus bikin ui nya + word manager + progress bar etc etc
            }
        }
        
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

    
    public InventoryScriptableObject GetPlayerInventory(){
        return inventory;
    }
    public int GetInventorySize(){
        return inventorySize;
    }
}
