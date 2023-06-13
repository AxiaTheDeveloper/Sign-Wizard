using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryInput : MonoBehaviour
{
    public static InventoryInput Instance;
    [SerializeField]private GameInput gameInput;
    [SerializeField]private WitchGameManager gameManager;
    [SerializeField]private InventoryUI inventoryUI;
    public event EventHandler OnQuitInventory;
    private bool isInventoryOpen;

    private Vector2 keyInputArrowUI;
    
    //Input pas inventory lg nyala
    private void Awake() {
        Instance = this;

    }
    private void Start() {
        isInventoryOpen = false;
    }
    private void Update()
    {
        //buka invent
        if(gameManager.IsInGame()){
            
            if(gameInput.GetInputOpenInventory() && !isInventoryOpen){
                // Debug.Log("Hi Open");
                inventoryUI.ShowUI();
                isInventoryOpen = true;
            }
        }
        else if(gameManager.IsInterfaceType() == 3){
            if(isInventoryOpen && (gameInput.GetInputEscape() || gameInput.GetInputOpenInventory())){
                // Debug.Log("Hi Close");
                OnQuitInventory?.Invoke(this,EventArgs.Empty);
                isInventoryOpen = false;
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
}
