using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//buat the whole inventory

public class InventoryOnly{
    private bool keepMoveRight,keepMoveLeft;
    public int SelectItemRight(int totalRow, int totalColumn, int selectItem, List<InventoryItemUI> UI_ItemList){
        Debug.Log("Hi bich");
        keepMoveRight = true;
        int selectItemNow = 0;// ini buat tau dia ada di row mana sih
        int selectItemHere = selectItem;
        for(int i=0;i<totalRow;i++){
            selectItemNow = i * totalColumn;
            if(selectItemHere == (totalColumn - 1 + selectItemNow)){
                keepMoveRight = false;
                break;
            }
        }
        if(keepMoveRight){
            UI_ItemList[selectItemHere].DeselectItem();
            selectItemHere++;
            UI_ItemList[selectItemHere].SelectItem();
        }
        else{
            UI_ItemList[selectItemHere].DeselectItem();
            selectItemHere = 0 + selectItemNow;
            UI_ItemList[selectItemHere].SelectItem();
        }
        // UpdateVisual_InventDescription();
        // inventUIDesc.SetItemDataDesc(); diisii~~~
        return selectItemHere; // kasih ke main class
    }
    public int SelectItemLeft(int totalRow, int totalColumn, int selectItem, List<InventoryItemUI> UI_ItemList){
        keepMoveLeft = true;
        int selectItemNow = 0;// ini buat tau dia ada di row mana sih
        int selectItemHere = selectItem;
        for(int i=0;i<totalRow;i++){
            selectItemNow = i * totalColumn;
            if(selectItemHere == (0 + selectItemNow)){
                keepMoveLeft = false;
                break;
            }
        }
        if(keepMoveLeft){
            UI_ItemList[selectItemHere].DeselectItem();
            selectItemHere--;
            UI_ItemList[selectItemHere].SelectItem();
        }
        else{
            UI_ItemList[selectItemHere].DeselectItem();
            selectItemHere = totalColumn-1 + selectItemNow;
            UI_ItemList[selectItemHere].SelectItem();
        }
        // UpdateVisual_InventDescription();
        
        // inventUIDesc.SetItemDataDesc(); diisii~~~
        return selectItemHere;
    }
    public int SelectItemDown(int totalRow, int totalColumn, int selectItem, List<InventoryItemUI> UI_ItemList, int inventorySize){
        int selectItemHere = selectItem;
        if(selectItemHere + totalColumn <= inventorySize - 1){
            UI_ItemList[selectItemHere].DeselectItem();
            selectItemHere += totalColumn;
            UI_ItemList[selectItemHere].SelectItem();
        }
        else if(selectItemHere + totalColumn > inventorySize - 1){
            UI_ItemList[selectItemHere].DeselectItem();
            selectItemHere -= (totalColumn * (totalRow-1));
            UI_ItemList[selectItemHere].SelectItem();
        }
        // UpdateVisual_InventDescription();
        
        // inventUIDesc.SetItemDataDesc(); diisii~~~
        return selectItemHere;
    }
    public int SelectItemUp(int totalRow, int totalColumn, int selectItem, List<InventoryItemUI> UI_ItemList, int inventorySize){
        // Debug.Log(selectItem - totalColumn);
        int selectItemHere = selectItem;
        if(selectItemHere - totalColumn >= 0){
            UI_ItemList[selectItemHere].DeselectItem();
            selectItemHere -= totalColumn;
            UI_ItemList[selectItemHere].SelectItem();
        }
        else if(selectItemHere - totalColumn < 0){
            UI_ItemList[selectItemHere].DeselectItem();
            selectItemHere += (totalColumn * (totalRow-1));
            UI_ItemList[selectItemHere].SelectItem();
        }
        // UpdateVisual_InventDescription();
        // inventUIDesc.SetItemDataDesc(); diisii~~~
        return selectItemHere;
    }
    public void ShowInventoryUI(int pilihanInterface, GameObject UI){
        WitchGameManager.Instance.ChangeInterfaceType(pilihanInterface);

        UI.SetActive(true);
        // inventUIDesc.EmptyDescUI();
        // UpdateVisual_InventDescription();
    }
    public void HideInventoryUI(GameObject UI){
        WitchGameManager.Instance.ChangeToInGame();

        UI.SetActive(false);
        
        
    }
}
public class InventoryWithDesc : InventoryOnly{
    public int SelectItemRight_Desc(int totalRow, int totalColumn, int selectItem, List<InventoryItemUI> UI_ItemList, InventoryUIDesc inventUIDesc)
    {
        Debug.Log("Hi biches");
        int selectItemHere = SelectItemRight(totalRow, totalColumn, selectItem, UI_ItemList);
        UpdateVisual_InventDescription(UI_ItemList, selectItemHere, inventUIDesc);
        return selectItemHere;
    }
    public int SelectItemLeft_Desc(int totalRow, int totalColumn, int selectItem, List<InventoryItemUI> UI_ItemList, InventoryUIDesc inventUIDesc){
        int selectItemHere = SelectItemLeft(totalRow, totalColumn, selectItem, UI_ItemList);
        UpdateVisual_InventDescription(UI_ItemList, selectItemHere, inventUIDesc);
        return selectItemHere;
    }
    public int SelectItemDown_Desc(int totalRow, int totalColumn, int selectItem, List<InventoryItemUI> UI_ItemList, int inventorySize, InventoryUIDesc inventUIDesc){
        int selectItemHere = SelectItemDown(totalRow, totalColumn, selectItem, UI_ItemList, inventorySize);
        UpdateVisual_InventDescription(UI_ItemList, selectItemHere, inventUIDesc);
        return selectItemHere;
    }
    public int SelectItemUp_Desc(int totalRow, int totalColumn, int selectItem, List<InventoryItemUI> UI_ItemList, int inventorySize, InventoryUIDesc inventUIDesc){
        int selectItemHere = SelectItemUp(totalRow, totalColumn, selectItem, UI_ItemList, inventorySize);
        UpdateVisual_InventDescription(UI_ItemList, selectItemHere, inventUIDesc);
        return selectItemHere;
    }
    
    public void ShowInventoryUI_Desc(int pilihanInterface, GameObject UI, InventoryUIDesc inventUIDesc, List<InventoryItemUI> UI_ItemList, int selectItem){
        ShowInventoryUI(pilihanInterface, UI);
        inventUIDesc.EmptyDescUI();
        UpdateVisual_InventDescription(UI_ItemList, selectItem, inventUIDesc);
    }

    public void UpdateVisual_InventDescription(List<InventoryItemUI> UI_ItemList, int selectItem, InventoryUIDesc inventUIDesc){
        if(UI_ItemList[selectItem].IsEmpty()){
            
            inventUIDesc.EmptyDescUI();
        }
        else{
            ItemScriptableObject item = UI_ItemList[selectItem].GetItemData();
            inventUIDesc.SetItemDataDesc(item.itemSprite,item.name,item.Desc);
        }
    }
}

public class InventoryUI : MonoBehaviour
{
    [SerializeField]private InventoryItemUI inventItemUI; 
    [SerializeField]private InventoryUIDesc inventUIDesc; 
    [SerializeField]private RectTransform contentPanel;
    [SerializeField]private PlayerInventory playerInventory;

    private List<InventoryItemUI> UI_ItemList;
    [SerializeField]private WitchGameManager gameManager;
    private int  selectItem, inventorySize;
    [SerializeField]private int totalRow, totalColumn;//kolom kanan, row bwh

    private enum TipeInventory{
        inventoryOnly, inventoryWithDesc
    }

    [SerializeField]private TipeInventory tipeInventory;
    private InventoryOnly inventOnly;
    private InventoryWithDesc invent_Desc;


    
    private void Awake() {
        inventUIDesc.EmptyDescUI();
    }
    private void Start() {
        if(tipeInventory == TipeInventory.inventoryOnly){
            inventOnly = new InventoryOnly();
        }
        else if(tipeInventory == TipeInventory.inventoryWithDesc){
            invent_Desc = new InventoryWithDesc();
        }
        UI_ItemList = new List<InventoryItemUI>();
        playerInventory.OnQuitInventory += inventoryInput_OnQuitInventory;
        inventorySize = playerInventory.GetInventorySize();
        selectItem = 0;
        CreateInventoryUI(inventorySize);
        playerInventory.GiveData_To_UI();

        
        gameObject.SetActive(false);
    }
    //bikin function baru lg aja yg kek bakal munculin misal ui angka trus masuk ke mode interface itu aja, abis itu trus yg di sana dblg aja oh lg mode interface itu, jd d player inventory semua diaturnya, lg mode interface itu trus ya gitu, trus kalo teken start kirim datanya ke example chest THE END, aman aman aja ditaro di sini ga siii ??? gbs de, inven ui nya beda

    private void inventoryInput_OnQuitInventory(object sender, EventArgs e)
    {
        HideInventoryUI();
    }

    public void CreateInventoryUI(int size){
        for(int i=0;  i < size;i++){
            InventoryItemUI UI_Item = Instantiate(inventItemUI, contentPanel);
            UI_ItemList.Add(UI_Item);
        }
        UI_ItemList[selectItem].SelectItem();
        
    }
    public void UpdateVisualInventorySlot(int position, InventorySlot item){
        // Debug.Log(position);
        if(!item.itemSO){
            UI_ItemList[position].ResetData();
        }
        else{
            UI_ItemList[position].SetItemData(item.itemSO,item.quantity);
        }
        
    }

    public void SelectItemRight(){
        
        if(tipeInventory == TipeInventory.inventoryOnly){
            selectItem = inventOnly.SelectItemRight(totalRow, totalColumn, selectItem, UI_ItemList);
        }
        else if(tipeInventory == TipeInventory.inventoryWithDesc){
            selectItem = invent_Desc.SelectItemRight_Desc(totalRow, totalColumn, selectItem, UI_ItemList, inventUIDesc);
        }
    }
    public void SelectItemLeft(){
        if(tipeInventory == TipeInventory.inventoryOnly){
            selectItem = inventOnly.SelectItemLeft(totalRow, totalColumn, selectItem, UI_ItemList);
        }
        else if(tipeInventory == TipeInventory.inventoryWithDesc){
            selectItem = invent_Desc.SelectItemLeft_Desc(totalRow, totalColumn, selectItem, UI_ItemList, inventUIDesc);
        }
    }
    public void SelectItemDown(){
        if(tipeInventory == TipeInventory.inventoryOnly){
            selectItem = inventOnly.SelectItemDown(totalRow, totalColumn, selectItem, UI_ItemList, inventorySize);
        }
        else if(tipeInventory == TipeInventory.inventoryWithDesc){
            selectItem = invent_Desc.SelectItemDown_Desc(totalRow, totalColumn, selectItem, UI_ItemList, inventorySize, inventUIDesc);
        }
    }
    public void SelectItemUp(){
        if(tipeInventory == TipeInventory.inventoryOnly){
            selectItem = inventOnly.SelectItemUp(totalRow, totalColumn, selectItem, UI_ItemList, inventorySize);
        }
        else if(tipeInventory == TipeInventory.inventoryWithDesc){
            selectItem = invent_Desc.SelectItemUp_Desc(totalRow, totalColumn, selectItem, UI_ItemList, inventorySize, inventUIDesc);
        }
    }
    public void ShowInventoryUI(){
        if(tipeInventory == TipeInventory.inventoryOnly){
            inventOnly.ShowInventoryUI(2, this.gameObject);
        }
        else if(tipeInventory == TipeInventory.inventoryWithDesc){
            invent_Desc.ShowInventoryUI_Desc(2, this.gameObject, inventUIDesc, UI_ItemList, selectItem);
        }
    }
    public void HideInventoryUI(){
        if(tipeInventory == TipeInventory.inventoryOnly){
            inventOnly.HideInventoryUI(this.gameObject);
        }
        else if(tipeInventory == TipeInventory.inventoryWithDesc){
            invent_Desc.HideInventoryUI(this.gameObject);
        }
    }

}
