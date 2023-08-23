using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestLogUI : MonoBehaviour
{
    [SerializeReference]private RectTransform canvas_QuestUI;
    private bool isShow;
    [SerializeField]private GameInput gameInput;
    [SerializeField]private TextMeshProUGUI questTitle, questDesc, questTaskList, questSender;

    private void Start() {
        HideUI();
    }
    public void SetData(QuestScriptableObject questSO){
        questTitle.text = questSO.Quest_Title;
        questDesc.text = questSO.QuestDescription;
        questTaskList.text = questSO.QuestTask;
        questSender.text = "- "+questSO.nameSender;
    }

    private void Update() {
        //ini ntr trgantung si mo di sini jg ato ga itunya
        if(WitchGameManager.Instance.IsInGame()){
            if(!isShow && gameInput.GetInputShowQuestLog()){
                ShowUI();
            }
            else if(isShow && gameInput.GetInputHideQuestLog()){
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
        LeanTween.move(canvas_QuestUI, new Vector3(65f, 55f, 0f), 0.2f);
    }

    private void HideUI(){
        LeanTween.move(canvas_QuestUI, new Vector3(965, 55f, 0f), 0.2f).setOnComplete(
            ()=> isShow = false
        );
    }
}
