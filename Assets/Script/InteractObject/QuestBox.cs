using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class QuestBox : MonoBehaviour
{
    [SerializeReference]private RectTransform questLetter, letter;
    private bool isShow;

    
    [SerializeField]private WitchGameManager gameManager;
    [SerializeField]private PlayerInventory playerInventory;
    [SerializeField]private QuestManager questManager;
    private bool hasCheckFirstTime = false;

    private void Start() {
        LeanTween.move(questLetter, new Vector3(0, 1050, 0f), 0.2f);
        letter.anchoredPosition = new Vector3(0, -800f, 0f);
        playerInventory.OnQuitQuestBox += playerInventory_OnQuitQuestBox;
    }

    private void playerInventory_OnQuitQuestBox(object sender, EventArgs e)
    {
        HideUI();
    }

    public void ShowUI(){
        
        gameManager.ChangeToCinematic();
        letter.anchoredPosition = new Vector3(0, -800f, 0f);
        LeanTween.move(letter, new Vector3(0, 800f, 0f), 0.2f).setOnComplete(
            ()=> LeanTween.move(questLetter, new Vector3(0, 0, 0f), 0.8f).setDelay(0.2f).setOnComplete(
                ()=> Show()
            )
        );
        
        
    }
    private void Show(){
        gameManager.ChangeInterfaceType(WitchGameManager.InterfaceType.InterfaceQuestBox);
        if(!hasCheckFirstTime){
            questManager.UpdateData_QuestLog();
        }
        hasCheckFirstTime = true;
    }

    private void HideUI(){
        LeanTween.move(questLetter, new Vector3(0, 1050, 0f), 0.2f).setOnComplete(
            ()=> gameManager.ChangeToInGame()
        );
        letter.anchoredPosition = new Vector3(0, -800f, 0f);
    }

    public bool GetHasCheckFirstTime(){
        return hasCheckFirstTime;
    }
}
