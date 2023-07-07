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

public class TheDoor
{
    public void OpenUI(Door_Outside door){

        door.ShowDialogue();
    }
}
public class TheQuestBox
{
    public void OpenUI(QuestBox questBox){
        questBox.ShowUI();
    }
}

public class InteractObject : MonoBehaviour
{
    
    public enum ObjectType{
        TheCauldron, TheChest, ThePenumbuk, TheSubmitPotion, TheDictionary, TheBed, TheDoor, TheQuestBox
    }
    public ObjectType type;

    private TheCauldron cauldron;
    private ThePenumbuk penumbuk;
    private TheChest chest;
    private TheSubmit submit;
    private TheDictionary dictionary;
    private TheBed bed;
    private TheDoor door;
    private TheQuestBox questBox;

    [SerializeField]private Chest Chest;
    [SerializeField]private Cauldron Cauldron;
    [SerializeField]private Penumbuk Penumbuk;
    [SerializeField]private SubmitPotion Submit;
    [SerializeField]private DictionaryUI Dictionary;
    [SerializeField]private Bed Bed;
    [SerializeField]private Door_Outside Door;
    [SerializeField]private QuestBox QuestBox;
    private PlayerSaveManager playerSave;


    private void Start() {
        playerSave = PlayerSaveManager.Instance;
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
            questBox = new TheQuestBox();
        }
        if(type == ObjectType.TheDictionary){
            dictionary = new TheDictionary();
        }
        if(type == ObjectType.TheBed){
            bed = new TheBed();
        }
        if(type == ObjectType.TheDoor){
            door = new TheDoor();
        }
    }
    public void Interacts(){
        if(type == ObjectType.TheCauldron){
            if(playerSave.GetPlayerLevelMode() == levelMode.outside){
                DialogueManager.Instance.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.sedangTidakAdaQuest_InteractObject);
            }
            else if(playerSave.GetPlayerLevelMode() == levelMode.MakingPotion){
                cauldron.OpenUI(Cauldron);
            }
            
        }
        if(type == ObjectType.TheChest){
            if(playerSave.GetPlayerLevelMode() == levelMode.outside){
                DialogueManager.Instance.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.sedangTidakAdaQuest_InteractObject);
            }
            else if(playerSave.GetPlayerLevelMode() == levelMode.MakingPotion){
                chest.OpenUI(Chest);
            }
            
        }
        if(type == ObjectType.ThePenumbuk){
            if(playerSave.GetPlayerLevelMode() == levelMode.outside){
                DialogueManager.Instance.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.sedangTidakAdaQuest_InteractObject);
            }
            else if(playerSave.GetPlayerLevelMode() == levelMode.MakingPotion){
                penumbuk.OpenUI(Penumbuk);
            }
            
        }
        if(type == ObjectType.TheSubmitPotion){
            if(playerSave.GetPlayerLevelMode() == levelMode.MakingPotion){
                submit.OpenUI(Submit);
            }
            else if(playerSave.GetPlayerLevelMode() == levelMode.outside){
                playerSave.ChangePlayerMode(levelMode.MakingPotion);
                questBox.OpenUI(QuestBox);
            }

        }
        if(type == ObjectType.TheDictionary){
            dictionary.OpenUI(Dictionary);
        }
        if(type == ObjectType.TheBed){
            bed.OpenUI(Bed);
        }
        if(type == ObjectType.TheDoor){
            if(playerSave.GetPlayerLevelMode() == levelMode.outside){
                if(WitchGameManager.Instance.GetPlace() == WitchGameManager.Place.outdoor){
                    if(QuestBox.GetHasCheckFirstTime()){
                        door.OpenUI(Door);
                    }
                    else{
                        DialogueManager.Instance.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.cekQuestDulu_InteractObject);
                    }
                }
                else if(WitchGameManager.Instance.GetPlace() == WitchGameManager.Place.indoor){
                    door.OpenUI(Door);
                }
                
            }
            else if(playerSave.GetPlayerLevelMode() == levelMode.MakingPotion){
                door.OpenUI(Door);
            }
        }

    }
}
