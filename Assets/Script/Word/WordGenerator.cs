using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGenerator : MonoBehaviour
{   
    private string[] wordArrayTumbuk = {"abc", "def", "ghi", "jkl", "mno", "pqr", "stu", "vwx", "yzc", "bde", "fgh", "ijk", "lmn", "opq", "rst", "uvw", "xyz", "acd", "efg", "hij", "klm", "nop", "qrs", "tuv", "wxy", "zab", "cde", "fgh", "ijk", "lmn", "opq", "rst", "uvw", "xyz", "yza", "bcd", "efg", "hij", "klm", "nop", "qrs", "tuv", "wxy", "zab", "cde", "fgh", "ijk", "lmn", "opq", "rst"};
    private string[] wordArrayKompor = {"a", "i", "u", "e", "o"};
    // private static string[] wordArray = {"adil", "apa", "asik"};
    public enum TypeWord{
        tumbuk, kompor
    }
    [SerializeField]private TypeWord typeWord;
    private string[] wordArray;
    
    private int randChecker, sameChecker;
    private bool firstTime = true;
    private bool firstRandom = false;
    public string GetRandomWord(){
        

        if(typeWord == TypeWord.tumbuk){
            wordArray = wordArrayTumbuk;
        }
        else if(typeWord == TypeWord.kompor){
            wordArray = wordArrayKompor;
        }

        int random = Random.Range(0,wordArray.Length);
        
        //ngecek biar random ga dapet huruf sama lagi sebanyak sameChecker (2 kali utk skrg)
        if(firstTime){
            firstTime = false;
            randChecker = random;
            sameChecker = 0;
        }
        else{
            firstRandom = true;
            while(random == randChecker){
                random = Random.Range(0,wordArray.Length);
                if(firstRandom){
                    sameChecker++;
                    firstRandom = false;
                }
                
            }
        }
        if(sameChecker == 5){
            randChecker = random;
            sameChecker = 0;
        }
        // Debug.Log(random);
        string chosenWord = wordArray[random];

        return chosenWord;
    }
}
