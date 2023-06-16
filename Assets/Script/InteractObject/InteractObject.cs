using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ini buat hubungin interact object dengan cauldron ato chest ato dll gitu
public class Cauldron
{
    public void Interact(){
        Debug.Log("This is Cauldron");
    }
}

public class Kyaaa
{
    public void Interact(){
        Debug.Log("This is Kyaa");
    }
}
public class TheChest
{
    public void OpenUI(Chest chest){
        chest.ShowWholeUI();
    }
}

public class InteractObject : MonoBehaviour
{
    
    public enum ObjectType{
        Cauldron, TheChest, Kyaa
    }
    public ObjectType type;
    public int x;

    private Cauldron cauldron;
    private Kyaaa throws;
    private TheChest chest;

    [SerializeField]private Chest Chest;


    private void Start() {
        
        
        
        if(type == ObjectType.Cauldron){
            cauldron = new Cauldron();
        }
        if(type == ObjectType.TheChest){
            chest = new TheChest();
        }
        if(type == ObjectType.Kyaa){
            throws = new Kyaaa();
        }
    }
    public void Interacts(){
        if(type == ObjectType.Cauldron){
            cauldron.Interact();
        }
        if(type == ObjectType.TheChest){
            chest.OpenUI(Chest);
        }
        if(type == ObjectType.Kyaa){
            throws.Interact();
        }
    }
}
