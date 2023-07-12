using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeMainMenu : MonoBehaviour
{
    [SerializeField]private RectTransform nightBG;
    [SerializeField]private MainMenuUI mainMenu;
    [SerializeField]private CanvasGroup loading_Text;
    private void Awake() {
        HideUI();
    }
    private void HideUI(){
        loading_Text.alpha = 0f;
        LeanTween.alpha(nightBG, 0f, 0.8f).setOnComplete(
            () => mainMenu.ChangeToNormal()
        );
    }
    public void ShowUI(){
        LeanTween.alpha(nightBG, 1f, 1.2f).setOnComplete(
            ()=>loading_Text.LeanAlpha(1f, 1f).setOnComplete(
                () => mainMenu.SelectToPlay()
            )
        );
    }

}
