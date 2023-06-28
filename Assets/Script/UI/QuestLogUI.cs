using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLogUI : MonoBehaviour
{
    [SerializeField]private GameObject QuestLog_UI;
    [SerializeField]private GameInput gameInput;
    private void Start() {
        HideUI();
    }

    private void Update() {
        if(WitchGameManager.Instance.IsInGame()){
            if(!QuestLog_UI.activeSelf && gameInput.GetInputShowQuestLog()){
                ShowUI();
            }
            else if(QuestLog_UI.activeSelf && gameInput.GetInputHideQuestLog()){
                HideUI();
            }
        }
        else{
            if(QuestLog_UI.activeSelf){
                HideUI();
            }
        }
        
    }

    private void ShowUI(){
        QuestLog_UI.SetActive(true);
    }

    private void HideUI(){
        QuestLog_UI.SetActive(false);
    }
}
