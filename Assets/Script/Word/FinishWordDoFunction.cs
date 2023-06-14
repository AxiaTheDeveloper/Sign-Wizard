using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObject{
    public void WordFinished(string contohObject){
        Debug.Log(contohObject);
    }
}
public class AddObjects{
    public void WordFinished(InventoryScriptableObject inventory, ItemScriptableObject item, int quantity){
        inventory.AddItemToSlot(item,quantity);
    }
}
public class FinishWordDoFunction : MonoBehaviour
{
    public enum FunctionType{
        AddObject, Adds
    }
    public FunctionType type;

    private AddObject add;
    private AddObjects adds;

    [SerializeField]private PlayerInventory playerInventory;
    [SerializeField]private ItemScriptableObject itemYangNambah;
    [SerializeField]private int quantity;


    private void Start() {
        if(type == FunctionType.AddObject){
            add = new AddObject();
        }
        else if(type == FunctionType.Adds){
             adds = new AddObjects();
        }
        
       
    }
    public void WordFinisheds(){
        if(type == FunctionType.AddObject){
            add.WordFinished("blaba");
        }
        if(type == FunctionType.Adds){
            adds.WordFinished(playerInventory.GetPlayerInventory(), itemYangNambah, quantity);
        }
    }
}
