using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGenerator : MonoBehaviour
{   
    private static string[] wordArray = {"adil", "air", "alas", "alat", "aliran", "amal", "ampun", "aneh", "angka", "api", "aroma", "artis", "atas", "ayam", "bagus", "bahagia", "bahasa", "baja", "baju", "baliho", "bandara", "bangga", "bangunan", "bantal", "baru", "baru-baru", "baru saja", "basah", "bata", "batu", "bazar", "beban", "belajar", "bekerja", "belakang", "belanja", "berani", "berasa", "berat", "berdua", "berguna", "berhasil", "beri", "bersama", "bersih", "bertemu", "beruang", "besar", "besok", "betul", "bibir", "bintang", "bisnis", "buah", "buku", "bukan", "bulan", "burung", "butuh", "cahaya", "cepat", "cerah", "cerita", "cinta", "cokelat", "daftar", "dalam", "dalamnya", "dandan", "datang", "dekat", "desain", "dewasa", "diam", "dinding", "diri", "diskon", "dong", "dua", "duduk", "dunia", "edukasi", "emas", "enak", "energi", "enggak", "gagal", "gadis", "gajah", "gambar", "ganteng", "gantungan", "garam", "garis", "gaya"};
    // private static string[] wordArray = {"adil", "apa", "asik"};
    
    private static int randChecker, sameChecker;
    private static bool firstTime = true;
    private static bool firstRandom = false;
    public static string GetRandomWord(){
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
        if(sameChecker == 2){
            randChecker = random;
            sameChecker = 0;
        }
        // Debug.Log(random);
        string chosenWord = wordArray[random];

        return chosenWord;
    }
}
