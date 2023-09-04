using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelingMerchant : MonoBehaviour
{
    private bool isFirstTimeChat = true;
    [SerializeField]private GameObject BG, charaImage, dialogue, nameChara;
    
    [SerializeField]private GameObject yesNoQuestion;
    [SerializeField]private GameObject Selected_Yes, Selected_No;
    [SerializeField]private WitchGameManager gameManager;
    private bool wantToGoHome, isSubmitButton;  
    [SerializeField]private GoingToOtherPlace goingToOtherPlace;
    [SerializeField]private PlayerInventory playerInventory;

    private void Start() 
    {
        playerInventory.OnQuitTravelingMerchant += playerInventory_OnQuitTravelingMerchant;
        playerInventory.OnSubmitTravelingMerchant += playerInventory_OnSubmitTravelingMerchant;
        BG.SetActive(false);
        isSubmitButton = false;
        wantToGoHome = false;
        Selected_On();
        yesNoQuestion.SetActive(false);
        charaImage.SetActive(false);
        nameChara.SetActive(false);
        dialogue.SetActive(false);
    }

    private void playerInventory_OnSubmitTravelingMerchant(object sender, EventArgs e)
    {
        if(wantToGoHome)
        {
            PauseUI.Instance.SaveData();
            HideDialogue();
            goingToOtherPlace.StraightToHome();
        }
        else{
            HideDialogue();
        }
    }

    private void playerInventory_OnQuitTravelingMerchant(object sender, EventArgs e)
    {
        HideDialogue();
    }

    public void ChatWithMerchant()
    {
        if(PlayerSaveManager.Instance.GetPlayerLevel() == 1)
        {
            if(isFirstTimeChat)
            {
                isFirstTimeChat = false;
                DialogueManager.Instance.ShowDialogue_ChatWithTravelingMerchant1();
            }
            else
            {
                ChatGoHome();
            }
        }
        else
        {
            ChatGoHome();
        }
    }

    private void Selected_On()
    {
        Selected_Yes.SetActive(wantToGoHome);
        Selected_No.SetActive(!wantToGoHome);
    }
    private IEnumerator dialogueSequence()
    {
        gameManager.ChangeToCinematic();
        dialogue.SetActive(true);
        DialogueSystem.DialogueLine line = dialogue.GetComponent<DialogueSystem.DialogueLine>();
        line.GoLineText();
        yield return new WaitUntil(()=> line.finished);

        dialogue.SetActive(true);
        BG.SetActive(true);
        charaImage.SetActive(true);
        nameChara.SetActive(true);
        gameManager.ChangeInterfaceType(WitchGameManager.InterfaceType.InterfaceTravelingMerchant);
        line.ChangeFinished_false();
        yesNoQuestion.SetActive(true);

        yield return new WaitUntil(()=> isSubmitButton);
        dialogue.SetActive(false);

    }

    public void ChatGoHome()
    {
        yesNoQuestion.SetActive(false);
        BG.SetActive(true);

        StartCoroutine(dialogueSequence());
    }
    public void HideDialogue()
    {
        BG.SetActive(false);
        isSubmitButton = false;
        wantToGoHome = false;
        Selected_On();
        yesNoQuestion.SetActive(false);
        charaImage.SetActive(false);
        nameChara.SetActive(false);

        dialogue.SetActive(false);
        // gameObject.SetActive(false);
        if(!gameManager.IsInGame()){
            gameManager.ChangeToInGame(WitchGameManager.InGameType.normal);
        }
    }

    public void Change_YesNo()
    {
        wantToGoHome = !wantToGoHome;
        Selected_On();
    }
    public bool WantToGoHome()
    {
        return wantToGoHome;
    }
}
