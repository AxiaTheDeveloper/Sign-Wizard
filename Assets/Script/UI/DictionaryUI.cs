using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryUI : MonoBehaviour
{
    [SerializeField]private WitchGameManager gameManager;
    [SerializeField]private GameInput gameInput;
    [SerializeField]private GameObject[] dictionaryPageList;
    private int pageNow;
    private int totalPage;
    private void Start() {
        pageNow = 0;
        totalPage = dictionaryPageList.Length;
        UpdatePage();
        gameObject.SetActive(false);
    }

    private void UpdatePage(){
        foreach(GameObject dictionaryPage in dictionaryPageList){
            dictionaryPage.SetActive(false);
        }
        dictionaryPageList[pageNow].SetActive(true);
    }
    private void Update() {
        if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.DictionaryTime){
            if(gameInput.GetInputEscape()){
                HideUI();
            }
            ChangePage();

        }
    }
    private void ChangePage(){
        Vector2 keyArrowInput = gameInput.GetInputArrow();
        if(keyArrowInput.x == -1 && pageNow > 0){
            pageNow--;
        }
        else if(keyArrowInput.x == 1 && pageNow < totalPage-1){
            pageNow++;
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
