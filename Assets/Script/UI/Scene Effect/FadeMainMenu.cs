using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeMainMenu : MonoBehaviour
{
    [SerializeField]private RectTransform nightBG;
    [SerializeField]private MainMenuUI mainMenu;
    [SerializeField]private CanvasGroup loading;
    [SerializeField]private GameObject character, loadingText;
    [SerializeField]private float scaleSize, scaleSpeed;
    private void Awake() {
        HideUI();
        LeanTween.scale(loadingText, new Vector3(scaleSize, scaleSize, scaleSize), scaleSpeed).setLoopPingPong();
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

    public void QuitAndFadeOut()
    {
        LeanTween.alpha(nightBG, 1f, 1.2f).setOnComplete(() => Application.Quit());
    }
    public void Show(){
        // Debug.Log("Show Loading UI");
        
        mainMenu.SelectToPlay();
        // Debug.Log("Start Loading");
    }

}
