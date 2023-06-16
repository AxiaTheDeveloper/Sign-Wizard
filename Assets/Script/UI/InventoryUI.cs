using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//buat the whole inventory

public class InventoryOnly{
    private bool keepMoveRight,keepMoveLeft;
    public int SelectItemRight(int totalRow, int totalColumn, int selectItem, List<InventoryItemUI> UI_ItemList){
        // Debug.Log("Hi bich");
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
            int row = selectItemHere/totalColumn;
            selectItemHere -= (totalColumn * (row));
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
            int row = totalRow-1;
            int selectItemCount = 0;
            do{
                selectItemCount = selectItemHere + (totalColumn * (row));
                --row;
            }while(selectItemCount > inventorySize - 1);
            selectItemHere = selectItemCount;
            
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
    public int SelectItemRight_Desc(int totalRow, int totalColumn, int selectItem, List<InventoryItemUI> UI_ItemList, InventoryUIDesc inventUIDesc, int quantity_Want)
    {
        // Debug.Log("Hi biches");
        int selectItemHere = SelectItemRight(totalRow, totalColumn, selectItem, UI_ItemList);
        UpdateVisual_InventDescription(UI_ItemList, selectItemHere, inventUIDesc, quantity_Want);
        return selectItemHere;
    }
    public int SelectItemLeft_Desc(int totalRow, int totalColumn, int selectItem, List<InventoryItemUI> UI_ItemList, InventoryUIDesc inventUIDesc, int quantity_Want){
        int selectItemHere = SelectItemLeft(totalRow, totalColumn, selectItem, UI_ItemList);
        UpdateVisual_InventDescription(UI_ItemList, selectItemHere, inventUIDesc, quantity_Want);
        return selectItemHere;
    }
    public int SelectItemDown_Desc(int totalRow, int totalColumn, int selectItem, List<InventoryItemUI> UI_ItemList, int inventorySize, InventoryUIDesc inventUIDesc, int quantity_Want){
        int selectItemHere = SelectItemDown(totalRow, totalColumn, selectItem, UI_ItemList, inventorySize);
        UpdateVisual_InventDescription(UI_ItemList, selectItemHere, inventUIDesc, quantity_Want);
        return selectItemHere;
    }
    public int SelectItemUp_Desc(int totalRow, int totalColumn, int selectItem, List<InventoryItemUI> UI_ItemList, int inventorySize, InventoryUIDesc inventUIDesc, int quantity_Want){
        int selectItemHere = SelectItemUp(totalRow, totalColumn, selectItem, UI_ItemList, inventorySize);
        UpdateVisual_InventDescription(UI_ItemList, selectItemHere, inventUIDesc, quantity_Want);
        return selectItemHere;
    }
    
    public void ShowInventoryUI_Desc(int pilihanInterface, GameObject UI, InventoryUIDesc inventUIDesc, List<InventoryItemUI> UI_ItemList, int selectItem, int quantity_Want){
        // Debug.Log("show");
        ShowInventoryUI(pilihanInterface, UI);
        inventUIDesc.EmptyDescUI();
        UpdateVisual_InventDescription(UI_ItemList, selectItem, inventUIDesc, quantity_Want);
    }

    public void UpdateVisual_InventDescription(List<InventoryItemUI> UI_ItemList, int selectItem, InventoryUIDesc inventUIDesc, int quantity_Want){
        
        if(UI_ItemList[selectItem].IsEmpty()){
            // Debug.Log("this?");
            inventUIDesc.EmptyDescUI();
        }
        else{
            
            ItemScriptableObject item = UI_ItemList[selectItem].GetItemData();
            
            inventUIDesc.SetItemDataDesc(item.itemSprite,item.itemName,item.Desc,quantity_Want, UI_ItemList[selectItem].GetPosisiWord());
            // Debug.Log(UI_ItemList[selectItem].GetPosisiWord().position);
        }
    }
    public void UpdateVisual_InventQuantity(InventoryUIDesc inventUIDesc, int quantity_Want){
        //dibwh ud di cek lebi ato krg etc, ini kalo angka brubah aja dipanggil
        inventUIDesc.changeQuantityWant(quantity_Want);
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
    [SerializeField]private int totalRow, totalColumn;//kolom kanan, row bwh, atur ini biar select tidak aneh WKWKKW

    private enum TipeInventory{
        inventoryOnly, inventoryWithDesc
    }

    [SerializeField]private TipeInventory tipeInventory;
    private enum OwnerShip{
        Player, Chest
    }
    [SerializeField]private OwnerShip owner;
    private InventoryOnly inventOnly;
    private InventoryWithDesc invent_Desc;


    private WordManager wordManager;


    //kalo owner bukan player maka ditaro di sini
    private InventoryScriptableObject chestInventory;
    [SerializeField]private Chest chest;

    private int quantity_Want, maxQuantityNow;

    [SerializeField]private GameObject wordPlaceShow_InventDesc;
    
    private void Awake() {
        if(tipeInventory == TipeInventory.inventoryWithDesc){
            inventUIDesc.EmptyDescUI();
        }
        
    }
    private void Start() {
        
        if(tipeInventory == TipeInventory.inventoryOnly){
            inventOnly = new InventoryOnly();
        }
        else if(tipeInventory == TipeInventory.inventoryWithDesc){
            invent_Desc = new InventoryWithDesc();
            quantity_Want = 1;
            maxQuantityNow = 0;
        }
        UI_ItemList = new List<InventoryItemUI>();
        if(owner == OwnerShip.Player){
            playerInventory.OnQuitInventory += playerInventory_OnQuitInventory;
            inventorySize = playerInventory.GetInventorySize();
        }
        else if(owner == OwnerShip.Chest){
            chestInventory = chest.GetChestInventory();
            inventorySize = chestInventory.inventSlot.Count;
        }
        
        selectItem = 0;
        CreateInventoryUI(inventorySize);
        if(owner == OwnerShip.Player){
            GiveData_To_UI(playerInventory.GetPlayerInventory());
            // Debug.Log(playerInventory.GetPlayerInventory());
            playerInventory.GetPlayerInventory().OnItemUpdate += inventory_OnItemUpdate;
        }
        else if(owner == OwnerShip.Chest){
            GiveData_To_UI(chestInventory);
            chestInventory.OnItemUpdate += otherInventory_OnItemUpdate;
        }
        
        

        
        StartCoroutine(DeactivateGameObjectDelayed());
    }
    private IEnumerator DeactivateGameObjectDelayed()
    {
        yield return null; // Wait for the next frame update
        gameObject.SetActive(false);
    }

    private void inventory_OnItemUpdate(object sender, InventoryScriptableObject.OnItemUpdateEventArgs e)
    {
        
        UpdateVisualInventorySlot(e.position,playerInventory.GetPlayerInventory().inventSlot[e.position]);
    }

    private void otherInventory_OnItemUpdate(object sender, InventoryScriptableObject.OnItemUpdateEventArgs e)
    {
        // Debug.Log("masuk sini");
        UpdateVisualInventorySlot(e.position,chestInventory.inventSlot[e.position]);
    }

    //bikin function baru lg aja yg kek bakal munculin misal ui angka trus masuk ke mode interface itu aja, abis itu trus yg di sana dblg aja oh lg mode interface itu, jd d player inventory semua diaturnya, lg mode interface itu trus ya gitu, trus kalo teken start kirim datanya ke example chest THE END, aman aman aja ditaro di sini ga siii ??? gbs de, inven ui nya beda

    private void playerInventory_OnQuitInventory(object sender, EventArgs e)
    {
        HideInventoryUI();
    }

    public void CreateInventoryUI(int size){
        for(int i=0;  i < size;i++){
            InventoryItemUI UI_Item = Instantiate(inventItemUI, contentPanel);
            UI_ItemList.Add(UI_Item);
        }
        UI_ItemList[selectItem].SelectItem();
        // Debug.Log(UI_ItemList[selectItem].transform.position);
        
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
            quantity_Want = 1;
            selectItem = invent_Desc.SelectItemRight_Desc(totalRow, totalColumn, selectItem, UI_ItemList, inventUIDesc, quantity_Want);
            maxQuantityNow = chestInventory.inventSlot[selectItem].quantity;
        }
    }
    public void SelectItemLeft(){
        if(tipeInventory == TipeInventory.inventoryOnly){
            selectItem = inventOnly.SelectItemLeft(totalRow, totalColumn, selectItem, UI_ItemList);
        }
        else if(tipeInventory == TipeInventory.inventoryWithDesc){
            quantity_Want = 1;
            selectItem = invent_Desc.SelectItemLeft_Desc(totalRow, totalColumn, selectItem, UI_ItemList, inventUIDesc, quantity_Want);
            maxQuantityNow = chestInventory.inventSlot[selectItem].quantity;
        }
    }
    public void SelectItemDown(){
        if(tipeInventory == TipeInventory.inventoryOnly){
            selectItem = inventOnly.SelectItemDown(totalRow, totalColumn, selectItem, UI_ItemList, inventorySize);
        }
        else if(tipeInventory == TipeInventory.inventoryWithDesc){
            quantity_Want = 1;
            selectItem = invent_Desc.SelectItemDown_Desc(totalRow, totalColumn, selectItem, UI_ItemList, inventorySize, inventUIDesc, quantity_Want);
            maxQuantityNow = chestInventory.inventSlot[selectItem].quantity;
        }
    }
    public void SelectItemUp(){
        
        if(tipeInventory == TipeInventory.inventoryOnly){
            selectItem = inventOnly.SelectItemUp(totalRow, totalColumn, selectItem, UI_ItemList, inventorySize);
        }
        else if(tipeInventory == TipeInventory.inventoryWithDesc){
            quantity_Want = 1;
            selectItem = invent_Desc.SelectItemUp_Desc(totalRow, totalColumn, selectItem, UI_ItemList, inventorySize, inventUIDesc, quantity_Want);
            maxQuantityNow = chestInventory.inventSlot[selectItem].quantity;
        }
    }
    public void ShowInventoryUI(){
        if(tipeInventory == TipeInventory.inventoryOnly){
            inventOnly.ShowInventoryUI(2, this.gameObject);
        }
        else if(tipeInventory == TipeInventory.inventoryWithDesc){
            invent_Desc.ShowInventoryUI_Desc(2, this.gameObject, inventUIDesc, UI_ItemList, selectItem, quantity_Want);
            maxQuantityNow = chestInventory.inventSlot[selectItem].quantity;
        }
    }
    public void HideInventoryUI(){
        if(tipeInventory == TipeInventory.inventoryOnly){
            inventOnly.HideInventoryUI(this.gameObject);
        }
        else if(tipeInventory == TipeInventory.inventoryWithDesc){
            quantity_Want = 1;
            invent_Desc.HideInventoryUI(this.gameObject);
            wordPlaceShow_InventDesc.SetActive(false);
            // Debug.Log(wordPlaceShow_InventDesc.activeSelf);
        }
    }
    public void ChangeQuantityWant(int change){
        if(tipeInventory == TipeInventory.inventoryWithDesc){
            if(change == 1 && quantity_Want < maxQuantityNow){
                quantity_Want += change;
                invent_Desc.UpdateVisual_InventQuantity(inventUIDesc, quantity_Want);
            }
            else if(change == -1 && quantity_Want > 1){
                quantity_Want += change;
                invent_Desc.UpdateVisual_InventQuantity(inventUIDesc, quantity_Want);
                wordPlaceShow_InventDesc.SetActive(false);
            }
        }
    }
    public int GetQuantityWant(){
        return quantity_Want;
    }
    public void ResetQuantityWant(){
        quantity_Want = 1;
        maxQuantityNow = chestInventory.inventSlot[selectItem].quantity;
        invent_Desc.UpdateVisual_InventQuantity(inventUIDesc, quantity_Want);
    }

    public void GiveData_To_UI(InventoryScriptableObject inventory){
        //updet semua
        for(int i=0;i<inventorySize;i++){
            UpdateVisualInventorySlot(i,inventory.inventSlot[i]);
        }
    }
    public int GetSelectedItem(){
        return selectItem;
    }

}
