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
    [SerializeField]private TextMeshProUGUI quantity_Text;
    [SerializeField]private Image borderSelect, borderSelectCauldron;
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
        // Debug.Log("TEST");
        // Debug.Log(itemImage + "set");
        itemImage.sprite = itemSO.itemSprite;

        quantity_Text.text = quantity.ToString();
        empty = false;
        RefreshVisualUI();
        // Debug.Log("eii");
    }
    public void ResetData(){
        empty = true;
        RefreshVisualUI();
    }
    public void SelectItem(){
        borderSelect.enabled = true;
        //save an item semua data dikirim keeee informasi
    }
    public void DeselectItem(){
        borderSelect.enabled = false;
    }

    public void SelectItem_Cooking(){
        borderSelectCauldron.enabled = true;
        selected_Cauldron = true;
    }
    public void DeSelectItem_Cooking(){
        borderSelectCauldron.enabled = false;
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
