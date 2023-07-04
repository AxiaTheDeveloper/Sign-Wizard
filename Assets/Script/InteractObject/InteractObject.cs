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

public class TheSubmit
{
    public void OpenUI(SubmitPotion submit){
        submit.ShowWholeUI();
    }
}
public class TheDictionary
{
    public void OpenUI(DictionaryUI dictionary){
        dictionary.ShowUI();
    }
}
public class TheBed
{
    public void OpenUI(Bed bed){
        bed.ShowDialogue();
    }
}

public class InteractObject : MonoBehaviour
{
    
    public enum ObjectType{
        TheCauldron, TheChest, ThePenumbuk, TheSubmitPotion, TheDictionary, TheBed
    }
    public ObjectType type;

    private TheCauldron cauldron;
    private ThePenumbuk penumbuk;
    private TheChest chest;
    private TheSubmit submit;
    private TheDictionary dictionary;
    private TheBed bed;

    [SerializeField]private Chest Chest;
    [SerializeField]private Cauldron Cauldron;
    [SerializeField]private Penumbuk Penumbuk;
    [SerializeField]private SubmitPotion Submit;
    [SerializeField]private DictionaryUI Dictionary;
    [SerializeField]private Bed Bed;


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
        if(type == ObjectType.TheSubmitPotion){
            submit = new TheSubmit();
        }
        if(type == ObjectType.TheDictionary){
            dictionary = new TheDictionary();
        }
        if(type == ObjectType.TheBed){
            bed = new TheBed();
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
        if(type == ObjectType.TheSubmitPotion){
            submit.OpenUI(Submit);
        }
        if(type == ObjectType.TheDictionary){
            dictionary.OpenUI(Dictionary);
        }
        if(type == ObjectType.TheBed){
            bed.OpenUI(Bed);
        }
    }
}
