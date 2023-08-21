using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControlUI : MonoBehaviour
{
    [SerializeField]private Sprite AnyChoose, AnyChooseHorizontal, AnyChooseVertical, Cauldron, CauldronFire, ChestInvent, ChestQuantity, Chest, CloseLetter, Dictionary, InGame, MortarInProgress, MortarSelectItem, submitPotion, tutorial;
    [SerializeField]private Image image;
    [SerializeField]private WitchGameManager gameManager;
    private void Start() {
        gameManager.OnChangeToCinematic += gameManager_OnChangeToCinematic;
        gameManager.OnChangeToInGame += gameManager_OnChangeToInGame;
        gameManager.OnChangeToInterface += gameManager_OnChangeToInterface;
        gameManager.OnChangeToPause += gameManager_OnChangeToPause;
    }


// InventoryTime, InventoryAndCauldron, CauldronFire, InventoryAndChest, QuantityTime, none, InventoryAndPenumbuk, TumbukTime, InventoryAndSubmit, SubmitPotion, DictionaryTime, InterfaceBed, InterfaceDoor, InterfaceQuestBox
    private void gameManager_OnChangeToInterface(object sender, EventArgs e)
    {
        if(!image.gameObject.activeSelf){
            image.gameObject.SetActive(true);
        }
        if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.InventoryTime){
            if(PlayerInventory.Instance.GetIsChestOpen()){
                image.sprite = ChestInvent;
            }
            else{
                image.sprite = Dictionary;
            }
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.InventoryAndCauldron){
            image.sprite = Cauldron;
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.CauldronFire){
            image.sprite = CauldronFire;
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.InventoryAndChest){
            image.sprite = Chest;
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.QuantityTime){
            image.sprite = ChestQuantity;
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.InventoryAndPenumbuk){
            image.sprite = MortarSelectItem;
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.TumbukTime){
            image.sprite = MortarInProgress;
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.InventoryAndSubmit){
            image.sprite = MortarSelectItem;
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.InterfaceBed || gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.InterfaceDoor || gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.InterfaceYesNoTutorial){
            image.sprite = AnyChooseHorizontal;
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.SubmitPotion){
            image.sprite = submitPotion;
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.DictionaryTime){
            image.sprite = Dictionary;
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.InterfaceQuestBox){
            image.sprite = CloseLetter;
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.InterfaceTutorial){
            image.sprite = tutorial;
        }
    }

    private void gameManager_OnChangeToInGame(object sender, EventArgs e)
    {
        if(!image.gameObject.activeSelf){
            image.gameObject.SetActive(true);
        }
        image.sprite = InGame;
    }

    private void gameManager_OnChangeToCinematic(object sender, EventArgs e)
    {
        image.gameObject.SetActive(false);
    }
    private void gameManager_OnChangeToPause(object sender, EventArgs e)
    {
        if(!image.gameObject.activeSelf){
            image.gameObject.SetActive(true);
        }
        image.sprite = MortarSelectItem;
    }
}
