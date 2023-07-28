using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//pop up yg muncul kalo misal si player ud deket dn bs interact misal - press z to interact
public class PopUpInteractUI : MonoBehaviour
{
    [SerializeField]private InteractObject interactObject;
    [SerializeField]private GameObject popUpUImessage;
    private void Start() {
        PlayerInteraction.Instance.OnSelectedInteractObject += playerInteraction_OnSelectedInteractObject;
        HidePopUp();
    }
    private void Update() {
        if(popUpUImessage.activeSelf && WitchGameManager.Instance.isCinematic()){
            popUpUImessage.SetActive(false);
        }
    }

    private void playerInteraction_OnSelectedInteractObject(object sender, PlayerInteraction.OnSelectedInteractObjectEventArgs e)
    {
        if(e.selectedObject == interactObject){
            ShowPopUp();
        }
        else{
            HidePopUp();
        }
        
    }

    private void ShowPopUp(){
        popUpUImessage.SetActive(true);
    }
    private void HidePopUp(){
        popUpUImessage.SetActive(false);
    }
}
