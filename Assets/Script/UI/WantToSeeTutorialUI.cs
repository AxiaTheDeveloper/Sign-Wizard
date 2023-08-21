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
            wantTutorial = false;
            UpdateArrow();
        }
        else if(input == -1 && !wantTutorial){
            
            wantTutorial = true;
            UpdateArrow();
        }
    }
    public void Choose()
    {
        WitchGameManager.Instance.ChangeToCinematic();
        if(!wantTutorial)
        {
            saveManager.ChangeFirstTimeTutorial();
        }
        panelUI.SetActive(false);
        door.PlayDoorOpenn();
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
