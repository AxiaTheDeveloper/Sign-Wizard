using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]private InventoryItemUI inventItemUI; 
    [SerializeField]private RectTransform contentPanel;

    private List<InventoryItemUI> UI_ItemList;
    [SerializeField]private WitchGameManager gameManager;

    
    private void Awake() {

    }
    private void Start() {
        UI_ItemList = new List<InventoryItemUI>();
        PlayerInventory.Instance.OnQuitInventory += playerInventory_OnQuitInventory;
        CreateInventoryUI(PlayerInventory.Instance.GetInventorySize());
        gameObject.SetActive(false);
    }

    private void playerInventory_OnQuitInventory(object sender, EventArgs e)
    {
        HideUI();
    }

    public void CreateInventoryUI(int size){
        for(int i=0;  i < size;i++){
            InventoryItemUI UI_Item = Instantiate(inventItemUI, contentPanel);
            UI_ItemList.Add(UI_Item);
        }
    }
    public void ShowUI(){
        gameObject.SetActive(true);
        gameManager.ChangeInterfaceType(2);
    }
    public void HideUI(){
        gameObject.SetActive(false);
        gameManager.ChangeToInGame();
    }
}
