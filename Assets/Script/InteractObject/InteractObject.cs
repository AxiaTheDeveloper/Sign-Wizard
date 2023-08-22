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
    public void OpenUI_Gift(QuestBox questBox){
        questBox.ShowUI_GiftLetter();
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
    private SoundManager soundManager;

    private bool hasCheckGift = false;
    [SerializeField]private InteractObject interactObject_QuestBox;



    private void Start() {
        soundManager = SoundManager.Instance;
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
        if(type == ObjectType.TheQuestBox)
        {
            questBox = new TheQuestBox();
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
                soundManager.PlayChestOpen();
                chest.OpenUI(Chest);
            }
            
        }
        if(type == ObjectType.ThePenumbuk){
            if(playerSave.GetPlayerLevelMode() == levelMode.outside){
                DialogueManager.Instance.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.sedangTidakAdaQuest_InteractObject);
            }
            else if(playerSave.GetPlayerLevelMode() == levelMode.MakingPotion){
                if(playerSave.GetPlayerLevel() == 1){
                    DialogueManager.Instance.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.tidakBisaPakaiPenumbuk);
                }
                else{
                    penumbuk.OpenUI(Penumbuk);
                }
                
            }
            
        }
        if(type == ObjectType.TheSubmitPotion){
            if(playerSave.GetPlayerLevelMode() == levelMode.MakingPotion){
                if(playerSave.GetPlayerLevel() == 6){
                    DialogueManager.Instance.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.sudahMenyelesaikanSemuaQuest);
                }
                else{
                    soundManager.PlayMailbox();
                    submit.OpenUI(Submit);
                }
                
            }
            else if(playerSave.GetPlayerLevelMode() == levelMode.outside){
                if(playerSave.GetPlayerLevel() > 1 && playerSave.GetPlayerLevel() < playerSave.GetMaxLevel()){
                    if(!hasCheckGift){
                        hasCheckGift = true;
                        soundManager.PlayMailbox();
                        questBox.OpenUI_Gift(QuestBox);
                    }
                    else{
                        playerSave.ChangePlayerMode(levelMode.MakingPotion);
                        soundManager.PlayMailbox();
                        questBox.OpenUI(QuestBox);
                    }
                }
                else{
                    playerSave.ChangePlayerMode(levelMode.MakingPotion);
                    soundManager.PlayMailbox();
                    questBox.OpenUI(QuestBox);
                }
                
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
                        if(playerSave.GetPlayerLevel() > 1 && playerSave.GetPlayerLevel() < playerSave.GetMaxLevel()){
                            if(!interactObject_QuestBox.GetHasCheckGift()){
                                DialogueManager.Instance.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.cekMailboxDulu_InteractObject);
                            }
                            else{
                                DialogueManager.Instance.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.cekQuestDulu_InteractObject);
                            }
                        }
                        else{
                            DialogueManager.Instance.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.cekMailboxDulu_InteractObject);
                        }
                        
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
    public bool GetHasCheckGift(){
        //ini hanya utk door sadja, dikirim dari inter object punya potion,dikirim ke door
        return hasCheckGift;
    }
}
