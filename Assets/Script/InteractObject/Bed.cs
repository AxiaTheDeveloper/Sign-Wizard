using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bed : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private GameObject BG, charaImage, dialogue, nameChara;
    
    [SerializeField]private GameObject yesNoQuestion;
    [SerializeField]private GameObject Selected_Yes, Selected_No;
    [SerializeField]private WitchGameManager gameManager;
    [SerializeField]private FadeNight_StartEnd fadeNight;
    [SerializeField]private PlayerInventory playerInventory;

    private bool isResetDay, isSubmitButton; // kalo reset change position player ke bed abis restart scene
    private void Start()
    {
        playerInventory.OnQuitBed += playerInventory_OnQuitBed;
        playerInventory.OnSubmitBed += playerInventory_OnSubmitBed;
        BG.SetActive(false);
        isSubmitButton = false;
        isResetDay = false;
        Selected_On();
        yesNoQuestion.SetActive(false);
        charaImage.SetActive(false);
        nameChara.SetActive(false);
        dialogue.SetActive(false);

    }

    private void playerInventory_OnSubmitBed(object sender, EventArgs e)
    {
        isSubmitButton = true;
        
        if(!isResetDay){
            HideDialogue();
        }
        else{
            HideDialogue();
            PlayerSaveManager.Instance.resetDay();
            fadeNight.ShowUI();
        }
    }

    private void playerInventory_OnQuitBed(object sender, EventArgs e)
    {
        HideDialogue();
    }

    private void Selected_On()
    {
        if(isResetDay)
        {
            Selected_Yes.SetActive(true);
            Selected_No.SetActive(false);
        }
        else
        {
            Selected_Yes.SetActive(false);
            Selected_No.SetActive(true);
        }
        
    }
    private IEnumerator dialogueSequence()
    {
        gameManager.ChangeToCinematic();
        dialogue.SetActive(true);
        DialogueSystem.DialogueLine line = dialogue.GetComponent<DialogueSystem.DialogueLine>();
        line.GoLineText();
        yield return new WaitUntil(()=> line.finished);
        BG.SetActive(true);
        charaImage.SetActive(true);
        nameChara.SetActive(true);
        gameManager.ChangeInterfaceType(WitchGameManager.InterfaceType.InterfaceBed);
        line.ChangeFinished_false();
        yesNoQuestion.SetActive(true);

        yield return new WaitUntil(()=> isSubmitButton);
        dialogue.SetActive(false);

    }

    public void ShowDialogue()
    {
        yesNoQuestion.SetActive(false);
        BG.SetActive(true);

        StartCoroutine(dialogueSequence());
    }
    public void HideDialogue()
    {
        BG.SetActive(false);
        isSubmitButton = false;
        isResetDay = false;
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
        isResetDay = !isResetDay;
        Selected_On();
    }
    public bool GetIsResetDay()
    {
        return isResetDay;
    }
}
