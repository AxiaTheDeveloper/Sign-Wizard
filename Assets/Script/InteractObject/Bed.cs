using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private GameObject BG, charaImage, dialogue;
    
    [SerializeField]private GameObject yesNoQuestion;
    [SerializeField]private GameObject Selected_Yes, Selected_No;
    private bool isResetDay;

    private void Awake() {
        HideDialogue();
    }
    private void Start() {
        isResetDay = true;
    }
    private IEnumerator dialogueSequence(){
        dialogue.SetActive(true);
        DialogueSystem.DialogueLine line = dialogue.GetComponent<DialogueSystem.DialogueLine>();
        line.GoLineText();
        yield return new WaitUntil(()=> line.finished);
        line.ChangeFinished_false();

    }

    public void ShowDialogue(){
        BG.SetActive(true);
        // charaImage.SetActive(true);
        gameObject.SetActive(true);
        StartCoroutine(dialogueSequence());
    }
    public void HideDialogue(){
        BG.SetActive(false);
        charaImage.SetActive(false);
        dialogue.SetActive(false);
        gameObject.SetActive(false);
    }
}
