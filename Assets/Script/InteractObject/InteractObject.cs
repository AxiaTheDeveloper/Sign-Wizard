using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IInteractCauldron{
    public void Interact();
}
public interface IInteractChest
{
    public void OpenUI(ExampleChest example);
}
public class Cauldron : IInteractCauldron
{
    public void Interact(){
        Debug.Log("This is Cauldron");
    }
}

public class Kyaaa : IInteractCauldron
{
    public void Interact(){
        Debug.Log("This is Kyaa");
    }
}
public class Chest : IInteractChest
{
    public void OpenUI(ExampleChest example){
        example.ShowUI();
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
        cauldron = new Cauldron();
        throws = new Kyaaa();
        chest = new Chest();
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
