using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



namespace DialogueSystem{
    public class DialogueBase : MonoBehaviour
    {
        public bool finished {get; protected set;}
        protected IEnumerator typeText(string inputText, TextMeshProUGUI textHolder, float delayTypeText, float delayBetweenLines){
            //kalo mau
            // textHolder.color = textColor;
            // textHolder.font = textFont;
            for(int i=0; i<inputText.Length;i++){
                textHolder.text += inputText[i];
                
                yield return new WaitForSeconds(delayTypeText);

                if(i > 5 && GameInput.Instance.GetInputNextLine_Dialogue()){
                    // Debug.Log("halo?");
                    textHolder.text = inputText;
                    break;
                }
            }
            yield return new WaitForSeconds(delayBetweenLines);


            yield return new WaitUntil(()=>GameInput.Instance.GetInputNextLine_Dialogue());
            finished = true;
        }
        public void ChangeFinished_false(){
            finished = false;
        }
    }
}
