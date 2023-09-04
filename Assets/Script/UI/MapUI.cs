using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUI : MonoBehaviour
{
    public static MapUI Instance{get;private set;}
    [SerializeField]private RectTransform canvas_MapUI;
    [SerializeField]private WitchGameManager gameManager;
    [SerializeField]private GameInput gameInput;
    [SerializeField]private GameObject playerChecker;
    [SerializeField]private GameObject chalHouse, viiHouse, elineHouse, cloterHouse;
    [SerializeField]private Vector3 placeInFrontOfHouse, placeInForest, placeInGraveyard, placeInBrokenBridgeGraveyard, placeInMagicalBridgeLeft, placeInMagicalBridgeRight, placeInBrokenBridgeTown, placeInTownKiriAtas, placeInTownKananAtas, placeInTownKiriBawah, placeInTownKananBawah, placeInTownMerchant, placeInTownFox;
    [SerializeField]private float inputCoolDownTimerMax;
    [SerializeField]private MapChecker[] mapChecker;
    private float inputCooldownTimer = 0;
    private void Awake() {
        Instance = this;
    }

    private void mapChecker_OnChangePlaceCheckerBridge(object sender, MapChecker.OnChangePlaceCheckerBridgeEventArgs e)
    {
        if(e.place == MapChecker.MapMagicalBridge.kiri)
        {
            playerChecker.transform.localPosition = placeInMagicalBridgeLeft;
                
        }
        else if(e.place == MapChecker.MapMagicalBridge.kanan)
        {
            playerChecker.transform.localPosition = placeInMagicalBridgeRight;
                
        }
            
        
    }
    private void mapChecker_OnChangePlaceCheckerTown(object sender, MapChecker.OnChangePlaceCheckerTownEventArgs e)
    {
        if(e.place == MapChecker.MapTown.kiriAtas)
        {
            playerChecker.transform.localPosition = placeInTownKiriAtas;
                
        }
        else if(e.place == MapChecker.MapTown.kananAtas)
        {
            playerChecker.transform.localPosition = placeInTownKananAtas;
                
        }
        else if(e.place == MapChecker.MapTown.kiriBawah)
        {
            playerChecker.transform.localPosition = placeInTownKiriBawah;
                
        }
        else if(e.place == MapChecker.MapTown.kananBawah)
        {
            playerChecker.transform.localPosition = placeInTownKananBawah;
                
        }
        else if(e.place == MapChecker.MapTown.Fox)
        {
            playerChecker.transform.localPosition = placeInTownFox;
                
        }
        else if(e.place == MapChecker.MapTown.Vii)
        {
            playerChecker.transform.localPosition = placeInTownMerchant;
                
        }

    }

    void Start()
    {
        gameManager = WitchGameManager.Instance;
        if(gameManager.GetOutDoorType() == WitchGameManager.OutDoorType.magicalBridge)
        {
            for(int i=0;i<mapChecker.Length;i++)
            {
                mapChecker[i].OnChangePlaceCheckerBridge += mapChecker_OnChangePlaceCheckerBridge;
            }
        }
        else if(gameManager.GetOutDoorType() == WitchGameManager.OutDoorType.town)
        {
            for(int i=0;i<mapChecker.Length;i++)
            {
                mapChecker[i].OnChangePlaceCheckerTown += mapChecker_OnChangePlaceCheckerTown;
            }
        }
        gameInput = GameInput.Instance;
        
        QuestManager questManager = QuestManager.Instance;
        HideUI();
        SetPlayerVisualinMap();
        PlayerSaveManager saveManager = PlayerSaveManager.Instance;
        if(saveManager.GetPlayerLevelMode() != levelMode.finishQuest)
        {
            if(saveManager.GetPlayerLevel() < saveManager.GetMaxLevel())
            {
                if(saveManager.GetPlayerLevelMode() == levelMode.MakingPotion)
                {
                        if(questManager.GetSendername() == "Chal")
                    {
                        chalHouse.SetActive(true);
                    }
                    else if(questManager.GetSendername() == "Vii")
                    {
                        viiHouse.SetActive(true);
                    }
                    else if(questManager.GetSendername() == "Cloter")
                    {
                        cloterHouse.SetActive(true);
                    }
                    else if(questManager.GetSendername() == "Eline")
                    {
                        elineHouse.SetActive(true);
                    }
                }
                else
                {
                    chalHouse.SetActive(false);
                    cloterHouse.SetActive(false);
                    elineHouse.SetActive(false);

                    viiHouse.SetActive(false);
                }
                
            }
            else
            {
                if(saveManager.GetPlayerLevelMode() == levelMode.MakingPotion)
                {
                    viiHouse.SetActive(true);
                }
                else
                {
                    chalHouse.SetActive(false);
                    cloterHouse.SetActive(false);
                    elineHouse.SetActive(false);

                    viiHouse.SetActive(false);
                }
                
            }
            
            
        }
        else{
            viiHouse.SetActive(true);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(gameManager.IsInGame() && gameManager.IsInGameType() == WitchGameManager.InGameType.normal && gameManager.GetPlace() == WitchGameManager.Place.outdoor)
        {
            if(gameInput.GetInputShowMap() && inputCooldownTimer <= 0){
                ShowUI();
                gameManager.ChangeInterfaceType(WitchGameManager.InterfaceType.InterfaceMap);
                inputCooldownTimer = inputCoolDownTimerMax;
            }
            
        }
        else if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.InterfaceMap)
        {
            if((gameInput.GetInputShowMap() || gameInput.GetInputEscape() || gameInput.GetInputEscapeMainMenu()) && inputCooldownTimer <= 0){
                HideUI();
                gameManager.ChangeToInGame(WitchGameManager.InGameType.normal);
                inputCooldownTimer = inputCoolDownTimerMax;
            }
        }
        if(inputCooldownTimer > 0)
        {
            inputCooldownTimer -= Time.deltaTime;
        }
    }
    private void SetPlayerVisualinMap()
    {
        if(gameManager.GetPlace() == WitchGameManager.Place.indoor)
        {
            playerChecker.transform.localPosition = placeInFrontOfHouse;
        }
        else if(gameManager.GetPlace() == WitchGameManager.Place.outdoor)
        {
            if(gameManager.GetOutDoorType() == WitchGameManager.OutDoorType.inFrontOfHouse)
            {
                playerChecker.transform.localPosition = placeInFrontOfHouse;
            }
            else if(gameManager.GetOutDoorType() == WitchGameManager.OutDoorType.forest)
            {
                playerChecker.transform.localPosition = placeInForest;
            }
            else if(gameManager.GetOutDoorType() == WitchGameManager.OutDoorType.graveyard)
            {
                playerChecker.transform.localPosition = placeInGraveyard;
            }
            else if(gameManager.GetOutDoorType() == WitchGameManager.OutDoorType.brokenBridge_Graveyard)
            {
                playerChecker.transform.localPosition = placeInBrokenBridgeGraveyard;
            }
            
            else if(gameManager.GetOutDoorType() == WitchGameManager.OutDoorType.brokenBridge_Town)
            {
                playerChecker.transform.localPosition = placeInBrokenBridgeTown;
            }

        }
    }
    private void ShowUI(){
        // canvas_QuestUI.anchoredPosition = new Vector3(755, 0f, 0f);
        LeanTween.move(canvas_MapUI, new Vector3(0f, 0f, 0f), 0.2f);
    }
    private void HideUI(){
        LeanTween.move(canvas_MapUI, new Vector3(0, -1125f, 0f), 0.2f);
    }
    public void OpenHouseQuest()
    {
        PlayerSaveManager saveManager = PlayerSaveManager.Instance;
        QuestManager questManager = QuestManager.Instance;
        if(saveManager.GetPlayerLevelMode() != levelMode.finishQuest)
        {
            if(saveManager.GetPlayerLevel() < saveManager.GetMaxLevel())
            {
                if(saveManager.GetPlayerLevelMode() == levelMode.MakingPotion)
                {
                        if(questManager.GetSendername() == "Chal")
                    {
                        chalHouse.SetActive(true);
                    }
                    else if(questManager.GetSendername() == "Vii")
                    {
                        viiHouse.SetActive(true);
                    }
                    else if(questManager.GetSendername() == "Cloter")
                    {
                        cloterHouse.SetActive(true);
                    }
                    else if(questManager.GetSendername() == "Eline")
                    {
                        elineHouse.SetActive(true);
                    }
                }
                else
                {
                    chalHouse.SetActive(false);
                    cloterHouse.SetActive(false);
                    elineHouse.SetActive(false);

                    viiHouse.SetActive(false);
                }
                
            }
            else
            {
                if(saveManager.GetPlayerLevelMode() == levelMode.MakingPotion)
                {
                    chalHouse.SetActive(false);
                    cloterHouse.SetActive(false);
                    elineHouse.SetActive(false);

                    viiHouse.SetActive(true);
                }
                else
                {
                    chalHouse.SetActive(false);
                    cloterHouse.SetActive(false);
                    elineHouse.SetActive(false);

                    viiHouse.SetActive(false);
                }
                
            }
            
            
        }
        else{
            chalHouse.SetActive(false);
                    cloterHouse.SetActive(false);
                    elineHouse.SetActive(false);

                    viiHouse.SetActive(true);
        }
    }

}
