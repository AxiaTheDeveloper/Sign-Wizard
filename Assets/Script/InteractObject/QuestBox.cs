using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class QuestBox : MonoBehaviour
{
    [SerializeReference]private RectTransform questLetter, letter, mainLetter;
    private bool isShow, isMainLetter = false;

    
    [SerializeField]private WitchGameManager gameManager;
    [SerializeField]private PlayerInventory playerInventory;
    [SerializeField]private QuestManager questManager;
    private bool hasCheckFirstTime = false;
    [SerializeField]private TextMeshProUGUI questDesc, questSender;

    private void Start() {
        LeanTween.move(questLetter, new Vector3(93.0238f, 1568.225f, 0f), 0.2f);
        LeanTween.move(mainLetter, new Vector3(120.166f, 1535, 0f), 0.2f);
        letter.anchoredPosition = new Vector3(0, -800f, 0f);
        playerInventory.OnQuitQuestBox += playerInventory_OnQuitQuestBox;
    }

    private void playerInventory_OnQuitQuestBox(object sender, EventArgs e)
    {
        HideUI();
    }
    public void SetData(QuestScriptableObject questSO){
        questDesc.text = questSO.QuestinMail;
        questSender.text = "- " + questSO.nameSender;

    }

    public void ShowUI(){
        
        gameManager.ChangeToCinematic();
        letter.anchoredPosition = new Vector3(0, -800f, 0f);
        SoundManager.Instance.PlayOpenEnvelope();
        LeanTween.move(letter, new Vector3(0, 800f, 0f), 0.2f).setOnComplete(
            ()=> LeanTween.move(questLetter, new Vector3(93.0238f, -208.5263f, 0f), 0.8f).setDelay(0.2f).setOnComplete(
                ()=> Show()
            )
        );
        
    }


    public void ShowUI_MainLetter(){
        isMainLetter = true;
        gameManager.ChangeToCinematic();
        letter.anchoredPosition = new Vector3(0, -800f, 0f);
        SoundManager.Instance.PlayOpenEnvelope();
        LeanTween.move(letter, new Vector3(0, 800f, 0f), 0.2f).setOnComplete(
            ()=> LeanTween.move(mainLetter, new Vector3(120.166f, -241, 0f), 0.8f).setDelay(0.2f).setOnComplete(
                ()=> gameManager.ChangeInterfaceType(WitchGameManager.InterfaceType.InterfaceQuestBox)
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
        if(isMainLetter){
            isMainLetter = false;
            LeanTween.move(mainLetter, new Vector3(120.166f, 1535, 0f), 0.2f).setOnComplete(
                ()=> gameManager.ChangeToInGame()
            );
        }
        else{
            LeanTween.move(questLetter, new Vector3(93.0238f, 1568.225f, 0f), 0.2f).setOnComplete(
                ()=> gameManager.ChangeToInGame()
            );
        }
        
        letter.anchoredPosition = new Vector3(0, -800f, 0f);
    }

    public bool GetHasCheckFirstTime(){
        return hasCheckFirstTime;
    }
}
