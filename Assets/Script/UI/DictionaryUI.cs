using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryUI : MonoBehaviour
{
    [SerializeField]private WitchGameManager gameManager;
    [SerializeField]private GameInput gameInput;
    [SerializeField]private GameObject[] potionPageList;
    [SerializeField]private GameObject runePage, potionPage;
    private int pageNow_Potion, totalPage, pagePart;
    private void Start() {
        pageNow_Potion = 0;
        pagePart = 0;
        totalPage = potionPageList.Length;
        UpdatePage();
        gameObject.SetActive(false);
    }

    private void UpdatePage(){
        if(pagePart == 0){
            runePage.SetActive(true);
            potionPage.SetActive(false);
        }
        else if(pagePart == 1){
            runePage.SetActive(false);
            potionPage.SetActive(true);
            
            foreach(GameObject dictionaryPage in potionPageList){
                dictionaryPage.SetActive(false);
            }
            potionPageList[pageNow_Potion].SetActive(true);
        }
        
    }
    private void Update() {
        if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.DictionaryTime){
            if(gameInput.GetInputEscape()){
                HideUI();
            }
            if(pagePart == 1){
                ChangePage_Potion();
            }
            
            ChangePagePart();

        }
    }
    private void ChangePagePart(){
        Vector2 keyArrowInput = gameInput.GetInputArrow_Dictionary();
        if(keyArrowInput.y == -1 && pagePart == 0){
            // Debug.Log("aaa");
            pagePart = 1;
        }
        else if(keyArrowInput.y == 1 && pagePart == 1){
            // Debug.Log("aaasss");
            pagePart = 0;
        }
        UpdatePage();
    }
    private void ChangePage_Potion(){
        Vector2 keyArrowInput = gameInput.GetInputArrow_Dictionary();
        if(keyArrowInput.x == -1 && pageNow_Potion > 0){
            pageNow_Potion--;
        }
        else if(keyArrowInput.x == 1 && pageNow_Potion < totalPage-1){
            pageNow_Potion++;
        }
        UpdatePage();
    }
    
    

    public void ShowUI(){
        gameManager.ChangeInterfaceType(WitchGameManager.InterfaceType.DictionaryTime);
        gameObject.SetActive(true);
    }
    public void HideUI(){
        gameObject.SetActive(false);
        gameManager.ChangeToInGame();
    }
}
