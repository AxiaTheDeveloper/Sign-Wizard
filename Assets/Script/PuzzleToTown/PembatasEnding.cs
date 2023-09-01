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
                if(PlayerSaveManager.Instance.GetPlayerLevelMode() != levelMode.finishQuest)
                {
                    DialogueManager.Instance.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.sudahMenyelesaikanPuzzle_PembatasEnding);
                }
                else
                {
                    //wah magic uda ilang etc 
                    DialogueManager.Instance.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.MagicalBridgeHilangFinishQuest_PembatasEnding);
                }
                
            }
            
        }
    }
}
