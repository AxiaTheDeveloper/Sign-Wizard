using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;
    [SerializeField]private InventoryUI inventoryUI;
    public event EventHandler OnQuitInventory;
    private bool isInventoryOpen;

    [SerializeField]private int inventorySize;


    private void Awake() {
        Instance = this;
    }
    private void Start() {
        isInventoryOpen = false;

    }
    void Update()
    {
        //buka invent
        if(WitchGameManager.Instance.IsInGame()){
            
            if(GameInput.Instance.GetInputOpenInventory() && !isInventoryOpen){
                // Debug.Log("Hi Open");
                inventoryUI.ShowUI();
                isInventoryOpen = true;
            }
        }
        else if(WitchGameManager.Instance.IsInterfaceType() == 3){
            if(isInventoryOpen && (GameInput.Instance.GetInputEscape() || GameInput.Instance.GetInputOpenInventory())){
                // Debug.Log("Hi Close");
                OnQuitInventory?.Invoke(this,EventArgs.Empty);
                isInventoryOpen = false;
            }
        }
        
    }
    public int GetInventorySize(){
        return inventorySize;
    }
}
