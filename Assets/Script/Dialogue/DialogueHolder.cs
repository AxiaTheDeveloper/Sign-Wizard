using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace DialogueSystem{
    public class DialogueHolder : MonoBehaviour
    {
        [SerializeField]private GameObject BG, charaImage, nameChara;
        [SerializeField]private GameObject BGVii, charaImageVii, nameCharaVii;
        public event EventHandler OnDialogueFinish;//ke dialogue manager
        public bool startOnAwake;

        private void Awake() 
        {
            if(startOnAwake)
            {
                StartCoroutine(dialogueSequence());
            }
            else
            {
                HideDialogue();
            }
        }
        private IEnumerator dialogueSequence()
        {
            for(int i=0;i<transform.childCount;i++)
            {
                Deactivate();
                transform.GetChild(i).gameObject.SetActive(true);
                DialogueSystem.DialogueLine line = transform.GetChild(i).GetComponent<DialogueLine>();
                line.GoLineText();
                yield return new WaitUntil(()=> line.finished);
                line.ChangeFinished_false();
                
            }
            
            HideDialogue();
            OnDialogueFinish?.Invoke(this, EventArgs.Empty);
        }
        private void Deactivate()
        {
            for(int i=0;i< transform.childCount;i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            
        }
        public void ShowDialogue()
        {
            // BG.SetActive(true);
            // if(BGVii) BGVii.SetActive(true);
            // charaImage.SetActive(true);
            gameObject.SetActive(true);
            StartCoroutine(dialogueSequence());
        }
        public void HideDialogue()
        {
            BG.SetActive(false);
            nameChara.SetActive(false);
            charaImage.SetActive(false);

            if(BGVii) BGVii.SetActive(false);
            if(nameCharaVii) nameCharaVii.SetActive(false);
            if(charaImageVii) charaImageVii.SetActive(false);
            gameObject.SetActive(false);
        }

        
    }
}
