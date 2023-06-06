using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Word 
{
    public string word;
    private int no_Letter;
    private WordUI display;
    public Word (string chosenWord, WordUI freeDisplay){
        word = chosenWord;
        no_Letter = 0;
        display = freeDisplay;
        display.SetWord(chosenWord);
    }
    public char GetLetter(){
        return word[no_Letter];
    }
    public void TypeOutLetter(){
        no_Letter++;
        display.RemoveLetterUI();
        //hilangin kata di screen atau nanti katanya berubah warna aja kali
    }
    public void CancelTypedOut(){
        no_Letter = 0;
        display.SetWord(word);
        //semua kata dibalikkin ke warna semula
    }
    public bool AllTyped(){
        if(no_Letter >= word.Length){
            display.RemoveWord();
        }
        return no_Letter >= word.Length;
    }
}
