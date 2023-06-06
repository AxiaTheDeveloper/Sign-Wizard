using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawner : MonoBehaviour
{
    [SerializeField]private GameObject displayPrefab;
    [SerializeField]private Transform parentCanvas;
    public WordUI SpawnWord(){
        GameObject displayObj = Instantiate(displayPrefab, parentCanvas);
        WordUI wordDisplay = displayObj.GetComponent<WordUI>();
        return wordDisplay;
    }
}
