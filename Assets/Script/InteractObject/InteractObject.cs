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
        TheCauldron, TheChest, ThePenumbuk, TheSubmitPotion, TheDictionary, TheBed, TheDoor, TheQuestBox, TheGraveyard_DialogueOnly, BrokenBridgeGraveSign_DialogueOnly, TruckBridgeGraveyard_DialogueOnly, CementBridgeGraveyard_DialogueOnly, TruckMerchant_DialogueOnly, TebingInFrontOfHouse_DialogueOnly, PangganganInFrontOfHouse_DialogueOnly, FlowersInFrontOfHouse_DialogueOnly, FountainTown_DialogueOnly, TruckTown_DialogueOnly, SignMerchantTown_DialogueOnly, CauldronMerchantTown_DialogueOnly, TongSampahKecilTown_DialogueOnly, TongSampahBesarTown_DialogueOnly, BicycleTown_DialogueOnly, CafeTable_DialogueOnly, TableFox_DialogueOnly, EmberFox_DialogueOnly, TongSampahBesarBelakang_DialogueOnly
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
    private DialogueManager dialogueManager;

    private bool hasCheckGift = false;
    [SerializeField]private InteractObject interactObject_QuestBox;
    [SerializeField]private TravelingMerchant travelingMerchant;

    [Header("INI KHUSUS INTERACT YANG GA PENTING AJA YA")]
    [SerializeField]private bool firsTimeInteractOnly = false;

    private void Start() {
        soundManager = SoundManager.Instance;
        playerSave = PlayerSaveManager.Instance;
        dialogueManager = DialogueManager.Instance;
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
                dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.sedangTidakAdaQuest_InteractObject);
            }
            else if(playerSave.GetPlayerLevelMode() == levelMode.MakingPotion){
                cauldron.OpenUI(Cauldron);
            }
            
        }
        if(type == ObjectType.TheChest){
            if(playerSave.GetPlayerLevelMode() == levelMode.outside){
                dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.sedangTidakAdaQuest_InteractObject);
            }
            else if(playerSave.GetPlayerLevelMode() == levelMode.MakingPotion){
                soundManager.PlayChestOpen();
                chest.OpenUI(Chest);
            }
            
        }
        if(type == ObjectType.ThePenumbuk){
            if(playerSave.GetPlayerLevelMode() == levelMode.outside){
                dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.sedangTidakAdaQuest_InteractObject);
            }
            else if(playerSave.GetPlayerLevelMode() == levelMode.MakingPotion){
                if(playerSave.GetPlayerLevel() == 1){
                    dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.tidakBisaPakaiPenumbuk_InteractObject);
                }
                else{
                    penumbuk.OpenUI(Penumbuk);
                }
                
            }
        }
        if(type == ObjectType.TheSubmitPotion){
            if(playerSave.GetPlayerLevelMode() == levelMode.MakingPotion){
                if(QuestManager.Instance.GetSendername() == Submit.GetCharHouseName())
                {
                    soundManager.PlayMailbox();
                    submit.OpenUI(Submit);
                }
                else{
                    if(!Submit.IsCharacterATravelingMerchant())
                    {
                        dialogueManager.ShowDialogue_WrongChoice_WithBahan(DialogueManager.DialogueWrongChoice.tidakAdaBarangYangDiminta_InteractObject, Submit.GetCharHouseName());
                    }
                    else{
                        //aku tidak memesan potion lalalalala, bikin dialogue baru
                        if(playerSave.GetPlayerLevel() == playerSave.GetMaxLevel())
                        {
                            travelingMerchant.ChatWithMerchant();
                        }
                        else
                        {
                            dialogueManager.ShowDialogue_WrongChoice_WithBahan(DialogueManager.DialogueWrongChoice.tidakAdaBarangYangDiminta_InteractObject, Submit.GetCharHouseName());
                        }
                        
                    }
                }
                
                
            }
            else if(playerSave.GetPlayerLevelMode() == levelMode.outside || playerSave.GetPlayerLevelMode() == levelMode.finishQuest){
                if(playerSave.GetPlayerLevel() == 1 && playerSave.GetPlayerLevelMode() == levelMode.outside)
                {
                    dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.belumMengecekKotakSuratLevel1_InteractObject);
                }
                else{
                    if(Submit.IsCharacterATravelingMerchant())
                    {
                        travelingMerchant.ChatWithMerchant();
                    }
                    else
                    {
                        dialogueManager.ShowDialogue_WrongChoice_WithBahan(DialogueManager.DialogueWrongChoice.tidakAdaBarangYangDiminta_InteractObject, Submit.GetCharHouseName());
                    }
                    
                }
                
            }

        }
        if(type == ObjectType.TheQuestBox)
        {
            if(playerSave.GetPlayerLevelMode() == levelMode.MakingPotion)
            {
                if(playerSave.GetPlayerLevel() == 6){
                    dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.sudahMenyelesaikanSemuaQuest_InteractObject);
                }
                else{
                    dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.SelesaikanQuestSekarang_InteractObject);
                }
                
            }
            else if(playerSave.GetPlayerLevelMode() == levelMode.finishQuest)
            {
                if(playerSave.GetPlayerLevel() == 6){
                    dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.sudahMenyelesaikanSemuaQuest_InteractObject);
                }
                else{
                    dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.belumAdaQuestYangDikirimTidur_InteractObject);
                }
                
            }
            else if(playerSave.GetPlayerLevelMode() == levelMode.outside)
            {
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
                                dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.cekMailboxDulu_InteractObject);
                            }
                            else{
                                dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.cekQuestDulu_InteractObject);
                            }
                        }
                        else{
                            dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.cekMailboxDulu_InteractObject);
                        }
                        
                    }
                }
                else if(WitchGameManager.Instance.GetPlace() == WitchGameManager.Place.indoor){
                    door.OpenUI(Door);
                }
                
            }
            else if(playerSave.GetPlayerLevelMode() == levelMode.MakingPotion || playerSave.GetPlayerLevelMode() == levelMode.finishQuest){
                door.OpenUI(Door);
            }
        }
        if(type == ObjectType.TheGraveyard_DialogueOnly)
        {
            dialogueManager.ShowDialogue_GerbangKuburan();
        }
        if(type == ObjectType.BrokenBridgeGraveSign_DialogueOnly)
        {
            dialogueManager.dShowDialogue_ReadBrokenBridgeGraveyardSigns();
        }
        if(type == ObjectType.TruckBridgeGraveyard_DialogueOnly)
        {
            dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.TruckBridgeGraveyard_InteractObject);
        }
        if(type == ObjectType.CementBridgeGraveyard_DialogueOnly)
        {
            dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.CementBridgeGraveyard_InteractObject);
        }
        if(type == ObjectType.TebingInFrontOfHouse_DialogueOnly)
        {
            dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.TebingInFrontOfHouse_InteractObject);
        }
        if(type == ObjectType.PangganganInFrontOfHouse_DialogueOnly)
        {
            dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.PangganganInFrontOfHouse_InteractObject);
        }
        if(type == ObjectType.FlowersInFrontOfHouse_DialogueOnly)
        {
            dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.FlowersInFrontOfHouse_InteractObject);
        }
        if(type == ObjectType.FountainTown_DialogueOnly)
        {
            dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.FountainTown_InteractObject);
        }
        if(type == ObjectType.TruckTown_DialogueOnly)
        {
            dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.TruckTown_InteractObject);
        }
        if(type == ObjectType.SignMerchantTown_DialogueOnly)
        {
            dialogueManager.ShowDialogue_ViiMerchantSign();
        }
        if(type == ObjectType.CauldronMerchantTown_DialogueOnly)
        {
            dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.CauldronMerchantTown_InteractObject);
        }
        if(type == ObjectType.TongSampahKecilTown_DialogueOnly)
        {
            dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.TongSampahKecilTown_InteractObject);
        }
        if(type == ObjectType.TongSampahBesarTown_DialogueOnly)
        {
            dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.TongSampahBesarTown_InteractObject);
        }
        if(type == ObjectType.BicycleTown_DialogueOnly)
        {
            dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.BicycleTown_InteractObject);
        }
        if(type == ObjectType.CafeTable_DialogueOnly)
        {
            dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.CafeTable_InteractObject);
        }
        if(type == ObjectType.TableFox_DialogueOnly)
        {
            dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.TableFox_InteractObject);
        }
        if(type == ObjectType.EmberFox_DialogueOnly)
        {
            dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.EmberFox_InteractObject);
        }
        if(type == ObjectType.TongSampahBesarBelakang_DialogueOnly)
        {
            dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.TongSampahBesarBelakang_InteractObject);
        }
    }
    public bool GetHasCheckGift(){
        //ini hanya utk door sadja, dikirim dari inter object punya potion,dikirim ke door
        return hasCheckGift;
    }
    public bool FirsTimeInteractOnly()
    {
        return firsTimeInteractOnly;
    }
}
