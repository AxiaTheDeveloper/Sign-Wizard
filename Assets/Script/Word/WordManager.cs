using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    [SerializeField]private WordSpawner spawner;
    // public List<Word> wordList;
    private bool hasChooseWord;//udah ada kata yang dipilih
    [SerializeField]private Word chosenWord;
    private void Start() {
        createWord();
        hasChooseWord = false;
        // createWord();
    }

    public void createWord(){
        // Word word = new Word(WordGenerator.GetRandomWord(), spawner.SpawnWord());
        chosenWord = new Word(WordGenerator.GetRandomWord(), spawner.SpawnWord());
        // wordList.Add(word); 
    }

    // public void InputLetter(char letter){
    //     if(hasChooseWord){
    //         //cuma si letter itu aja, mungkin di sini bisa ditaro syarat teken backspace gitu untuk cancel(?) we still dont know mo pake yg mn  
            
    //         //check letter selanjutnya
    //         if(chosenWord.GetLetter() == letter){
    //             chosenWord.TypeOutLetter();
    //         }
    //     }  
    //     else{
    //         foreach(Word word in wordList){
    //             if(word.GetLetter() == letter){
    //                 //kalo huruf pertama sama kita pilih kata itu
    //                 hasChooseWord = true;
    //                 chosenWord = word;
    //                 word.TypeOutLetter();
    //                 break;
    //             }
    //         }
    //     }
    //     if(hasChooseWord && chosenWord.AllTyped()){
    //         hasChooseWord = false;
    //         wordList.Remove(chosenWord);
    //         //kalo mo itu palingan haschoosword diapus trus tinggal kek abis yg ini abis, chosenword = word selanjutnya d list, ato pake queue kali ya hem, jd tiap create word tuh, create gituh, apa kita gameobject d tmpt lain poolnya hem, hem, well ga si ribet mending d sini 
    //         createWord();
    //     }
    //     //mungkin di sini kirim kalo, haschooseword false brarti lsg false, tp kalo haschoosword true tp itu false, tp berarti perlu tanda apakah yglain ada haschooseword ato ga
    // }
    public bool InputFirstLetter(char letter){
        
        if(chosenWord.GetLetter() == letter){
            //kalo huruf pertama sama kita pilih kata itu
            hasChooseWord = true;
            chosenWord.TypeOutLetter();
            return true;
        }
            
        return false;
        
        //mungkin di sini kirim kalo, haschooseword false brarti lsg false, tp kalo haschoosword true tp itu false, tp berarti perlu tanda apakah yglain ada haschooseword ato ga
    }
    public bool checkerAdaYangSama(char letter){
        return hasChooseWord && chosenWord.GetLetter() == letter;
    }
    public bool InputLetters(char letter){
        //ini kalo ud ada has chooseword
        
        if(hasChooseWord && chosenWord.GetLetter() == letter){
            
            chosenWord.TypeOutLetter();
            return true;
        }
        return false;
        

    }
    public bool InputLetter(char letter){
        //ini kalo ud ada has chooseword
        
        if(hasChooseWord && chosenWord.GetLetter() == letter){
            chosenWord.TypeOutLetter();
        }
        if(hasChooseWord && chosenWord.AllTyped()){
            // wordList.Remove(chosenWord);
            
            //kalo mo itu palingan haschoosword diapus trus tinggal kek abis yg ini abis, chosenword = word selanjutnya d list, ato pake queue kali ya hem, jd tiap create word tuh, create gituh, apa kita gameobject d tmpt lain poolnya hem, hem, well ga si ribet mending d sini 
            
            //do this and this and this trgantung yg kepasang ini.
            hasChooseWord = false;
            createWord();
            return false;
        }
        return true;

    }
    public void CancelInputLetter(){
        if(hasChooseWord){
            Debug.Log(chosenWord.word);
            chosenWord.CancelTypedOut();
            hasChooseWord = false;
        }
    }
}
