using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WordInput : MonoBehaviour
{
    public static WordInput Instance;
    [SerializeField]private WordManager[] wordManager;
    [SerializeField]private List<int> chosenWordsList;
    public event EventHandler OnQuitInterface;

    private WitchGameManager gameManager;
    [SerializeField]private GameInput gameInput;

    
    private bool hasChosenWords, YesWordSame;//no word same di cek kedua
    private bool isOnlyOneWord, hasSelectedWord;
    private int choice;

    
    private void Awake() {
        Instance = this;
        gameManager = WitchGameManager.Instance;

    }
    
    private void Start() {
        hasChosenWords = false;
        // isOnlyOneWord = false;
        // hasSelectedWord = false;
        // YesWordSame = false;
    }
    private void Update() {
        if(gameManager.IsInterfaceType() == 5){
            if(Input.anyKeyDown){
                foreach(char letter in Input.inputString){
                    //kalo one word only butuh penanda yg mana yg dipilih dan apakah ud dipilih dr inventory ui nya
                    if(!hasChosenWords){
                        for(int i=0;i<wordManager.Length;i++){     
                            if(wordManager[i].InputFirstLetter(letter)){

                                chosenWordsList.Add(i);
                                hasChosenWords = true;
                            }
                        }
                    }

                    else{
                        if(chosenWordsList.Count > 1){
                            List<int> chosenWordsCopy = new List<int>(chosenWordsList); // Create a copy of the list

                            foreach (int i in chosenWordsCopy)
                            {
                                // Debug.Log(i);
                                YesWordSame = wordManager[i].checkerAdaYangSama(letter);
                                if(YesWordSame){
                                    break;
                                }
                                // Debug.Log(YesWordSame);
                            }
                            if(YesWordSame){
                                // Debug.Log("Dihapus");
                                foreach (int i in chosenWordsCopy)
                                {
                                    // Debug.Log(i);
                                    if (wordManager[i].InputLetters(letter))
                                    {
                                        // chosenWordsList.Add(i);
                                        hasChosenWords = true;
                                        
                                    }
                                    else
                                    {
                                        chosenWordsList.Remove(i);
                                        wordManager[i].CancelInputLetter();
                                        // hasChosenWords = false;
                                    }
                                }
                            }
                        }
                        else if(chosenWordsList.Count == 1){
                            if(!wordManager[chosenWordsList[0]].InputLetter(letter)){
                                hasChosenWords = false;
                                chosenWordsList.Clear();
                            }
                        }
                        else if(chosenWordsList.Count < 1){
                            hasChosenWords = false;
                        }
                        
                    }
                }
            }
            
            if(gameInput.GetInputCancelInputLetter()){
                UndoInputLetterManyWords();
            }
            if(gameInput.GetInputEscape()){
                UndoInputLetterManyWords();
                OnQuitInterface?.Invoke(this,EventArgs.Empty);
            }
        }
        
    }

    private void UndoInputLetterManyWords(){
        foreach(WordManager wordMn in wordManager){
            wordMn.CancelInputLetter();
            chosenWordsList.Clear();
            hasChosenWords = false;
        }
    }
    private void UndoInputLetterOneWord(int choice){
        wordManager[choice].CancelInputLetter();
        // chosenWordsList.Clear();
        hasChosenWords = false;
    }
    public void ChangeisOnlyOneWord(bool choice){
        isOnlyOneWord = choice;
    }
    public void HasSelectedWord(int selectItem){
        choice = selectItem;
    }
    public void GetWordManager(WordManager[] wordManagers){
        wordManager = wordManagers;
    }
}
