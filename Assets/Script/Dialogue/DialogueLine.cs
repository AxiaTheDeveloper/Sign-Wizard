using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace DialogueSystem{
    public class DialogueLine : DialogueBase
    {
        private enum DialogueType{
            character, announcement
        }
        [Header("Option")]
        [SerializeField]private DialogueType dialogueType;

        [Header("All text")]
        private string inputText;
        private TextMeshProUGUI textHolder;
        [SerializeField]private bool isInputText_FromOtherCode = false;
        
        // [SerializeField]private Color textColor;
        // [SerializeField]private Font textFont;
        [Header("Time Delay ")]
        [SerializeField]private float delayTypeText;
        [SerializeField]private float delayBetweenLines;

        [Header("Image")]
        [SerializeField]private bool isCharaHaveImage;
        [SerializeField]private Image imageHolder;
        [SerializeField]private Sprite spriteCharacter;
        [Header("BG")]
        [SerializeField]private Color bgColor;
        [SerializeField]private Image backGroundHolder;


        private void Awake() {
            textHolder = GetComponent<TextMeshProUGUI>();
        }

        public void ChangeInputText(string inputTexts){
            inputText = inputTexts;
            // GoLineText();
        }
        public void GoLineText(){
            // Debug.Log(inputText);
            if(!isInputText_FromOtherCode){
                inputText = textHolder.text;
            }
            textHolder.text = "";
            // Debug.Log("lewat sini tiap start?");
            if(dialogueType == DialogueType.character){
                if(isCharaHaveImage){
                    imageHolder.sprite = spriteCharacter;
                    imageHolder.gameObject.SetActive(true);
                }
                else{
                    imageHolder.gameObject.SetActive(false);
                }
                backGroundHolder.color = bgColor;
            }
            IEnumerator lineText = typeText(inputText, textHolder, delayTypeText, delayBetweenLines);
            StartCoroutine(lineText);
            // Debug.Log(finished);
        }
    }
}
