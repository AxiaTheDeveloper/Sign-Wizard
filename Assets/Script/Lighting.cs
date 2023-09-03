using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    private WitchGameManager gameManager;
    [SerializeField]private GameObject MorningLight;
    [SerializeField]private GameObject EveningLight;
    [SerializeField]private GameObject NightLight;
    private void Start() 
    {
        gameManager = WitchGameManager.Instance;
        if(gameManager.GetOutDoorType() == WitchGameManager.OutDoorType.inFrontOfHouse)
        {
            if(PlayerSaveManager.Instance.GetPlayerLevelMode() != levelMode.finishQuest)
            {
                MorningLight.SetActive(true);
                NightLight.SetActive(false);
            }
            else 
            {
                MorningLight.SetActive(false);
                NightLight.SetActive(true);
            }
        }
        else if(gameManager.GetOutDoorType() == WitchGameManager.OutDoorType.magicalBridge)
        {
            if(PlayerSaveManager.Instance.GetPlayerLevelMode() != levelMode.finishQuest)
            {
                MorningLight.SetActive(true);
                EveningLight.SetActive(false);
            }
            else 
            {
                MorningLight.SetActive(false);
                EveningLight.SetActive(true);
            }
        }
        else if(gameManager.GetOutDoorType() == WitchGameManager.OutDoorType.town || gameManager.GetOutDoorType() == WitchGameManager.OutDoorType.brokenBridge_Town)
        {
            if(PlayerSaveManager.Instance.GetPlayerLevel() < PlayerSaveManager.Instance.GetMaxLevel())
            {
                EveningLight.SetActive(true);
                MorningLight.SetActive(false);
            }
            else 
            {
                EveningLight.SetActive(false);
                MorningLight.SetActive(true);
            }
        }
        //ntr buat d town dan cuma ganti kalo dh level 6 aja
    }
}
