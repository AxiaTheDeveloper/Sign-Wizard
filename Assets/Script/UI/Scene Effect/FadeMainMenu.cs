using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeMainMenu : MonoBehaviour
{
    [SerializeField]private RectTransform nightBG;
    [SerializeField]private MainMenuUI mainMenu;
    [SerializeField]private CanvasGroup loading;
    [SerializeField]private GameObject character;
    private void Awake() {
        HideUI();
    }
    private void HideUI(){
        character.SetActive(false);
        loading.alpha = 0f;
        LeanTween.alpha(nightBG, 0f, 1.2f).setOnComplete(
            () => mainMenu.ChangeToNormal()
        );
    }
    public void ShowUI(){
        character.SetActive(true);
        LeanTween.alpha(nightBG, 1f, 1.2f).setOnComplete(
            ()=>loading.LeanAlpha(1f, 1f).setOnComplete(
                () => Show()
            )
        );
    }
    public void Show(){
        
        mainMenu.SelectToPlay();
    }

}
