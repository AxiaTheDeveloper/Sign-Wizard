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
        private void Start() {
            inputText = textHolder.text;
            textHolder.text = "";
            IEnumerator lineText = typeText(inputText, textHolder, delayTypeText, delayBetweenLines);

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
            // else if(dialogueType == DialogueType.announcement){

            // }


            StartCoroutine(lineText);
        }
    }
}
