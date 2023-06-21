using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ini buat hubungin interact object dengan cauldron ato chest ato dll gitu
public class TheCauldron
{
    public void OpenUI(Cauldron cauldron){
        cauldron.ShowWholeUI();
    }
}

public class ThePenumbuk
{
    public void OpenUI(Penumbuk penumbuk){
        penumbuk.ShowWholeUI();
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
        TheCauldron, TheChest, ThePenumbuk
    }
    public ObjectType type;

    private TheCauldron cauldron;
    private ThePenumbuk penumbuk;
    private TheChest chest;

    [SerializeField]private Chest Chest;
    [SerializeField]private Cauldron Cauldron;
    [SerializeField]private Penumbuk Penumbuk;


    private void Start() {
        
        
        
        if(type == ObjectType.TheCauldron){
            cauldron = new TheCauldron();
        }
        if(type == ObjectType.TheChest){
            chest = new TheChest();
        }
        if(type == ObjectType.ThePenumbuk){
            penumbuk = new ThePenumbuk();
        }
    }
    public void Interacts(){
        if(type == ObjectType.TheCauldron){
            cauldron.OpenUI(Cauldron);
        }
        if(type == ObjectType.TheChest){
            chest.OpenUI(Chest);
        }
        if(type == ObjectType.ThePenumbuk){
            penumbuk.OpenUI(Penumbuk);
        }
    }
}
