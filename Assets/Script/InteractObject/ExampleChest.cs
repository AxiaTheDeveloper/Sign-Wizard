using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleChest : MonoBehaviour
{
    [SerializeField]private GameObject wordUI;
    [SerializeField]private WordInput wordInput;
    [SerializeField]private WordManager[] wordManager;
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
        wordInput.GetWordManager(wordManager);
        wordInput.ChangeisOnlyOneWord(false);
        wordUI.SetActive(true);
        gameManager.ChangeInterfaceType(0);
        // change game state
    }
    public void CloseUI(){
        wordUI.SetActive(false);
        gameManager.ChangeToInGame();
        //change game state
        //di sini delete semua gameobject ???
    }
}
