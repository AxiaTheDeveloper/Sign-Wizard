using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//buat the whole inventory

public class InventoryOnly{
    private bool keepMoveRight,keepMoveLeft, move;
    public int SelectItemRight(int totalRow, int totalColumn, int selectItem, List<InventoryItemUI> UI_ItemList, int inventorySize){
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
        if(selectItemHere == inventorySize - 1 && keepMoveRight){
            keepMoveRight = false;
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
    public int SelectItemLeft(int totalRow, int totalColumn, int selectItem, List<InventoryItemUI> UI_ItemList, int inventorySize){
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
            if(selectItemHere > inventorySize - 1){
                selectItemHere = inventorySize -1;
            }
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
    public void ShowInventoryUI(WitchGameManager.InterfaceType pilihanInterface, GameObject UI){
        WitchGameManager gameManager = WitchGameManager.Instance; 
        if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.InventoryAndChest){
            gameManager.ChangeInterfaceType(pilihanInterface);
            RectTransform ui_rect = UI.GetComponent<RectTransform>();
            ui_rect.anchoredPosition = new Vector3(-362.5f, -900, 0); 
            LeanTween.move(ui_rect, new Vector3(-362.5f, 25, 0), 0.2f);
            UI.SetActive(true);
            move = true;
        }
        else{
            move = false;
            RectTransform ui_rect = UI.GetComponent<RectTransform>();
            ui_rect.anchoredPosition = new Vector3(-362.5f, 25, 0); 
            gameManager.ChangeInterfaceType(pilihanInterface);
            UI.SetActive(true);
        }
        // inventUIDesc.EmptyDescUI();
        // UpdateVisual_InventDescription();
    }
    public void HideInventoryUI(GameObject UI){
        WitchGameManager.Instance.ChangeToInGame(WitchGameManager.InGameType.normal);
        if(move){
            move = false;
            RectTransform ui_rect = UI.GetComponent<RectTransform>();
            LeanTween.move(ui_rect, new Vector3(-362.5f, -900, 0), 0.2f).setOnComplete(()=>{
                UI.SetActive(false);
            });
        }
        else{
            UI.SetActive(false);
        }
        
        
        
    }
    public event EventHandler<OnItemSubmitPotionkEventArgs> OnItemSubmitPotion;// ini buat di submitpotion
    public class OnItemSubmitPotionkEventArgs : EventArgs{
        public int Position;
        public bool isAdd;
    }
    private List<int> selectItem_SubmitPotion = new List<int>();

    public void SelectItem_SubmitPotion(int selectItem, List<InventoryItemUI> UI_ItemList){
        InventoryItemUI UI_item = UI_ItemList[selectItem];
        if(UI_item.IsSelected_Cooking() && selectItem_SubmitPotion.Count > 0){
            UI_item.DeSelectItem_Cooking();
            selectItem_SubmitPotion.Remove(selectItem);
            OnItemSubmitPotion?.Invoke(this, new OnItemSubmitPotionkEventArgs{
                Position = selectItem, isAdd = false
            });
        }
        else{
            if(!UI_item.IsEmpty()){
                if(UI_item.GetItemData().type == ItemType.potion){
                    // Debug.Log("masuk sini ya ?");
                    selectItem_SubmitPotion.Add(selectItem);
                    UI_item.SelectItem_Cooking();
                    OnItemSubmitPotion?.Invoke(this, new OnItemSubmitPotionkEventArgs{
                        Position = selectItem, isAdd = true
                    });
                    
                    
                }
                else{
                    DialogueManager.Instance.ShowDialogue_WrongChoice_WithBahan(DialogueManager.DialogueWrongChoice.bukanPotion_InventoryUI, UI_item.GetItemData().itemName);
                    // Debug.Log("Bukan Bahan Potion");
                    //debug.log bukan potion
                }
                
            }
        }
    }
    public void DeselectItem(List<InventoryItemUI> UI_ItemList){
        if(selectItem_SubmitPotion.Count > 0){
            List<int> listCopy = new List<int>(selectItem_SubmitPotion);
            foreach(int selectItem in listCopy){
                SelectItem_SubmitPotion(selectItem, UI_ItemList);//deselect item
            }
            
        }
        
    }

    public void Hide_SubmitPotion(GameObject UI, List<InventoryItemUI> UI_ItemList){
        // Debug.Log("close");
        DeselectItem(UI_ItemList);
        
        HideInventoryUI(UI);
    }
}
public class InventoryWithDesc : InventoryOnly{
    public int SelectItemRight_Desc(int totalRow, int totalColumn, int selectItem, List<InventoryItemUI> UI_ItemList, int inventorySize,InventoryUIDesc inventUIDesc, int quantity_Want)
    {
        // Debug.Log("Hi biches");
        int selectItemHere = SelectItemRight(totalRow, totalColumn, selectItem, UI_ItemList, inventorySize);
        UpdateVisual_InventDescription(UI_ItemList, selectItemHere, inventUIDesc, quantity_Want);
        return selectItemHere;
    }
    public int SelectItemLeft_Desc(int totalRow, int totalColumn, int selectItem, List<InventoryItemUI> UI_ItemList, int inventorySize,InventoryUIDesc inventUIDesc, int quantity_Want){
        int selectItemHere = SelectItemLeft(totalRow, totalColumn, selectItem, UI_ItemList, inventorySize);
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
    
    public void ShowInventoryUI_Desc(WitchGameManager.InterfaceType pilihanInterface, GameObject UI, InventoryUIDesc inventUIDesc, List<InventoryItemUI> UI_ItemList, int selectItem, int quantity_Want){
        // Debug.Log("show");
        WitchGameManager.Instance.ChangeInterfaceType(pilihanInterface);
        UI.SetActive(true);
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
    public void UpdateVisual_InventQuantity(InventoryUIDesc inventUIDesc, int quantity_Want, int maxQuantity){
        //dibwh ud di cek lebi ato krg etc, ini kalo angka brubah aja dipanggil
        inventUIDesc.changeQuantityWant(quantity_Want, maxQuantity);
    }
}

public class InventoryCauldron : InventoryOnly{
    public event EventHandler<OnItemCauldronEventArgs> OnItemCauldron;// ini buat di cauldron nanti
    public class OnItemCauldronEventArgs : EventArgs{
        public int Position;
        public bool isAdd;
    }
    
    public void ShowInventoryUI_Cauldron(WitchGameManager.InterfaceType pilihanInterface, GameObject UI){
        // Debug.Log("show");
        WitchGameManager.Instance.ChangeInterfaceType(pilihanInterface);
        
        UI.SetActive(true);

        //shownya dibedain dr yg atas biar kalo ada animasi tidak aneh, soalnya keluar brg UI lain
    }

    public void SelectItem_ForCauldron(int selectItem, List<int> list_selected_Cauldron_Item, List<InventoryItemUI> UI_ItemList){
        InventoryItemUI UI_item = UI_ItemList[selectItem];
        if(UI_item.IsSelected_Cooking()){
            UI_item.DeSelectItem_Cooking();
            list_selected_Cauldron_Item.Remove(selectItem);
            OnItemCauldron?.Invoke(this, new OnItemCauldronEventArgs{
                Position = selectItem, isAdd = false
            });
        }
        else{
            if(list_selected_Cauldron_Item.Count == 3 && !UI_item.IsEmpty()){
                DialogueManager.Instance.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.sudahPenuh_Cauldron);
                // Debug.Log("Uda penuh");
            }
            else if(list_selected_Cauldron_Item.Count < 3 && !UI_item.IsEmpty()){
                if(UI_item.GetItemData().type == ItemType.bahanPotion){
                    list_selected_Cauldron_Item.Add(selectItem);
                    UI_item.SelectItem_Cooking();
                    OnItemCauldron?.Invoke(this, new OnItemCauldronEventArgs{
                        Position = selectItem, isAdd = true
                    });
                }
                else{
                    DialogueManager.Instance.ShowDialogue_WrongChoice_WithBahan(DialogueManager.DialogueWrongChoice.bukanBahanPotion_InventoryUI, UI_item.GetItemData().itemName);
                    // Debug.Log("Bukan Bahan potion");
                }
                
            }
        }
    }
    public void Hide_Cauldron(GameObject UI, List<int> list_selected_Cauldron_Item, List<InventoryItemUI> UI_ItemList){
        
        DeselectItem_ForCauldron(list_selected_Cauldron_Item, UI_ItemList);
        HideInventoryUI(UI);
    }
    public void Hide_CauldronUIOnly(GameObject UI){
        HideInventoryUI(UI);
    }
    public void DeselectItemFromCauldron_Only(List<int> list_selected_Cauldron_Item, List<InventoryItemUI> UI_ItemList){
        DeselectItem_ForCauldron(list_selected_Cauldron_Item, UI_ItemList);
    }
    public void DeselectItem_ForCauldron(List<int> list_selected_Cauldron_Item, List<InventoryItemUI> UI_ItemList){
        List<int> listCopy = new List<int>(list_selected_Cauldron_Item);
        // listCopy = list_selected_Cauldron_Item;
        foreach(int selectItem in listCopy){
            UI_ItemList[selectItem].DeSelectItem_Cooking();
            // Debug.Log("dari inventory ada di posisi " +selectItem);
            list_selected_Cauldron_Item.Remove(selectItem);
            OnItemCauldron?.Invoke(this, new OnItemCauldronEventArgs{
                Position = selectItem, isAdd = false
            });
            
        }
    }

}

public class InventoryPenumbuk : InventoryOnly{
    public event EventHandler<OnItemPenumbukEventArgs> OnItemPenumbuk;// ini buat di cauldron nanti
    public class OnItemPenumbukEventArgs : EventArgs{
        public int Position;
        public bool isAdd;
    }
    private int penumbuk_SelectedItem;
    private bool hasSelectItem;
    public void ShowInventoryUI_Penumbuk(WitchGameManager.InterfaceType pilihanInterface, GameObject UI){
        // Debug.Log("show");
        WitchGameManager.Instance.ChangeInterfaceType(pilihanInterface);



        UI.SetActive(true);
        hasSelectItem = false;
        // Debug.Log("show" + UI.activeSelf);
        
        //shownya dibedain dr yg atas biar kalo ada animasi tidak aneh, soalnya keluar brg UI lain
    }

    public void SelectItem_Penumbuk(int selectItem, List<InventoryItemUI> UI_ItemList){
        InventoryItemUI UI_item = UI_ItemList[selectItem];
        if(UI_item.IsSelected_Cooking() && hasSelectItem){
            UI_item.DeSelectItem_Cooking();
            hasSelectItem = false;
            OnItemPenumbuk?.Invoke(this, new OnItemPenumbukEventArgs{
                Position = selectItem, isAdd = false
            });
        }
        else{
            if(!UI_item.IsEmpty()){
                if(UI_item.GetItemData().type == ItemType.bahanHarusDigerus){
                    // Debug.Log("masuk sini ya ?");
                    penumbuk_SelectedItem = selectItem;
                    hasSelectItem = true;
                    UI_item.SelectItem_Cooking();
                    OnItemPenumbuk?.Invoke(this, new OnItemPenumbukEventArgs{
                        Position = selectItem, isAdd = true
                    });
                }
                else{
                    DialogueManager.Instance.ShowDialogue_WrongChoice_WithBahan(DialogueManager.DialogueWrongChoice.bukanBahanTumbukan_InventoryUI, UI_item.GetItemData().itemName);
                    // Debug.Log("Bukan Bahan yang bisa tumbukan");
                }
                
            }
        }
    }
    public void Hide_Penumbuk(GameObject UI, List<InventoryItemUI> UI_ItemList){
        // Debug.Log("close");
        if(hasSelectItem){
            SelectItem_Penumbuk(penumbuk_SelectedItem, UI_ItemList);//deselect item
        }
        

        HideInventoryUI(UI);
    }

    public void ShowUI_Penumbuk_Tapi_PenumbukIsOpen(WitchGameManager.InterfaceType pilihanInterface, GameObject UI, List<InventoryItemUI> UI_ItemList){
        SelectItem_Penumbuk(penumbuk_SelectedItem, UI_ItemList);//deselect item terpilih
        ShowInventoryUI_Penumbuk(pilihanInterface, UI);
        

    }
    public void HideUI_Penumbuk_Tapi_PenumbukIsOpen(GameObject UI){
        

        HideInventoryUI(UI);
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
        inventoryOnly, inventoryWithDesc, inventoryCauldron, inventoryPenumbuk
    }

    [SerializeField]private TipeInventory tipeInventory;
    private enum OwnerShip{
        Player, Chest
    }
    [SerializeField]private OwnerShip owner;
    private InventoryOnly inventOnly;
    private InventoryWithDesc invent_Desc;
    private InventoryCauldron invent_Cauldron;
    private InventoryPenumbuk invent_Penumbuk;


    private WordManager wordManager;


    //kalo owner bukan player maka ditaro di sini
    private InventoryScriptableObject chestInventory;
    [SerializeField]private Chest chest;

    private int quantity_Want, maxQuantityNow;

    [SerializeField]private GameObject wordPlaceShow_InventDesc;

    [Header("This is for Invent with Cauldron")]

    private List<int> list_selected_Cauldron_Item;
    
    // [SerializeField]private Cauldron cauldron;

    [Header("This is for Invent with Penumbuk")]
    private int selectedItem_Penumbuk;

    [Header("This is for ChestInvent")]
    [SerializeField]private int[] maxItemShown_PerLevel; // misal lvl 1 ya cuma muncul 3 ingredient prtm doang etc etc
    private int chosen_MaxItem;
    private bool hi;
    
    private void Awake() {
        if(tipeInventory == TipeInventory.inventoryWithDesc){
            inventUIDesc.EmptyDescUI();
        }
        if(tipeInventory == TipeInventory.inventoryOnly){
            inventOnly = new InventoryOnly();
        }
        else if(tipeInventory == TipeInventory.inventoryWithDesc){
            invent_Desc = new InventoryWithDesc();
            quantity_Want = 1;
            maxQuantityNow = 0;
        }
        else if(tipeInventory == TipeInventory.inventoryCauldron){
            invent_Cauldron = new InventoryCauldron();
            list_selected_Cauldron_Item = new List<int>();
        }
        else if(tipeInventory == TipeInventory.inventoryPenumbuk){
            invent_Penumbuk = new InventoryPenumbuk();
        }
        
        
    }
    private void Start() {
        
        
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

        //di sini cek skrg player lvl brp, trus cek maxItemshownnya berapa
        
        if(owner == OwnerShip.Player){
            GiveData_To_UI(playerInventory.GetPlayerInventory());
            // Debug.Log(playerInventory.GetPlayerInventory());
            playerInventory.GetPlayerInventory().OnItemUpdate += inventory_OnItemUpdate;
        }
        else if(owner == OwnerShip.Chest){
            int level = PlayerSaveManager.Instance.GetPlayerLevel();
            if(level == 6){
                level = 5;
            }
            chosen_MaxItem = maxItemShown_PerLevel[level];
            GiveData_To_UI(chestInventory);
            chestInventory.OnItemUpdate += otherInventory_OnItemUpdate;
        }
        
        // StartCoroutine(DeactivateGameObjectDelayed());
        gameObject.SetActive(false);
    }



    private void inventory_OnItemUpdate(object sender, InventoryScriptableObject.OnItemUpdateEventArgs e)
    {
        
        UpdateVisualInventorySlot(e.position,playerInventory.GetPlayerInventory().inventSlot[e.position]);
    }
    

    private void otherInventory_OnItemUpdate(object sender, InventoryScriptableObject.OnItemUpdateEventArgs e)
    {

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

        if(item.itemSO == null){
            
            UI_ItemList[position].ResetData();
        }
        else{
            if(tipeInventory == TipeInventory.inventoryWithDesc){
                if(position < chosen_MaxItem){
                    UI_ItemList[position].SetItemData(item.itemSO,item.quantity);
                }
                else{
                    UI_ItemList[position].ResetData();
                }
            }
            else{
                UI_ItemList[position].SetItemData(item.itemSO,item.quantity);
            }
            
        }
    }

    public void SelectItemRight(){
        
        if(tipeInventory == TipeInventory.inventoryOnly){
            selectItem = inventOnly.SelectItemRight(totalRow, totalColumn, selectItem, UI_ItemList, inventorySize);
        }
        else if(tipeInventory == TipeInventory.inventoryWithDesc){
            quantity_Want = 1;
            selectItem = invent_Desc.SelectItemRight_Desc(totalRow, totalColumn, selectItem, UI_ItemList, inventorySize, inventUIDesc, quantity_Want);
            maxQuantityNow = chestInventory.inventSlot[selectItem].quantity;
        }
        else if(tipeInventory == TipeInventory.inventoryCauldron){
            selectItem = invent_Cauldron.SelectItemRight(totalRow, totalColumn, selectItem, UI_ItemList, inventorySize);
        }
        else if(tipeInventory == TipeInventory.inventoryPenumbuk){
            selectItem = invent_Penumbuk.SelectItemRight(totalRow, totalColumn, selectItem, UI_ItemList, inventorySize);
        }
        
    }
    public void SelectItemLeft(){
        if(tipeInventory == TipeInventory.inventoryOnly){
            selectItem = inventOnly.SelectItemLeft(totalRow, totalColumn, selectItem, UI_ItemList, inventorySize);
        }
        else if(tipeInventory == TipeInventory.inventoryWithDesc){
            quantity_Want = 1;
            selectItem = invent_Desc.SelectItemLeft_Desc(totalRow, totalColumn, selectItem, UI_ItemList, inventorySize, inventUIDesc, quantity_Want);
            maxQuantityNow = chestInventory.inventSlot[selectItem].quantity;
        }
        else if(tipeInventory == TipeInventory.inventoryCauldron){
            selectItem = invent_Cauldron.SelectItemLeft(totalRow, totalColumn, selectItem, UI_ItemList, inventorySize);
        }
        else if(tipeInventory == TipeInventory.inventoryPenumbuk){
            selectItem = invent_Penumbuk.SelectItemLeft(totalRow, totalColumn, selectItem, UI_ItemList, inventorySize);
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
        else if(tipeInventory == TipeInventory.inventoryCauldron){
            selectItem = invent_Cauldron.SelectItemDown(totalRow, totalColumn, selectItem, UI_ItemList, inventorySize);
        }
        else if(tipeInventory == TipeInventory.inventoryPenumbuk){
            selectItem = invent_Penumbuk.SelectItemDown(totalRow, totalColumn, selectItem, UI_ItemList, inventorySize);
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
        else if(tipeInventory == TipeInventory.inventoryCauldron){
            selectItem = invent_Cauldron.SelectItemUp(totalRow, totalColumn, selectItem, UI_ItemList, inventorySize);
        }
        else if(tipeInventory == TipeInventory.inventoryPenumbuk){
            selectItem = invent_Penumbuk.SelectItemUp(totalRow, totalColumn, selectItem, UI_ItemList, inventorySize);
        }
    }
    public void ShowInventoryUI(){
        if(tipeInventory == TipeInventory.inventoryOnly){
            inventOnly.ShowInventoryUI(WitchGameManager.InterfaceType.InventoryTime, this.gameObject);
        }
        else if(tipeInventory == TipeInventory.inventoryWithDesc){
            invent_Desc.ShowInventoryUI_Desc(WitchGameManager.InterfaceType.InventoryAndChest, this.gameObject, inventUIDesc, UI_ItemList, selectItem, quantity_Want);
            maxQuantityNow = chestInventory.inventSlot[selectItem].quantity;
        }
        else if(tipeInventory == TipeInventory.inventoryCauldron){
            invent_Cauldron.ShowInventoryUI_Cauldron(WitchGameManager.InterfaceType.InventoryAndCauldron, this.gameObject);
        }
        else if(tipeInventory == TipeInventory.inventoryPenumbuk){
            
            invent_Penumbuk.ShowInventoryUI_Penumbuk(WitchGameManager.InterfaceType.InventoryAndPenumbuk, this.gameObject);
            // Debug.Log(gameManager.IsInterfaceType());
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
        else if(tipeInventory == TipeInventory.inventoryCauldron){
            invent_Cauldron.Hide_Cauldron(this.gameObject, list_selected_Cauldron_Item, UI_ItemList);
        }
        else if(tipeInventory == TipeInventory.inventoryPenumbuk){
            
            invent_Penumbuk.Hide_Penumbuk(this.gameObject, UI_ItemList);
        }
    }
    public void HideInventoryUI_SubmitPotion(){
        if(tipeInventory == TipeInventory.inventoryOnly){
            inventOnly.Hide_SubmitPotion(this.gameObject, UI_ItemList);
        }
    }

    public void ShowInventory_PenumbukIsOpen(){
        if(tipeInventory == TipeInventory.inventoryPenumbuk){
            invent_Penumbuk.ShowUI_Penumbuk_Tapi_PenumbukIsOpen(WitchGameManager.InterfaceType.InventoryAndPenumbuk, this.gameObject, UI_ItemList);
        }
    }
    public void HideInventory_PenumbukIsOpen(){
        if(tipeInventory == TipeInventory.inventoryPenumbuk){
            // invent_Penumbuk.HideUI_Penumbuk_Tapi_PenumbukIsOpen(this.gameObject);
            gameManager.ChangeInterfaceType(WitchGameManager.InterfaceType.TumbukTime); 
        }
    }
    public void HideCauldronInventory_Only(){
        if(tipeInventory == TipeInventory.inventoryCauldron){
            invent_Cauldron.Hide_CauldronUIOnly(this.gameObject);
        }
    }
    public void DeselectItemCauldron_Only(){
        if(tipeInventory == TipeInventory.inventoryCauldron){
            invent_Cauldron.DeselectItemFromCauldron_Only(list_selected_Cauldron_Item, UI_ItemList);
        }
    }
    public void DeselectItem_SubmitPotion(){
        if(tipeInventory == TipeInventory.inventoryOnly){
            inventOnly.DeselectItem(UI_ItemList);
        }
    }

    public void SelectItem_Cauldron(){
        if(tipeInventory == TipeInventory.inventoryCauldron){
            invent_Cauldron.SelectItem_ForCauldron(selectItem, list_selected_Cauldron_Item, UI_ItemList);
        }
        else if (tipeInventory == TipeInventory.inventoryPenumbuk){
            invent_Penumbuk.SelectItem_Penumbuk(selectItem, UI_ItemList);
            // Debug.Log(playerInventory);
        }
        else if(tipeInventory == TipeInventory.inventoryOnly){
            inventOnly.SelectItem_SubmitPotion(selectItem, UI_ItemList);
        }
    }
    public void ChangeQuantityWant(int change){
        if(tipeInventory == TipeInventory.inventoryWithDesc){
            if(change == 1 && quantity_Want < maxQuantityNow){
                quantity_Want += change;
                invent_Desc.UpdateVisual_InventQuantity(inventUIDesc, quantity_Want,maxQuantityNow);
            }
            else if(change == -1 && quantity_Want > 1){
                quantity_Want += change;
                invent_Desc.UpdateVisual_InventQuantity(inventUIDesc, quantity_Want, maxQuantityNow);
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
        invent_Desc.UpdateVisual_InventQuantity(inventUIDesc, quantity_Want, maxQuantityNow);
    }

    public void GiveData_To_UI(InventoryScriptableObject inventory){
        //updet semua
        for(int i=0;i<inventorySize;i++){
            UpdateVisualInventorySlot(i,inventory.inventSlot[i]);
            
        }
    }

    public void moveChestUI(bool moveRight){
        if(tipeInventory == TipeInventory.inventoryWithDesc){
            
            if(moveRight){
                inventUIDesc.Show_Hide_WordPlace();
                RectTransform rect = GetComponent<RectTransform>();
                LeanTween.move(rect, new Vector3(850f, 25f, 0f), 0.2f);
            }
            else{
                RectTransform rect = GetComponent<RectTransform>();
                LeanTween.move(rect, new Vector3(0f, 25f, 0f), 0.2f).setOnComplete(
                    ()=>inventUIDesc.Show_Hide_WordPlace()
                );
                
            }

            
        }
    }
    public int GetSelectedItem(){
        return selectItem;
    }
    public InventoryCauldron GetInventoryCauldron(){
        return invent_Cauldron;
    }
    public InventoryPenumbuk GetInventoryPenumbuk(){
        return invent_Penumbuk;
    }
    public InventoryOnly GetInventoryOnly(){
        return inventOnly;
    }
    public bool isUIEmpty(){
        return UI_ItemList[selectItem].IsEmpty();
    }







}
