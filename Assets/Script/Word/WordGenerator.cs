using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGenerator : MonoBehaviour
{   
    private string[] wordArrayTumbuk = {"adil", "air", "alas", "alat", "aliran", "amal", "ampun", "aneh", "angka", "api", "aroma", "artis", "atas", "ayam", "bagus", "bahagia", "bahasa", "baja", "baju", "baliho", "bandara", "bangga", "bangunan", "bantal", "baru", "baru-baru", "baru saja", "basah", "bata", "batu", "bazar", "beban", "belajar", "bekerja", "belakang", "belanja", "berani", "berasa", "berat", "berdua", "berguna", "berhasil", "beri", "bersama", "bersih", "bertemu", "beruang", "besar", "besok", "betul", "bibir", "bintang", "bisnis", "buah", "buku", "bukan", "bulan", "burung", "butuh", "cahaya", "cepat", "cerah", "cerita", "cinta", "cokelat", "daftar", "dalam", "dalamnya", "dandan", "datang", "dekat", "desain", "dewasa", "diam", "dinding", "diri", "diskon", "dong", "dua", "duduk", "dunia", "edukasi", "emas", "enak", "energi", "enggak", "gagal", "gadis", "gajah", "gambar", "ganteng", "gantungan", "garam", "garis", "gaya"};
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
