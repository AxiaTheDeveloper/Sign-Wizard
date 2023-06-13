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
    [SerializeField]private Image borderSelect;

    
    private bool empty = true;
    private void Awake() {
        ResetData();
        DeselectItem();
    }
    private void Start() {
        
    }
    private void Update() {
        

    }

    private void RefreshVisualUI(){
        if(empty){
            itemImage.gameObject.SetActive(false);
        }
        else{
            itemImage.gameObject.SetActive(true);
        }
    }
    public void SetItemData(ItemScriptableObject newItemSO, int quantity){
        itemSO = newItemSO;
        itemImage.sprite = itemSO.itemSprite;
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
        //save an item semua data dikirim keeee informasi
    }
    public void DeselectItem(){
        borderSelect.enabled = false;
    }

    public bool IsEmpty(){
        return empty;
    }
    public ItemScriptableObject GetItemData(){
        return itemSO;
    }
}
