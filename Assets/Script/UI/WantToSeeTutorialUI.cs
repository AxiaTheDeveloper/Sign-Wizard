using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WantToSeeTutorialUI : MonoBehaviour
{
    private bool wantTutorial = true;
    [SerializeField]private GameObject panelUI;
    [SerializeField]private GameObject arrowYes, arrowNo;
    [SerializeField]private PlayerSaveManager saveManager;
    [SerializeField]private Door_Outside door;
    [SerializeField]private GoingToOtherPlace goingToOtherPlace;
    private void Awake() 
    {
        panelUI.SetActive(false);
        wantTutorial = true;
        UpdateArrow();
    }
    public void Change_YesNoTutorial(float input)
    {
        if(input == 1 && wantTutorial)
        {
            SoundManager.Instance.PlayMenuSound();
            wantTutorial = false;
            UpdateArrow();
        }
        else if(input == -1 && !wantTutorial){
            SoundManager.Instance.PlayMenuSound();
            wantTutorial = true;
            UpdateArrow();
        }
    }
    public void Choose()
    {
        SoundManager.Instance.PlayMenuSound();
        WitchGameManager gameManager = WitchGameManager.Instance;
        gameManager.ChangeToCinematic();
        if(!wantTutorial)
        {
            if(gameManager.GetOutDoorType() == WitchGameManager.OutDoorType.inFrontOfHouse)
            {
                saveManager.ChangeFirstTimeTutorial();
            }
            else if(gameManager.GetOutDoorType() == WitchGameManager.OutDoorType.forest)
            {
                saveManager.ChangeFirstTimeTutorialPuzzle();
            }
        }
        panelUI.SetActive(false);
        if(gameManager.GetOutDoorType() == WitchGameManager.OutDoorType.inFrontOfHouse)
        {
            door.PlayDoorOpenn();
        }
        else if(gameManager.GetOutDoorType() == WitchGameManager.OutDoorType.forest)
        {
            goingToOtherPlace.FadeGetOutScene();
        }
        
    }

    private void UpdateArrow()
    {
        arrowYes.SetActive(wantTutorial);
        arrowNo.SetActive(!wantTutorial);
    }
    public bool wantToSeeTutorial()
    {
        return wantTutorial;
    }
    public void ShowWantTutorial()
    {
        panelUI.SetActive(true);
    }
}
