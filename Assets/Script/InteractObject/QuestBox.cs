using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class QuestBox : MonoBehaviour
{
    [SerializeReference]private RectTransform questLetter, letter, mainLetter, giftLetter;
    private bool isShow, isMainLetter = false, isGiftLetter = false;

    
    [SerializeField]private WitchGameManager gameManager;
    [SerializeField]private PlayerInventory playerInventory;
    [SerializeField]private QuestManager questManager;
    private bool hasCheckFirstTime = false;
    [SerializeField]private TextMeshProUGUI questDesc, questSender, giftDesc, giftSender;
    [SerializeField]private GameObject[] giftImage;
    [SerializeField]private PlayerSaveManager playerSave;
    private DialogueManager dialogueManager;

    private void Start() {
        LeanTween.move(questLetter, new Vector3(93.0238f, 1568.225f, 0f), 0.2f);
        LeanTween.move(mainLetter, new Vector3(120.166f, 1535, 0f), 0.2f);
        LeanTween.move(giftLetter, new Vector3(120.166f, 1535, 0f), 0.2f);
        letter.anchoredPosition = new Vector3(0, -800f, 0f);
        playerInventory.OnQuitQuestBox += playerInventory_OnQuitQuestBox;
        playerSave = PlayerSaveManager.Instance;
        dialogueManager = DialogueManager.Instance;
    }

    private void playerInventory_OnQuitQuestBox(object sender, EventArgs e)
    {
        HideUI();
    }
    public void SetData(QuestScriptableObject questSO){
        questDesc.text = questSO.QuestinMail;
        questSender.text = "- " + questSO.nameSender;
        if(playerSave.GetPlayerLevel() > 1 && playerSave.GetPlayerLevel() < playerSave.GetMaxLevel()){
            giftDesc.text = questSO.GiftDescinMail;
            giftSender.text = "- " + questSO.giftSender;
            UpdateGiftImage();
        }
        
    }
    private void UpdateGiftImage(){
        int giftNow = playerSave.GetPlayerLevel() - 2;
        giftImage[giftNow].SetActive(true);
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
    public void ShowUI_GiftLetter(){
        isGiftLetter = true;
        gameManager.ChangeToCinematic();
        letter.anchoredPosition = new Vector3(0, -800f, 0f);
        SoundManager.Instance.PlayOpenEnvelope();
        LeanTween.move(letter, new Vector3(0, 800f, 0f), 0.2f).setOnComplete(
            ()=> LeanTween.move(giftLetter, new Vector3(120.166f, -241, 0f), 0.8f).setDelay(0.2f).setOnComplete(
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
                ()=> DialogueManager.Instance.ShowDialogue_AfterReadingMainLetter()
            );
        }
        else{
            if(isGiftLetter){
                isGiftLetter =false;
                LeanTween.move(giftLetter, new Vector3(120.166f, 1535, 0f), 0.2f).setOnComplete(
                    ()=> ShowDialogueGift()
                );
            }
            else{
                LeanTween.move(questLetter, new Vector3(93.0238f, 1568.225f, 0f), 0.2f).setOnComplete(
                    ()=> gameManager.ChangeToInGame()
                );
            }
            
        }
        
        letter.anchoredPosition = new Vector3(0, -800f, 0f);
    }

    private void ShowDialogueGift(){
        gameManager.ChangeToCinematic();
        if(playerSave.GetPlayerLevel() == 2){
            dialogueManager.ShowDialogue_Gift(DialogueManager.DialogueNerimaGift.gift1);
        }
        else if(playerSave.GetPlayerLevel() == 3){
            dialogueManager.ShowDialogue_Gift(DialogueManager.DialogueNerimaGift.gift2);
        }
        else if(playerSave.GetPlayerLevel() == 4){
            dialogueManager.ShowDialogue_Gift(DialogueManager.DialogueNerimaGift.gift3);
        }
        else if(playerSave.GetPlayerLevel() == 5){
            dialogueManager.ShowDialogue_Gift(DialogueManager.DialogueNerimaGift.gift4);
        }
        
    }
    

    public bool GetHasCheckFirstTime(){
        return hasCheckFirstTime;
    }
}
