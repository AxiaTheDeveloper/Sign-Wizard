using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUI : MonoBehaviour
{
    [SerializeField]private RectTransform canvas_MapUI;
    [SerializeField]private WitchGameManager gameManager;
    [SerializeField]private GameInput gameInput;
    [SerializeField]private GameObject playerChecker;
    [SerializeField]private Vector3 placeInFrontOfHouse, placeInForest, placeInGraveyard, placeInBrokenBridgeGraveyard, placeInMagicalBridge, placeInBrokenBridgeTown, placeInTown;
    [SerializeField]private float inputCoolDownTimerMax;
    private float inputCooldownTimer = 0;
    void Start()
    {
        gameInput = GameInput.Instance;
        gameManager = WitchGameManager.Instance;
        HideUI();
        SetPlayerVisualinMap();
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
            else if(gameManager.GetOutDoorType() == WitchGameManager.OutDoorType.magicalBridge)
            {
                playerChecker.transform.localPosition = placeInMagicalBridge;
            }
            else if(gameManager.GetOutDoorType() == WitchGameManager.OutDoorType.town)
            {
                playerChecker.transform.localPosition = placeInTown;
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
}