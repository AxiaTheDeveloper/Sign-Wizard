using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUIDesc : MonoBehaviour
{
    
    [SerializeField]private Image item_Image;
    [SerializeField]private TextMeshProUGUI item_Title;
    [SerializeField]private TextMeshProUGUI item_Desc;
    [SerializeField]private GameObject wholeQuantity;
    [SerializeField]private TextMeshProUGUI quantity_PlayerWant;

    private void Awake() {
        EmptyDescUI();
    }
    private void Start() {
        
    }
    public void EmptyDescUI(){
        item_Image.gameObject.SetActive(false);
        item_Title.text = "";
        item_Desc.text = "";
        wholeQuantity.SetActive(false);

    }
    public void SetItemDataDesc(Sprite spriteItem, string itemTitle, string itemDesc, int quantityWant){
        item_Image.sprite = spriteItem;
        item_Image.gameObject.SetActive(true);
        item_Title.text = itemTitle;
        item_Desc.text = itemDesc;
        quantity_PlayerWant.text = quantityWant.ToString();
        wholeQuantity.SetActive(true);
    }
    public void changeQuantityWant(int quantityWant){
        quantity_PlayerWant.text = quantityWant.ToString();
    }
}
