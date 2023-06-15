using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordUI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI textNow;
    [SerializeField]private string colorText, wrongColorText;
    private string textFull;

    public void SetWord(string word){
        textNow.text = word;
        textFull = word;
        textNow.color = Color.white;
        
    }
    public void ChangeColorLetterUI(int position){
        // saveTextA += textFull[position];
        string saveTextA = "", saveTextB = "", saveText = "";
        for(int i=0;i<position+1;i++){
            saveTextA += textFull[i];
        }
        // Debug.Log(saveTextA);
        for(int i=position+1;i<textFull.Length;i++){
            saveTextB += textFull[i];
        }
        // Debug.Log(saveTextB);
        
        saveText = colorText + saveTextA + "</color>" + saveTextB;
        textNow.text = saveText;
    }

    public void ChangeWrongColorUI(int position){
        // saveTextA += textFull[position];
        string saveTextA = "", saveTextB = "", saveText = "", saveWrongText = "";
        for(int i=0;i<position;i++){
            saveTextA += textFull[i];
        }
        saveWrongText += textFull[position];
        Debug.Log(saveTextA);
        for(int i=position+1;i<textFull.Length;i++){
            saveTextB += textFull[i];
        }
        Debug.Log(saveWrongText);
        Debug.Log(saveTextB);
        
        saveText = colorText + saveTextA + wrongColorText + saveWrongText + "</color>" + "</color>" + saveTextB;
        textNow.text = saveText;
    }
    public void RemoveWord(){
        Destroy(gameObject);
    }
}
