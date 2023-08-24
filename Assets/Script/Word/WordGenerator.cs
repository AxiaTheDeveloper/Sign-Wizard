using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGenerator : MonoBehaviour
{   
    private string[] wordArrayTumbuk = {"abc", "def", "ghi", "jkl", "mno", "pqr", "stu", "vwx", "yzc", "bde", "fgh", "ijk", "lmn", "opq", "rst", "uvw", "xyz", "acd", "efg", "hij", "klm", "nop", "qrs", "tuv", "wxy", "zab", "cde", "fgh", "ijk", "lmn", "opq", "rst", "uvw", "xyz", "yza", "bcd", "efg", "hij", "klm", "nop", "qrs", "tuv", "wxy", "zab", "cde", "fgh", "ijk", "lmn", "opq", "rst"};
    private string[] wordArrayKompor = {"a", "i", "u", "e", "o"};
    private string[] wordArrayPuzzleToTown = {"a", "b", "c", "d", "e"};
    // private static string[] wordArray = {"adil", "apa", "asik"};
    public enum TypeWord{
        tumbuk, kompor, PuzzleToTown
    }
    [SerializeField]private TypeWord typeWord;
    private string[] wordArray;
    
    private int randChecker, sameChecker;
    private bool firstTime = true;
    private bool foundDifferent = false;
    public string GetRandomWord(){
        

        if(typeWord == TypeWord.tumbuk){
            wordArray = wordArrayTumbuk;
        }
        else if(typeWord == TypeWord.kompor){
            wordArray = wordArrayKompor;
        }
        else if(typeWord == TypeWord.PuzzleToTown){
            wordArray = wordArrayPuzzleToTown;
        }

        int random = Random.Range(0,wordArray.Length);
        
        //ngecek biar random ga dapet huruf sama lagi sebanyak sameChecker (2 kali utk skrg)
        if(firstTime){
            firstTime = false;
            randChecker = random;
            sameChecker = 0;
            Debug.Log(wordArray[random] + "checker" + randChecker);
        }
        else{
            foundDifferent = false;
            while(!foundDifferent){
                if(random == randChecker)
                {
                    random = Random.Range(0,wordArray.Length);
                    sameChecker++;
                }
                if(random != randChecker)
                {
                    foundDifferent = true;
                }
                
            }
            Debug.Log(wordArray[random] + "checker" + randChecker);
        }
        if(sameChecker == 1){
            firstTime = true;
            sameChecker = 0;
        }
        
        // Debug.Log(random);
        string chosenWord = wordArray[random];

        return chosenWord;
    }
}
