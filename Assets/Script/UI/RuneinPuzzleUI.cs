using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneinPuzzleUI : MonoBehaviour
{
    private WitchGameManager gameManager;
    [SerializeReference]private RectTransform canvas_QuestUI;
    private bool isShow;
    [SerializeField]private GameInput gameInput;
    [SerializeField]private GameObject BISINDO, SIBI, ASL;

    private void Start() {
        gameManager = WitchGameManager.Instance;
        gameInput = GameInput.Instance;
        HideUI();
        showLanguageRune();
    }
    private void showLanguageRune()
    {
        if(PlayerPrefs.GetString("pilihanBahasa") == "BISINDO")
        {
            BISINDO.SetActive(true);
        }
        else if(PlayerPrefs.GetString("pilihanBahasa") == "SIBI")
        {
            SIBI.SetActive(true);
        }
        else if(PlayerPrefs.GetString("pilihanBahasa") == "ASL")
        {
            ASL.SetActive(true);
        }
        
    }

    private void Update() {
        //ini ntr trgantung si mo di sini jg ato ga itunya
        if(gameManager.IsInGame() && gameManager.IsInGameType() == WitchGameManager.InGameType.puzzle){
            if(!isShow && gameInput.GetInputShowRuneinPuzzle()){
                ShowUI();
            }
            else if(isShow && gameInput.GetInputHideRuneinPuzzle()){
                HideUI();
            }
        }
        else{
            if(isShow){
                HideUI();
            }
        }
        
    }
    

    private void ShowUI(){
        isShow = true;
        // canvas_QuestUI.anchoredPosition = new Vector3(755, 0f, 0f);
        LeanTween.move(canvas_QuestUI, new Vector3(-450, -53.67901f, 0f), 0.2f);
    }

    private void HideUI(){
        LeanTween.move(canvas_QuestUI, new Vector3(-1572, -53.67901f, 0f), 0.2f).setOnComplete(
            ()=> isShow = false
        );
    }
}
