using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawner : MonoBehaviour
{
    [SerializeField]private GameObject displayPrefab_BISINDO;
    [SerializeField]private GameObject displayPrefab_SIBI;
    private GameObject displayPrefab;
    [SerializeField]private Transform parentCanvas;
    private enum bahasa{
        BISINDO, SIBI
    }
    [SerializeField]private bahasa pilihanBahasa;
    private void Start() {
        if(pilihanBahasa == bahasa.BISINDO){
            displayPrefab = displayPrefab_BISINDO;
        }
        else if(pilihanBahasa == bahasa.SIBI){
            displayPrefab = displayPrefab_SIBI;
        }
    }
    public WordUI SpawnWord(){
        
        GameObject displayObj = Instantiate(displayPrefab, parentCanvas);
        WordUI wordDisplay = displayObj.GetComponent<WordUI>();
        return wordDisplay;
    }
}
