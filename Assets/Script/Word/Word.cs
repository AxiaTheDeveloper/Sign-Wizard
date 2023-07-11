using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Word 
{
    public string word;
    private int no_Letter;
    private WordUI display;
    private bool isSameTemplate, isAlreadyWrong;
    public Word (string chosenWord, WordUI freeDisplay, bool GetisSameTemplate){
        word = chosenWord;
        
        no_Letter = 0;
        display = freeDisplay;
        display.SetWord(chosenWord);
        isSameTemplate = GetisSameTemplate;
    }
    public char GetLetter(){
        return word[no_Letter];
    }
    // public void RightGiveColor(){
    //     no_Letter 
    // }
    public void TypeOutLetter(){
        display.ChangeColorLetterUI(no_Letter);
        
        no_Letter++;
        
        //hilangin kata di screen atau nanti katanya berubah warna aja kali
    }

    public void WrongLetter(){
        if(!isAlreadyWrong){
            
            isAlreadyWrong = true;
            display.ChangeWrongColorUI(no_Letter);
        }
        else{
            CancelTypedOut();
        }
        
    }
    public void CancelTypedOut(){
        isAlreadyWrong = false;
        no_Letter = 0;
        display.SetWord(word);
        // Debug.Log(no_Letter);
        //semua kata dibalikkin ke warna semula
    }
    public bool AllTyped(){
        if(no_Letter >= word.Length){
            if(!isSameTemplate){
                display.RemoveWord();
            }   
            
        }
        return no_Letter >= word.Length;
    }
}
