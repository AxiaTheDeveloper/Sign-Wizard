using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpInteractUI : MonoBehaviour
{
    [SerializeField]private InteractObject interactObject;
    [SerializeField]private GameObject popUpUImessage;
    private void Start() {
        PlayerInteraction.Instance.OnSelectedInteractObject += playerInteraction_OnSelectedInteractObject;
        HidePopUp();
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
