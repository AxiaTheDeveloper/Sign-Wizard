using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Word 
{
    public string word;
    private int no_Letter;
    private WordUI display;
    private bool isSameTemplate, isAlreadyWrong, isAlreadyWrong_Reset;//already wrong reset cuma buat kalo wordmanager byk aja jadinya ga ngestuck di 1 orang
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
        isAlreadyWrong_Reset = false;
        display.ChangeColorLetterUI(no_Letter);
        
        
        no_Letter++;
        
        //hilangin kata di screen atau nanti katanya berubah warna aja kali
    }

    public void WrongLetter(){
        if(!isAlreadyWrong){
            isAlreadyWrong_Reset = false;
            isAlreadyWrong = true;
            if(no_Letter <= word.Length){
                display.ChangeWrongColorUI(no_Letter);
            }

        }
        else{
            isAlreadyWrong_Reset = true;
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
    public bool GetIsAlreadyWrong_Reset()
    {
        return isAlreadyWrong_Reset;
    }
}
