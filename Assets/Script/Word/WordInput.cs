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
    private bool isUIFirstTimeShowing;
    private int choice;

    private bool adaWord;

    
    private void Awake() {
        Instance = this;
        gameManager = WitchGameManager.Instance;
        adaWord = true;

    }
    
    private void Start() {
        hasChosenWords = false;
        isUIFirstTimeShowing = true;
        // isOnlyOneWord = false;
        // hasSelectedWord = false;
        // YesWordSame = false;
    }
    private void Update() {
        if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.InventoryAndChest || gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.CauldronFire || gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.TumbukTime){
            if(Input.anyKeyDown && !gameInput.GetInputCancelInputLetter()){
                foreach(char letter in Input.inputString){
                    //kalo one word only butuh penanda yg mana yg dipilih dan apakah ud dipilih dr inventory ui nya
                    // Debug.Log(letter);
                    
                    if(!hasChosenWords && !isUIFirstTimeShowing){
                        if(wordManager.Length == 1){
                            if(adaWord){
                                if(!wordManager[0].InputLetter_OnlyOneWordManager(letter)){
                                    hasChosenWords = false;
                                    chosenWordsList.Clear();
                                }
                            }
                        }
                        else{
                            for(int i=0;i<wordManager.Length;i++){
                                if(adaWord){
                                    if(wordManager[i].InputFirstLetter(letter)){

                                        chosenWordsList.Add(i);
                                        hasChosenWords = true;
                                    }
                                }
                                    
        
                                
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
            if(isUIFirstTimeShowing){
                isUIFirstTimeShowing = false; // biar input buat nyalain invent dkk ga masuk;
            }
            if(gameInput.GetInputCancelInputLetter()){
                UndoInputLetterManyWords();
                
                
            }
            if(gameInput.GetInputEscape()){
                isUIFirstTimeShowing = true;
                UndoInputLetterManyWords();
                OnQuitInterface?.Invoke(this,EventArgs.Empty);
            }
        }
        
    }

    public void UndoInputLetterManyWords(){
        
        foreach(WordManager wordMn in wordManager){
            // Debug.Log("????");
            if(wordManager.Length == 1){
                wordMn.CancelInputLetter_OnlyOneWordManager();
            }
            else{
                wordMn.CancelInputLetter();
            }
            
        }

        chosenWordsList.Clear();
        hasChosenWords = false;
    }


    public void GetWordManager(WordManager[] wordManagers){
        // Debug.Log(wordManagers[0]);
        wordManager = wordManagers;
    }
    public void OpenUI(){
        isUIFirstTimeShowing = true;
    }

    //khusus chest doang
    public void ChangeAdaWord(bool change){
        adaWord = change;
    }
}
