using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleChest : MonoBehaviour
{
    [SerializeField]private GameObject UI;
    [SerializeField]private GameObject wordThings;

    private void Update() {
        if(GameInput.Instance.GetInputEscape() && UI.activeSelf){
            CloseUI();
        }
    }
    public void ShowUI(){
        UI.SetActive(true);
        wordThings.SetActive(true);
        WitchGameManager.Instance.ChangeToInterface();
        // change game state
    }
    public void CloseUI(){
        UI.SetActive(false);
        wordThings.SetActive(false);
        WitchGameManager.Instance.ChangeToInGame();
        //change game state
        //di sini delete semua gameobject ???
    }
}
