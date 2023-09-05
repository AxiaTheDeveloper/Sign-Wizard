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
        private DialogueLine.LeftRightImagePosition lastImagePosition;
        private DialogueLine.LeftRightNamePosition lastNamePosition;
        private bool isFirstTimeLine = true;
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
                if(isFirstTimeLine)
                {
                    isFirstTimeLine = false;
                    lastImagePosition = line.GetImagePosition();
                    lastNamePosition = line.GetNamePosition();
                }
                else
                {
                    if(lastImagePosition != line.GetImagePosition())
                    {
                        if(lastImagePosition == DialogueLine.LeftRightImagePosition.Left) charaImage.SetActive(false);
                        else if(lastImagePosition == DialogueLine.LeftRightImagePosition.Right) charaImageVii.SetActive(false);
                    }
                    if(lastNamePosition != line.GetNamePosition())
                    {
                        if(lastNamePosition == DialogueLine.LeftRightNamePosition.Left) nameChara.SetActive(false);
                        else if(lastNamePosition == DialogueLine.LeftRightNamePosition.Right) nameCharaVii.SetActive(false);
                    }
                    lastImagePosition = line.GetImagePosition();
                    lastNamePosition = line.GetNamePosition();
                }
                
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
            isFirstTimeLine = true;
            nameChara.SetActive(false);
            charaImage.SetActive(false);
            BG.SetActive(false);
            
            if(nameCharaVii) nameCharaVii.SetActive(false);
            if(charaImageVii) charaImageVii.SetActive(false);
            if(BGVii) BGVii.SetActive(false);
            
            gameObject.SetActive(false);
        }

        
    }
}
