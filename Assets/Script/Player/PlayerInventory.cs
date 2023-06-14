using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInventory : MonoBehaviour
{
    [Header("This is for Player Inventory")]
    public static PlayerInventory Instance;
    [SerializeField]private InventoryUI inventoryUI;
    [SerializeField]private InventoryUI otherInventoryUI; //ini misal buka di chest ato buka di mana gitu dr interactable object, trus dibuat code lg yg ngatur show hide nya, trus pas show dikasih ke sini, pas hide d null
    [SerializeField]private InventoryScriptableObject inventory;

    private int inventorySize;


    [Header("This is for Player Input")]
    [SerializeField]private GameInput gameInput;
    [SerializeField]private WitchGameManager gameManager;
    public event EventHandler OnQuitInventory;// ini buat dikirim ke InventoryUI buat kasih tau suruh hide canvasnya
    private bool isInventoryOpen;

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

        inventory.OnItemUpdate += inventory_OnItemUpdate;
    }

    private void inventory_OnItemUpdate(object sender, InventoryScriptableObject.OnItemUpdateEventArgs e)
    {
        inventoryUI.UpdateVisualInventorySlot(e.position,inventory.inventSlot[e.position]);
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
            if(isInventoryOpen && (gameInput.GetInputEscape() || gameInput.GetInputOpenInventory())){
                // Debug.Log("Hi Close");
                OnQuitInventory?.Invoke(this,EventArgs.Empty);
                isInventoryOpen = false;
            }
            else if(otherInventoryUI){
                // if()
            }
            InputArrowInventory();
        }
    }
    private void InputArrowInventory(){
        keyInputArrowUI = gameInput.GetInputArrow();
        if(keyInputArrowUI.y == 1){
            inventoryUI.SelectItemUp();
        }
        else if(keyInputArrowUI.y == -1){
            inventoryUI.SelectItemDown();
        }
        else if(keyInputArrowUI.x == -1){
            inventoryUI.SelectItemLeft();
        }
        else if(keyInputArrowUI.x == 1){
            inventoryUI.SelectItemRight();
        }
    }

    
    public void GiveData_To_UI(){
        //updet semua
        for(int i=0;i<inventorySize;i++){
            inventoryUI.UpdateVisualInventorySlot(i,inventory.inventSlot[i]);
        }
    }
    public InventoryScriptableObject GetPlayerInventory(){
        return inventory;
    }
    public int GetInventorySize(){
        return inventorySize;
    }
}
