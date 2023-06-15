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
public class Chest
{
    public void OpenUI(ExampleChest example){
        example.ShowWholeUI();
    }
}

public class InteractObject : MonoBehaviour
{
    
    public enum ObjectType{
        Cauldron, Chest, Kyaa
    }
    public ObjectType type;
    public int x;

    private Cauldron cauldron;
    private Kyaaa throws;
    private Chest chest;

    [SerializeField]private ExampleChest example;


    private void Start() {
        
        
        
        if(type == ObjectType.Cauldron){
            cauldron = new Cauldron();
        }
        if(type == ObjectType.Chest){
            chest = new Chest();
        }
        if(type == ObjectType.Kyaa){
            throws = new Kyaaa();
        }
    }
    public void Interacts(){
        if(type == ObjectType.Cauldron){
            cauldron.Interact();
        }
        if(type == ObjectType.Chest){
            chest.OpenUI(example);
        }
        if(type == ObjectType.Kyaa){
            throws.Interact();
        }
    }
}
