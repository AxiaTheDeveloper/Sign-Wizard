using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordInput : MonoBehaviour
{
    [SerializeField]private WordManager wordManager;
    private void Update() {
        foreach(char letter in Input.inputString){
            wordManager.InputLetter(letter);
        }
        if(Input.GetKey(KeyCode.Escape)){
            wordManager.CancelInputLetter();
        }
    }
}
