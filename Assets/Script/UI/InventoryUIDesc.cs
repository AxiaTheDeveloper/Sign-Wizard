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
    [SerializeField]private WordManager wordManager_Chest;
    [SerializeField]private GameObject wordPlace_GameObject;
    [SerializeField]private GameObject quantityUI;
    [SerializeField]WordInput wordInput;
    private bool isFirsTime = true;

    private void Awake() {
        EmptyDescUI();
    }
    public void EmptyDescUI(){
        item_Image.gameObject.SetActive(false);
        item_Title.text = "";
        item_Desc.text = "";
        wordInput.ChangeAdaWord(false);
        wordManager_Chest.changeTheWord("");
        
        wordPlace_GameObject.SetActive(false);
        wholeQuantity.SetActive(false);
    }
    private void Update() {
        if(WitchGameManager.Instance.IsInterfaceType() == WitchGameManager.InterfaceType.QuantityTime){
            wordPlace_GameObject.SetActive(true);
            if(!quantityUI.activeSelf){
                // wordPlace_GameObject.SetActive(false);
                
                quantityUI.SetActive(true);
            }
        }
        else{
            if(quantityUI.activeSelf){
                // wordPlace_GameObject.SetActive(true);
                quantityUI.SetActive(false);
            }
            
        }
    }
    public void Show_Hide_WordPlace(){
        if(wordPlace_GameObject.activeSelf){
            wordPlace_GameObject.SetActive(false);
        }
        else{
            wordPlace_GameObject.SetActive(true);
        }
    }
    public void SetItemDataDesc(Sprite spriteItem, string itemTitle, string itemDesc, int quantityWant, Transform posisiWord){
        item_Image.sprite = spriteItem;
        item_Image.gameObject.SetActive(true);
        
        item_Title.text = itemTitle;
        item_Desc.text = itemDesc;
        quantity_PlayerWant.text = quantityWant.ToString();
        wholeQuantity.SetActive(true);
        wordInput.ChangeAdaWord(true);
        wordManager_Chest.changeTheWord(itemTitle);
        
        // Debug.Log(posisiWord.position);
        
        if(isFirsTime){
            isFirsTime = false;
        }
        else{
            wordPlace_GameObject.transform.position = posisiWord.position;
        }
        
        wordPlace_GameObject.SetActive(true);
    }
    public void changeQuantityWant(int quantityWant){
        quantity_PlayerWant.text = quantityWant.ToString();
    }
}
