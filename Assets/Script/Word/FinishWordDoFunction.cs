using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// public class AddObject{
//     public void WordFinished(string contohObject){
//         Debug.Log(contohObject);
//     }
// }
// public class AddObjects{
//     public void WordFinished(InventoryScriptableObject inventory, ItemScriptableObject item, int quantity){
//         inventory.AddItemToSlot(item,quantity);
//         // i guess jg manggil ke chest?
//     }
// }
public class FinishWordDoFunction : MonoBehaviour
{
    //buat kirim sinyal aja kalo uda selesai tergantung mo ke siapa
    public event EventHandler OnFinishChestWord;
    public enum Type{
        AddObject, Adds, Chest
    }
    public Type type;


    // [SerializeField]private PlayerInventory playerInventory;
    // [SerializeField]private ItemScriptableObject itemYangNambah;
    // [SerializeField]private int quantity;


    private void Start() {        
       
    }
    public void WordFinisheds(){
        if(type == Type.AddObject){
            Debug.Log("yey");
        }
        if(type == Type.Chest){
            OnFinishChestWord?.Invoke(this,EventArgs.Empty);
            // adds.WordFinished(playerInventory.GetPlayerInventory(), itemYangNambah, quantity);
        }
    }
}