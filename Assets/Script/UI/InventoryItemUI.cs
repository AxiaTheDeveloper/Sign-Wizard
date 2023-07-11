using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



//ngurusin per 1-1 kotak

public class InventoryItemUI : MonoBehaviour
{
    private ItemScriptableObject itemSO;
    [SerializeField]private Image itemImage;
    [SerializeField]private TextMeshProUGUI quantity_Text, itemName;
    [SerializeField]private Image borderSelect, borderSelectCauldron, border_mainBG;
    [SerializeField]private Transform posisiWord;

    
    private bool empty = true;
    private bool selected_Cauldron = false;
    private void Awake() {
        ResetData();
        DeselectItem();
        DeSelectItem_Cooking();
    }





    private void RefreshVisualUI(){

        if(itemImage != null){
            if(empty){
                itemImage.gameObject.SetActive(false);
            }
            else{
                itemImage.gameObject.SetActive(true);
            }
        }
        
    }

    public void SetItemData(ItemScriptableObject newItemSO, int quantity){
        itemSO = newItemSO;
        if(itemName != null){
            itemName.text = newItemSO.itemName;
        }
        // Debug.Log("TEST");
        // Debug.Log(itemImage + "set");
        if(itemImage != null){
            itemImage.sprite = itemSO.itemSprite;
        }
        

        quantity_Text.text = quantity.ToString();
        empty = false;
        RefreshVisualUI();
        
    }
    public void ResetData(){
        empty = true;
        RefreshVisualUI();
    }
    public void SelectItem(){
        borderSelect.enabled = true;
        
    }
    public void DeselectItem(){
        borderSelect.enabled = false;
    }

    public void SelectItem_Cooking(){
        borderSelectCauldron.enabled = true;
        border_mainBG.enabled = false;
        selected_Cauldron = true;
    }
    public void DeSelectItem_Cooking(){
        borderSelectCauldron.enabled = false;
        border_mainBG.enabled = true;
        selected_Cauldron = false;
    }

    public bool IsEmpty(){
        return empty;
    }
    public bool IsSelected_Cooking(){
        return selected_Cauldron;
    }
    public ItemScriptableObject GetItemData(){
        return itemSO;
    }
    public Transform GetPosisiWord(){
        return posisiWord;
    }
}
