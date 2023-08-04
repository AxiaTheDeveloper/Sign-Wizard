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
    [SerializeField]private TimelineManager timelineManager;
    [SerializeField]private TimelineManager.TimelineType typeTimeline;
    [SerializeField]private bool isEndedWithTimeline;
    private CanvasGroup canvasGroup;
    
    private bool hasReadFull;
    private void Awake(){
        hasReadFull = false;
        canvasGroup = GetComponent<CanvasGroup>();
        gameObject.SetActive(false);
        if(pressSpaceToContinue.activeSelf){
            pressSpaceToContinue.SetActive(false);
        }
        
        canvasGroup.LeanAlpha(0f, 1.2f);
        
        UpdateVisual();
        
    }
    
    private void Update(){
        if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.InterfaceTutorial && gameObject.activeSelf){
            if(pageList.Length == 1){
                hasReadFull = true;
                pressSpaceToContinue.SetActive(true);
            }
            if((gameInput.GetInputEscape() || gameInput.GetInputNextLine_Dialogue()) && hasReadFull){
                hasReadFull = false;
                // Debug.Log(dialogueTutorial);
                Hide_Tutorial();
                
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
        gameObject.SetActive(true);
        canvasGroup.LeanAlpha(1f, 0.2f).setOnComplete(
            () => gameManager.ChangeInterfaceType(WitchGameManager.InterfaceType.InterfaceTutorial)
        );
    }
    public void Hide_Tutorial(){
        if(pressSpaceToContinue.activeSelf){
            pressSpaceToContinue.SetActive(false);
        }
        canvasGroup.LeanAlpha(0f, 0.5f).setOnComplete(
            () => StartCoroutine(StartNextThing())
        );
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
        if(isEndedWithTimeline){
            timelineManager.Start_Tutorials(typeTimeline);
        }
        else{
            dialogueManager.ShowDialogue_Tutorial(dialogueTutorial);
        }
        gameObject.SetActive(false);
    }

}
