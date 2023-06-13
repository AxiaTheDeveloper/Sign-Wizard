using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleChest : MonoBehaviour
{
    [SerializeField]private GameObject UI;
    [SerializeField]private GameObject wordThings;
    private WitchGameManager gameManager;

    private void Awake() {
        gameManager = WitchGameManager.Instance;
    }

    private void Start() {
        WordInput.Instance.OnQuitInterface += wordInput_OnQuitInterface;
    }

    private void wordInput_OnQuitInterface(object sender, EventArgs e)
    {
        CloseUI();
    }


    public void ShowUI(){
        UI.SetActive(true);
        wordThings.SetActive(true);
        gameManager.ChangeInterfaceType(0);
        // change game state
    }
    public void CloseUI(){
        UI.SetActive(false);
        wordThings.SetActive(false);
        gameManager.ChangeToInGame();
        //change game state
        //di sini delete semua gameobject ???
    }
}
