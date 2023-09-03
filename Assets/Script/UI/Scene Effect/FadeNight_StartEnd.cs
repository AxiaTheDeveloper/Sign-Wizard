using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeNight_StartEnd : MonoBehaviour
{
    [SerializeField]private RectTransform nightBG;
    [SerializeField]private PauseUI pause;

    [Header("Fade out")]
    
    [SerializeField]private PlayerSaveManager playerSave;
    [SerializeField]private WitchGameManager gameManager;
    [SerializeField]private DialogueManager dialogueManager;

    [SerializeField]private CanvasGroup nextDay_Text, outsideLight;
    private bool hasFadeNight = false;
    private void Update() {
        if(!hasFadeNight){
            hasFadeNight = true;
            HideUI();
        }

    }

    private void HideUI(){
        nextDay_Text.alpha = 0f;
        if(outsideLight){
            outsideLight.alpha = 0f;
        }
        // Debug.Log("nande");
        if(playerSave.GetIsReset() || playerSave.GetIsSubmitPotion()){
            if(playerSave.GetIsSubmitPotion()){
                playerSave.player_GoOutDialogue_Place();
            }
            LeanTween.alpha(nightBG, 0f, 1.2f).setOnComplete(
                () => StartCoroutine(delayPlayerMove())
            );
        }
        else{
            StartCoroutine(delay());
            LeanTween.alpha(nightBG, 0f, 0.8f).setOnComplete(
                () => StartCoroutine(delayPlayerMove())
            );
        }
    }

    private IEnumerator delayPlayerMove(){
        yield return null;
        if(playerSave.GetIsSubmitPotion()){
            dialogueManager.ShowDialogue_Go_Out_Dialogue();
        
        }
        else{
            //kalo lvl 1 ato 0 trgantung ntr di outside, mainin dialog da da da
            if(playerSave.GetPlayerLevelMode() == levelMode.outside && gameManager.GetOutDoorType() == WitchGameManager.OutDoorType.inFrontOfHouse && playerSave.GetPlayerLevel() == 1){
                dialogueManager.ShowDialogue_Intro();
            }
            else if(playerSave.IsFirstTime_TutorialPuzzle() && gameManager.GetOutDoorType() == WitchGameManager.OutDoorType.magicalBridge)
            {
                TimelineManager.Instance.Start_Tutorials(TimelineManager.TimelineType.TutorialPuzzle);
            }
            else{
                if(playerSave.GetFirstTimeTutorial()){
                    if(gameManager.GetPlace() == WitchGameManager.Place.indoor) dialogueManager.ShowDialogue_Tutorial(DialogueManager.DialogueTutorial.playerTutorialStart);
                    else{
                        gameManager.ChangeToInGame(WitchGameManager.InGameType.normal);
                    }
                    //ntr ini mainin timeline nya ,skrg smntr ini
                    
                }
                else
                {
                    gameManager.ChangeToInGame(WitchGameManager.InGameType.normal);
                }
                
                
            }
            
        }
    }
    private IEnumerator delay(){
        yield return null;
    }
    public void ShowOutsideLight(){
        outsideLight.gameObject.SetActive(true);
        SoundManager.Instance.PlayDoorOpen();
        outsideLight.LeanAlpha(1f, 1.2f).setOnComplete(
            () => playerSave.Go_OutsideNow()
        );
    }

    public void ShowUI(){
        gameManager.ChangeToCinematic();
        LeanTween.alpha(nightBG, 1f, 1.2f).setOnComplete(
            () => nextDay_Text.LeanAlpha(1f, 1f).setOnComplete(
                () => nextDay_Text.LeanAlpha(0f, 0.5f).setOnComplete(
                    () => playerSave.ResetDay_Sleep()
                )
            )
        );
    }
    public void ShowUI_Potion(){
        gameManager.ChangeToCinematic();
        nightBG.gameObject.GetComponent<Image>().color = new Color32(0,6,19,0);
        LeanTween.alpha(nightBG, 1f, 1.2f).setOnComplete(
            () => nextDay_Text.LeanAlpha(1f, 1f).setOnComplete(
                () => nextDay_Text.LeanAlpha(0f, 0.5f).setOnComplete(
                    () => playerSave.ResetDay_SubmitPotion()
                )
            )
        );
    }

    public void ShowUI_Option(){
        gameManager.ChangeToCinematic();
        nightBG.gameObject.GetComponent<Image>().color = new Color32(0,6,19,0);
        LeanTween.alpha(nightBG, 1f, 1.2f).setOnComplete(
            () => pause.GoToMainMenu()
        );
    }


}
