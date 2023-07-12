using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubmitPotionUI : MonoBehaviour
{
    [SerializeField]private GameObject UI_AskingWhichPotion, UI_AreYouSure;
    [SerializeField]private GameObject Selected_Yes, Selected_No;
    private bool isChosePotion;

    [SerializeField]private TextMeshProUGUI text_AreYouSure;

    private void Start() {
        isChosePotion = false;
        Selected_On();
        HideAllUI();
    }

    private void Selected_On(){
        if(isChosePotion){
            Selected_Yes.SetActive(true);
            Selected_No.SetActive(false);
        }
        else{
            Selected_Yes.SetActive(false);
            Selected_No.SetActive(true);
        }
        
    }
    public void Show_AskingWhichPotion(){
        UI_AskingWhichPotion.SetActive(true);
        UI_AreYouSure.SetActive(false);
        isChosePotion = false;
        Selected_On();
    }
    public void Show_AreYouSure(string potionName){
        text_AreYouSure.text = "Apakah aku akan kirim potion ini ?";
        UI_AskingWhichPotion.SetActive(false);
        UI_AreYouSure.SetActive(true);
    }

    public void ShowAllUI(){
        gameObject.SetActive(true);
    }
    public void HideAllUI(){
        gameObject.SetActive(false);
        Show_AskingWhichPotion();
        
    }

    public void Change_YesNo(){
        isChosePotion = !isChosePotion;
        Selected_On();
    }
    public bool GetIsChosePotion(){
        return isChosePotion;
    }

}
