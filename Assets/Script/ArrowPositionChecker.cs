using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPositionChecker : MonoBehaviour
{
    [SerializeField]private Transform ChalHouse, ElineHouse, ViiHouse, CloterHouse;
    [SerializeField]private Transform arrow;
    [SerializeField]private float jarakTerkecil;
    private Transform target;
    private void Start() {
        QuestManager questManager = QuestManager.Instance;
        PlayerSaveManager saveManager = PlayerSaveManager.Instance;
        if(saveManager.GetPlayerLevelMode() != levelMode.finishQuest)
        {
            if(saveManager.GetPlayerLevel() < saveManager.GetMaxLevel())
            {
                if(questManager.GetSendername() == "Chal")
                {
                    target = ChalHouse;
                }
                else if(questManager.GetSendername() == "Vii")
                {
                    target = ViiHouse;
                }
                else if(questManager.GetSendername() == "Cloter")
                {
                    target = CloterHouse;
                }
                else if(questManager.GetSendername() == "Eline")
                {
                    target = ElineHouse;
                }
            }
            else
            {
                target = ViiHouse;
            }
            
            
        }
        else{
            target = ViiHouse;
        }
    }
    private void Update() {
        if (target != null)
        {
            Vector3 directionToTarget = target.position - transform.position;
            if(Vector2.Distance(target.position,transform.position) > jarakTerkecil)
            {
                arrow.gameObject.SetActive(true);
            }
            else
            {
                arrow.gameObject.SetActive(false);
            }
            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

            arrow.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
    public void SearchTarget()
    {
        QuestManager questManager = QuestManager.Instance;
        PlayerSaveManager saveManager = PlayerSaveManager.Instance;
        if(saveManager.GetPlayerLevelMode() != levelMode.finishQuest)
        {
            if(saveManager.GetPlayerLevel() < saveManager.GetMaxLevel())
            {
                if(questManager.GetSendername() == "Chal")
                {
                    target = ChalHouse;
                }
                else if(questManager.GetSendername() == "Vii")
                {
                    target = ViiHouse;
                }
                else if(questManager.GetSendername() == "Cloter")
                {
                    target = CloterHouse;
                }
                else if(questManager.GetSendername() == "Eline")
                {
                    target = ElineHouse;
                }
            }
            else
            {
                target = ViiHouse;
            }
            
            
        }
        else{
            target = ViiHouse;
        }
    }
}
