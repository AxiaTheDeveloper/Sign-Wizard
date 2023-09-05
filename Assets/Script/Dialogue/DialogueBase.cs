using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



namespace DialogueSystem{
    public class DialogueBase : MonoBehaviour
    {
        public bool finished {get; protected set;}
        [SerializeField]protected GameObject pressToContinue_textHolder;

        protected IEnumerator typeText(string inputText, TextMeshProUGUI textHolder, float delayTypeText, float delayBetweenLines, GameObject backgroundHolder, GameObject imageHolder, GameObject nameHolder){
            //kalo mau
            // textHolder.color = textColor;
            // textHolder.font = textFont;
            
            for(int i=0; i<inputText.Length;i++)
            {
                textHolder.text += inputText[i];
                
                yield return new WaitForSeconds(delayTypeText);

                if(i > 5 && GameInput.Instance.GetInputNextLine_Dialogue()){
                    textHolder.text = inputText;
                    break;
                }
            }
            pressToContinue_textHolder.SetActive(true);
            yield return new WaitForSeconds(delayBetweenLines);

            yield return new WaitUntil(()=>GameInput.Instance.GetInputNextLine_Dialogue());
            pressToContinue_textHolder.SetActive(false);
            
            finished = true;
            // textHolder.gameObject.SetActive(false);
            // imageHolder.SetActive(false);
            // nameHolder.SetActive(false);
            // backgroundHolder.SetActive(false);
            
            
        }
        public void ChangeFinished_false()
        {
            finished = false;
        }
    }
}
