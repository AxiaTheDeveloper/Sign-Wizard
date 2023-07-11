using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeMainMenu : MonoBehaviour
{
    [SerializeField]private RectTransform nightBG;
    [SerializeField]private MainMenuUI mainMenu;
    private void Awake() {
        HideUI();
    }
    private void HideUI(){
        LeanTween.alpha(nightBG, 0f, 0.8f).setOnComplete(
            () => mainMenu.ChangeToNormal()
        );
    }
    public void ShowUI(){
        LeanTween.alpha(nightBG, 1f, 1.2f).setOnComplete(
            () => mainMenu.SelectToPlay()
        );
    }

}
