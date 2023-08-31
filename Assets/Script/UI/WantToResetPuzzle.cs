using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WantToResetPuzzle : MonoBehaviour
{
    private bool wantReset = true;
    [SerializeField]private GameObject panelUI;
    [SerializeField]private GameObject arrowYes, arrowNo;
    [SerializeField]private GoingToOtherPlace goingToOtherPlace_SalahSatu;
    private void Awake() 
    {
        panelUI.SetActive(false);
        wantReset = false;
        UpdateArrow();
    }
    public void Change_YesNoTutorial(float input)
    {
        if(input == 1 && wantReset)
        {
            SoundManager.Instance.PlayMenuSound();
            wantReset = false;
            UpdateArrow();
        }
        else if(input == -1 && !wantReset){
            SoundManager.Instance.PlayMenuSound();
            wantReset = true;
            UpdateArrow();
        }
    }
    public void Choose()
    {
        SoundManager.Instance.PlayMenuSound();
        WitchGameManager.Instance.ChangeToCinematic();
        panelUI.SetActive(false);
        if(wantReset)
        {
            DialogueManager.Instance.ShowDialogue_ResetPuzzle();
        }
        else{
            WitchGameManager.Instance.ChangeToInGame(WitchGameManager.InGameType.puzzle);
        }
    }

    private void UpdateArrow()
    {
        arrowYes.SetActive(wantReset);
        arrowNo.SetActive(!wantReset);
    }
    public bool wantToSeeReset()
    {
        return wantReset;
    }
    public void ShowWantReset()
    {
        panelUI.SetActive(true);
    }
}
