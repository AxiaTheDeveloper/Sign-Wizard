using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;
    [SerializeField]private InventoryUI inventoryUI;
    [SerializeField]private InventoryScriptableObject inventory;

    private int inventorySize;

    public event EventHandler OnUpdateUI;
    

    private void Awake() {
        Instance = this;
        inventorySize = inventory.size;
    }
    private void Start(){
        
        if(inventory.inventSlot.Count != inventorySize){
            inventory.CreateInventory();
        }

        inventory.OnItemAdd += inventory_OnItemAdd;
    }

    private void inventory_OnItemAdd(object sender, InventoryScriptableObject.OnItemAddEventArgs e)
    {
        inventoryUI.UpdateVisualInventorySlot(e.position,inventory.inventSlot[e.position]);
    }

    void Update()
    {
        
        
    }
    public void GiveData_To_UI(){
        //updet semua
        for(int i=0;i<inventorySize;i++){
            inventoryUI.UpdateVisualInventorySlot(i,inventory.inventSlot[i]);
        }
    }
    public int GetInventorySize(){
        return inventorySize;
    }
}
