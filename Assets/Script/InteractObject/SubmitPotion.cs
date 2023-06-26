using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitPotion : MonoBehaviour
{
    [SerializeField]private PlayerInventory playerInventory;
    [SerializeField]private WitchGameManager gameManager;
    public void ShowWholeUI(){

        playerInventory.ShowPlayerInventory();
        gameManager.ChangeInterfaceType(WitchGameManager.InterfaceType.InventoryAndSubmit);
    }
}
