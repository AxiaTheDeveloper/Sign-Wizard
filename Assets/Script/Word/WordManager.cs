using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    [SerializeField]private WordSpawner spawner;
    // public List<Word> wordList;
    private bool hasChooseWord, hasWord;//udah ada kata yang dipilih, has word buat ngecek chosenWord ud ada isi ato blom sbnrnya biar ga crash aja pas input pertama dn blm create word
    [SerializeField]private FinishWordDoFunction function;
    [SerializeField]private Word chosenWord;
    private enum WordType{
        SameWord, DifferentWord
    }
    [SerializeField]private WordType wordType;
    
    private enum TemplateType{
        SameTemplate, SpawnTemplate
    } 
    [SerializeField]private TemplateType templateType;
    



    [SerializeField]private string theWord;
    private string choseWord;
    [SerializeField]private WordUI wordDisplay;

    [SerializeField]private float delayedSpawnWord_Time = 0.2f;

    [SerializeField]private WordGenerator wordGenerator;
    private void Start() {
        hasWord = false;
        hasChooseWord = false;
        
        
        createWord();
        
    }

    public void createWord(){
        // Word word = new Word(WordGenerator.GetRandomWord(), spawner.SpawnWord());
        hasWord = true;

        if(wordType == WordType.SameWord){
            choseWord = theWord;
        }
        else if(wordType == WordType.DifferentWord){
            choseWord = wordGenerator.GetRandomWord();
        }

        if(templateType == TemplateType.SameTemplate){

            if(wordDisplay == null){
                wordDisplay =  spawner.SpawnWord();
                // Debug.Log("harusnya sekali aja");
            }
            else{

                chosenWord = new Word(choseWord, wordDisplay, true);

                // Debug.Log(chosenWord);
            }
        }
        else if(templateType == TemplateType.SpawnTemplate){
            chosenWord = new Word(choseWord, spawner.SpawnWord(), false);
        }
        // wordList.Add(word); 
    }
    public void changeTheWord(string Word){
        if(theWord != Word){
            theWord = Word;
            createWord();
            
        }
        
    }


    public bool InputFirstLetter(char letter){
        // Debug.Log(chosenWord.GetLetter() + " " + letter);
        if(hasWord && chosenWord.GetLetter() == letter){

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
        chosenWord.WrongLetter();
        return false;
    }
    public bool InputLetter_OnlyOneWordManager(char letter){
        if(chosenWord.GetLetter() == letter){

            chosenWord.TypeOutLetter();
        }
        else{
            chosenWord.WrongLetter();
        }
        if(chosenWord.AllTyped()){
            

            function.WordFinisheds();
            hasChooseWord = false;
            hasWord = false;

            StartCoroutine(DelayedCreateWord());

            //di sini suruh kerjain apa gitu
            return false;
        }
        return true;
    }

    public bool InputLetter(char letter){
        //ini kalo ud ada has chooseword

        if(hasChooseWord && chosenWord.GetLetter() == letter){

            chosenWord.TypeOutLetter();
        }
        else{
            chosenWord.WrongLetter();
        }
        if(hasChooseWord && chosenWord.AllTyped()){
            

            function.WordFinisheds();
            hasChooseWord = false;
            hasWord = false;

            StartCoroutine(DelayedCreateWord());

            //di sini suruh kerjain apa gitu
            return false;
        }
        return true;

    }
    private IEnumerator DelayedCreateWord(){

        yield return new WaitForSeconds(delayedSpawnWord_Time);

        createWord();
    }

    public void CancelInputLetter(){
        if(hasChooseWord){
            // Debug.Log(chosenWord.word);
            chosenWord.CancelTypedOut();
            hasChooseWord = false;
        }
    }
}
