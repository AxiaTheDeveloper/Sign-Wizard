using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField]private GameObject[] pageList;
    [SerializeField]private GameObject pressSpaceToContinue;
    private int selection = 0;
    [SerializeField]private SoundManager soundManager;
    [SerializeField]private WitchGameManager gameManager;
    [SerializeField]private GameInput gameInput;
    [SerializeField]private DialogueManager.DialogueTutorial dialogueTutorial;
    [SerializeField]private DialogueManager dialogueManager;

    private bool hasReadFull;
    private void Awake(){
        hasReadFull = false;
        Hide_Tutorial();
        UpdateVisual();
    }

    private void Update(){
        if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.InterfaceTutorial && gameObject.activeSelf){
            if((gameInput.GetInputEscape() || gameInput.GetInputNextLine_Dialogue()) && hasReadFull){
                // Debug.Log(dialogueTutorial);
                StartCoroutine(StartNextThing());
                
                
                
            }
            Vector2 keyInput = gameInput.GetInputArrow_Dictionary();
            ChangePage(keyInput);
        }
        
    }
    private void UpdateVisual(){
        foreach(GameObject page in pageList){
            page.SetActive(false);
        }
        pageList[selection].SetActive(true);
    }

    public void Show_Tutorial(){
        gameManager.ChangeInterfaceType(WitchGameManager.InterfaceType.InterfaceTutorial);
        gameObject.SetActive(true);
    }
    public void Hide_Tutorial(){
        if(pressSpaceToContinue.activeSelf){
            pressSpaceToContinue.SetActive(false);
        }
        
        gameObject.SetActive(false);
    }

    private void ChangePage(Vector2 keyArrowInput){
        if(keyArrowInput.x == -1 && selection > 0){

            selection--;
            soundManager.PlayFlipPage();
        }
        else if(keyArrowInput.x == 1 && selection < pageList.Length-1){
            selection++;

            if(!hasReadFull){
                // Debug.Log(selection +"" + pageList.Length);
                if(selection == pageList.Length-1){
                    hasReadFull = true;
                    pressSpaceToContinue.SetActive(true);
                }
            }

            soundManager.PlayFlipPage();
        }
        UpdateVisual();
    }

    private IEnumerator StartNextThing(){
        yield return new WaitForSeconds(0.01f);
        dialogueManager.ShowDialogue_Tutorial(dialogueTutorial);
        Hide_Tutorial();
    }

}
