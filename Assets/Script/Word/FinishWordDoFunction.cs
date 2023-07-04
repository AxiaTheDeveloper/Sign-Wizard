using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FinishWordDoFunction : MonoBehaviour
{
    //buat kirim sinyal aja kalo uda selesai tergantung mo ke siapa
    public event EventHandler OnFinishChestWord, OnStopCauldronFire, OnTumbuk;//ke chest, ke cauldron, ke penumbuk
    public enum Type{
        AddObject, Cauldron, Chest, Penumbuk
    }
    public Type type;


    // [SerializeField]private PlayerInventory playerInventory;
    // [SerializeField]private ItemScriptableObject itemYangNambah;
    // [SerializeField]private int quantity;


    public void WordFinisheds(){
        Debug.Log("PAK TOLONG");
        if(type == Type.AddObject){
            Debug.Log("yey");
        }
        else if(type == Type.Chest){
            Debug.Log("HELOOOOO");
            OnFinishChestWord?.Invoke(this,EventArgs.Empty);
            Debug.Log("HELOOOOO AAAAAAAAAAAAAAAAAAA");
            // adds.WordFinished(playerInventory.GetPlayerInventory(), itemYangNambah, quantity);
        }
        else if(type == Type.Cauldron){
            OnStopCauldronFire?.Invoke(this,EventArgs.Empty);
        }
        else if(type == Type.Penumbuk){
            OnTumbuk?.Invoke(this,EventArgs.Empty);
        }
    }
}
