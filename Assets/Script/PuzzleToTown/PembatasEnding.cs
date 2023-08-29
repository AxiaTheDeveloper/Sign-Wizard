using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PembatasEnding : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(WitchGameManager.Instance.IsInGameType() == WitchGameManager.InGameType.normal)
            {
                DialogueManager.Instance.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.sudahMenyelesaikanPuzzle_PembatasEnding);
            }
            
        }
    }
}
