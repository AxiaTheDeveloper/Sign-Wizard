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
    [SerializeField]private InventoryUI ChestInventoryUI, CauldronUI; //ini misal buka di chest ato buka di mana gitu dr interactable object, trus dibuat code lg yg ngatur show hide nya, trus pas show dikasih ke sini, pas hide d null
    [SerializeField]private InventoryScriptableObject inventory;

    private int inventorySize;


    [Header("This is for Player Input")]
    [SerializeField]private GameInput gameInput;
    [SerializeField]private WitchGameManager gameManager;
    public event EventHandler OnQuitInventory, OnQuitChest, OnClearPlayerInventory, OnQuitCauldron; //OnQuitInventory nyambung ke InventoryUI, OnQuitChest ke function ExampleChest
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
        else if(gameManager.IsInterfaceType() == 3){
            if(isInventoryOpen){
                if(isChestOpen){
                    if(gameInput.GetInputEscape() || gameInput.GetInputOpenInventory_ChestOpen()){
                        OnQuitInventory?.Invoke(this,EventArgs.Empty);
                        isInventoryOpen = false;
                        gameManager.ChangeInterfaceType(4);
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
        else if(gameManager.IsInterfaceType() == 4){
            if(gameInput.GetInputEscape()){
                OnQuitCauldron?.Invoke(this,EventArgs.Empty);
                // isChestOpen = false;
            }
            else if(gameInput.GetInputSelectItemForCauldron()){
                CauldronUI.SelectItem_Cauldron();
            }
            InputArrowInventory(CauldronUI);
        }
        else if(gameManager.IsInterfaceType() == 5){
            isChestOpen = true;
            if(gameInput.GetInputEscape()){
                OnQuitChest?.Invoke(this,EventArgs.Empty);
                isChestOpen = false;
            }
            InputArrowInventory(ChestInventoryUI);
            if(gameInput.GetInputGetKeyTabDown()){
                gameManager.ChangeInterfaceType(5);
            }
            if(gameInput.GetInputOpenInventory_ChestOpen() && !isInventoryOpen){
                inventoryUI.ShowInventoryUI();
                isInventoryOpen = true;
                WordInput.Instance.UndoInputLetterManyWords();
                
            }
        }
        else if(gameManager.IsInterfaceType() == 6){
            InputArrowInventory_AddQuantity(ChestInventoryUI);
            if(gameInput.GetInputGetKeyTabDown()){
                gameManager.ChangeInterfaceType(4);
            }
            if(gameInput.GetInputEscape()){
                OnQuitChest?.Invoke(this,EventArgs.Empty);
                isChestOpen = false;
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
