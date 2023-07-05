using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawner : MonoBehaviour
{
    [SerializeField]private GameObject displayPrefab_BISINDO;
    [SerializeField]private GameObject displayPrefab_SIBI;
    private GameObject displayPrefab;
    [SerializeField]private Transform parentCanvas;

    [SerializeField]private int fontSize;
    [SerializeField]private Color32 colorFont;
    private const string PLAYER_PREF_PILIHAN_BAHASA = "pilihanBahasa";
    private void Awake() {
        string pilihanBahasa = PlayerPrefs.GetString(PLAYER_PREF_PILIHAN_BAHASA, "SIBI");
        if(pilihanBahasa == "SIBI"){
            displayPrefab = displayPrefab_SIBI;
        }
        if(pilihanBahasa == "BISINDO"){
            displayPrefab = displayPrefab_BISINDO;
        }
    }
    public WordUI SpawnWord(){
        // Debug.Log(displayPrefab);
        GameObject displayObj = Instantiate(displayPrefab, parentCanvas);
        // Debug.Log(displayObj);
        WordUI wordDisplay = displayObj.GetComponent<WordUI>();
        wordDisplay.ChangeFontSize(fontSize);
        wordDisplay.ChangeFontColor(colorFont);
        return wordDisplay;
    }
}
