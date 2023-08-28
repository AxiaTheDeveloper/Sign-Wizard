using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Destination_Outside
{
    forest, graveyard, magicalBridge, brokenBridge_Graveyard, brokenBridge_Town, town, inFrontOfHouse
}
public class GoingToOtherPlace : MonoBehaviour
{
    private WitchGameManager gameManager;
    private PlayerSaveManager playerSave;

    [SerializeField]private Destination_Outside destination;
    [SerializeField]private CanvasGroup darkBG_effect;    
    private void Start() 
    {
        gameManager = WitchGameManager.Instance;
        playerSave = PlayerSaveManager.Instance;
    }
    public void FadeGetOutScene()
    {
        gameManager.ChangeToCinematic();
        darkBG_effect.LeanAlpha(1f, 0.3f).setOnComplete(
            () => GoToDestination()
        );
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(gameManager.GetOutDoorType() == WitchGameManager.OutDoorType.inFrontOfHouse && playerSave.GetPlayerLevel() == 1 && playerSave.GetPlayerLevelMode() == levelMode.outside)
            {
                DialogueManager.Instance.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.belumMengecekKotakSuratLevel1_InteractObject);
            }
            else if(gameManager.GetOutDoorType() == WitchGameManager.OutDoorType.forest && destination == Destination_Outside.magicalBridge)
            {
                if(playerSave.GetPlayerLevelMode() != levelMode.MakingPotion)
                {
                    DialogueManager.Instance.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.tidakPerluKeKota_GoingToOtherPlace);
                }
                else if(playerSave.GetPlayerLevelMode() == levelMode.MakingPotion){
                    if(!QuestManager.Instance.CheckPotion_BeforeGoToForest(PlayerInventory.Instance.GetPlayerInventory()))
                    {
                        DialogueManager.Instance.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.potionYangDibawaTidakSesuai_GoingToOtherPlace);
                    }
                }
            }
            else if(gameManager.GetOutDoorType() == WitchGameManager.OutDoorType.town && destination == Destination_Outside.magicalBridge)
            {
                if(playerSave.GetPlayerLevelMode() == levelMode.MakingPotion)
                {
                    DialogueManager.Instance.ShowDialogue_WrongChoice_WithBahan(DialogueManager.DialogueWrongChoice.belumMengantarkanPotionKeRumahPemesan_GoingToOtherPlace, QuestManager.Instance.GetSendername());
                }
                else if(playerSave.GetPlayerLevelMode() == levelMode.finishQuest)
                {
                    destination = Destination_Outside.inFrontOfHouse;
                    FadeGetOutScene();
                }

            }
            else
            {
                FadeGetOutScene();
            }
        }
    }
    private void GoToDestination()
    {
        if(destination == Destination_Outside.forest)
        {
            if(PlayerPrefs.GetString("pilihanIDEN") == "ID"){
                SceneManager.LoadSceneAsync("OutDoor_Forest_ID");
            }
            else{
                SceneManager.LoadSceneAsync("OutDoor_Forest_EN");
            }
            
        }
        else if(destination == Destination_Outside.graveyard)
        {
            if(PlayerPrefs.GetString("pilihanIDEN") == "ID"){
                SceneManager.LoadSceneAsync("OutDoor_Graveyard_ID");
            }
            else{
                SceneManager.LoadSceneAsync("OutDoor_Graveyard_EN");
            }
        }
        else if(destination == Destination_Outside.magicalBridge)
        {
            if(PlayerPrefs.GetString("pilihanIDEN") == "ID"){
                SceneManager.LoadSceneAsync("OutDoor_MagicalBridge_ID");
            }
            else{
                SceneManager.LoadSceneAsync("OutDoor_MagicalBridge_EN");
            }
        }
        else if(destination == Destination_Outside.brokenBridge_Graveyard)
        {
            if(PlayerPrefs.GetString("pilihanIDEN") == "ID"){
                SceneManager.LoadSceneAsync("OutDoor_BrokenBridgeGraveyard_ID");
            }
            else{
                SceneManager.LoadSceneAsync("OutDoor_BrokenBridgeGraveyard_EN");
            }
        }
        else if(destination == Destination_Outside.town)
        {
            if(PlayerPrefs.GetString("pilihanIDEN") == "ID"){
                SceneManager.LoadSceneAsync("OutDoor_Town_ID");
            }
            else{
                SceneManager.LoadSceneAsync("OutDoor_Town_EN");
            }
        }
        else if(destination == Destination_Outside.brokenBridge_Town)
        {
            if(PlayerPrefs.GetString("pilihanIDEN") == "ID"){
                SceneManager.LoadSceneAsync("OutDoor_BrokenBridgeTown_ID");
            }
            else{
                SceneManager.LoadSceneAsync("OutDoor_BrokenBridgeTown_EN");
            }
        }
        else if(destination == Destination_Outside.inFrontOfHouse)
        {
            if(PlayerPrefs.GetString("pilihanIDEN") == "ID"){
                SceneManager.LoadSceneAsync("OutDoor_InFrontHouse_ID");
            }
            else{
                SceneManager.LoadSceneAsync("OutDoor_InFrontHouse_EN");
            }
        }
    }
}
