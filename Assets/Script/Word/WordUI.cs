using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordUI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI textNow;
    public void SetWord(string word){
        textNow.text = word;
        textNow.color = Color.white;
    }
    public void RemoveLetterUI(){
        textNow.text = textNow.text.Remove(0,1);
        textNow.color = Color.yellow;
    }
    public void RemoveWord(){
        Destroy(gameObject);
    }
}
