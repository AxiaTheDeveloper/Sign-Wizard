using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Door_Outside : MonoBehaviour
{
        // Start is called before the first frame update
    [SerializeField]private GameObject BG, charaImage, dialogue, nameChara;
    [SerializeField]private CanvasGroup darkBG_effect;
    
    [SerializeField]private GameObject yesNoQuestion;
    [SerializeField]private GameObject Selected_Yes, Selected_No;
    [SerializeField]private WitchGameManager gameManager;
    [SerializeField]private PlayerInventory playerInventory;
    [SerializeField]private PlayerSaveManager playerSave;
    [SerializeField]private FadeNight_StartEnd fadeNight;
    
    [field : TextArea]
    [SerializeField]private string Go_outside_dialogue, Go_inside_dialogue;
    

    private bool wantTo_GoIn, isSubmitButton; // kalo reset change position player ke bed abis restart scene
    private void Start(){
        if(gameManager.GetPlace() == WitchGameManager.Place.outdoor){
            darkBG_effect.alpha = 0f;
        }
        
        playerInventory.OnQuitDoor += playerInventory_OnQuitDoor;
        playerInventory.OnSubmitDoor += playerInventory_OnSubmitDoor;
        BG.SetActive(false);
        isSubmitButton = false;
        wantTo_GoIn = false;
        Selected_On();
        nameChara.SetActive(false);
        yesNoQuestion.SetActive(false);
        charaImage.SetActive(false);

        dialogue.SetActive(false);

    }

    private void playerInventory_OnSubmitDoor(object sender, EventArgs e)
    {
        isSubmitButton = true;
        
        if(!wantTo_GoIn){
            HideDialogue();
        }
        else{
            HideDialogue();
            gameManager.ChangeToCinematic();
            if(gameManager.GetPlace() == WitchGameManager.Place.indoor){
                fadeNight.ShowOutsideLight();
            }
            else if(gameManager.GetPlace() == WitchGameManager.Place.outdoor){
                SoundManager.Instance.PlayDoorOpen();
                darkBG_effect.LeanAlpha(1f, 1.2f).setOnComplete(
                    () => playerSave.Go_InsideNow()
                );
            }
            
        
        }
    }

    private void playerInventory_OnQuitDoor(object sender, EventArgs e)
    {
        HideDialogue();
    }

    private void Selected_On(){
        if(wantTo_GoIn){
            Selected_Yes.SetActive(true);
            Selected_No.SetActive(false);
        }
        else{
            Selected_Yes.SetActive(false);
            Selected_No.SetActive(true);
        }
        
    }
    private IEnumerator dialogueSequence(){
        gameManager.ChangeToCinematic();
        
        dialogue.SetActive(true);
        DialogueSystem.DialogueLine line = dialogue.GetComponent<DialogueSystem.DialogueLine>();
        if(gameManager.GetPlace() == WitchGameManager.Place.indoor){
            line.ChangeInputText(Go_outside_dialogue);
        }
        else if(gameManager.GetPlace() == WitchGameManager.Place.outdoor){
            line.ChangeInputText(Go_inside_dialogue);
        }
        line.GoLineText();
        yield return new WaitUntil(()=> line.finished);
        // Debug.Log(line.finished);
        gameManager.ChangeInterfaceType(WitchGameManager.InterfaceType.InterfaceDoor);
        line.ChangeFinished_false();
        yesNoQuestion.SetActive(true);

        yield return new WaitUntil(()=> isSubmitButton);
        dialogue.SetActive(false);

    }

    public void ShowDialogue(){
        yesNoQuestion.SetActive(false);
        BG.SetActive(true);
        // charaImage.SetActive(true);
        // gameObject.SetActive(true);
        StartCoroutine(dialogueSequence());
    }
    public void HideDialogue(){
        BG.SetActive(false);
        isSubmitButton = false;
        wantTo_GoIn = false;
        Selected_On();
        yesNoQuestion.SetActive(false);
        charaImage.SetActive(false);
        nameChara.SetActive(false);
        // Debug.Log(charaImage.activeSelf);
        dialogue.SetActive(false);
        // gameObject.SetActive(false);
        if(!gameManager.IsInGame()){
            gameManager.ChangeToInGame();
        }
    }

    public void Change_YesNo(){
        wantTo_GoIn = !wantTo_GoIn;
        Selected_On();
    }
    public bool GetWantToGoIn(){
        return wantTo_GoIn;
    }

}
