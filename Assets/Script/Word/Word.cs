using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Word 
{
    public string word;
    private int no_Letter;
    private WordUI display;
    private bool isSameTemplate;
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
        display.ChangeWrongColorUI(no_Letter);
    }
    public void CancelTypedOut(){
        no_Letter = 0;
        display.SetWord(word);
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
