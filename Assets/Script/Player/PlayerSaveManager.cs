using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PlayerSaveManager : MonoBehaviour
{
    private WitchGameManager gameManager;
    public static PlayerSaveManager Instance{get; private set;}
    [SerializeField]private PlayerSave playerSaveSO;
    [SerializeField]private PlayerAnimator playerAnimator;
    private Transform playerPosition;
    [SerializeField]private Transform bedPlayerPosition;
    [SerializeField]private int maxLevel;

    [Header("Di In front of House")]
    [SerializeField]private Transform spawnPlaceFromForest;
    [Header("Di Forest")]
    [SerializeField]private Transform spawnPlaceFromMagicalBridge;
    [SerializeField]private Transform spawnPlaceFromGraveyard;
    [Header("Di Graveyard")]
    [SerializeField]private Transform spawnPlaceFromBrokenBridge;
    [Header("Di Town")]
    [SerializeField]private Transform spawnPlaceFromBrokenBridge_Town;
    [Header("Di MagicalBridge")]
    [SerializeField]private Transform spawnPlaceFromTown;

    [SerializeField]private FadeNight_StartEnd fade;

    

    private void Awake() {
        Instance = this;
        playerAnimator = transform.GetChild(0).GetComponent<PlayerAnimator>();
        if(playerSaveSO.modeLevel == levelMode.outside && playerSaveSO.level == 1){
            Transform visualPos = transform.GetChild(0);
            // Debug.Log(visualPos.gameObject);
            visualPos.localPosition = new Vector3(0,-2.65f,0);
        }
        
    }
    private void Start(){
        gameManager = WitchGameManager.Instance;
        if(gameManager.GetPlace() == WitchGameManager.Place.outdoor)
        {
            if(gameManager.GetOutDoorType() == WitchGameManager.OutDoorType.inFrontOfHouse)
            {
                if(playerSaveSO.outDoorTypeNow != WitchGameManager.OutDoorType.none && playerSaveSO.outDoorTypeNow != WitchGameManager.OutDoorType.inFrontOfHouse)
                {
                    transform.position = spawnPlaceFromForest.localPosition;
                    playerAnimator.animator.Play("Player_Idle_Up");
                    playerAnimator.animator.SetBool("idle", true);
                    if(playerSaveSO.outDoorTypeNow == WitchGameManager.OutDoorType.town && playerSaveSO.modeLevel == levelMode.finishQuest)
                    {
                        gameObject.SetActive(false);
                        fade.DoFinishQuestAnimation();
                    }
                }
            }
            else if(gameManager.GetOutDoorType() == WitchGameManager.OutDoorType.forest)
            {
                if(playerSaveSO.outDoorTypeNow == WitchGameManager.OutDoorType.magicalBridge)
                {
                    transform.position = spawnPlaceFromMagicalBridge.localPosition;
                    playerAnimator.animator.Play("Player_Idle_Left");
                    playerAnimator.animator.SetBool("idle", true);
                }
                else if(playerSaveSO.outDoorTypeNow == WitchGameManager.OutDoorType.graveyard)
                {
                    transform.position = spawnPlaceFromGraveyard.localPosition;
                    playerAnimator.animator.Play("Player_Idle_Up");
                    playerAnimator.animator.SetBool("idle", true);
                }
            }
            else if(gameManager.GetOutDoorType() == WitchGameManager.OutDoorType.graveyard)
            {
                if(playerSaveSO.outDoorTypeNow == WitchGameManager.OutDoorType.brokenBridge_Graveyard)
                {
                    transform.position = spawnPlaceFromBrokenBridge.localPosition;
                    playerAnimator.animator.Play("Player_Idle_Left");
                    playerAnimator.animator.SetBool("idle", true);
                }
            }
            else if(gameManager.GetOutDoorType() == WitchGameManager.OutDoorType.town)
            {
                if(playerSaveSO.outDoorTypeNow == WitchGameManager.OutDoorType.brokenBridge_Town)
                {
                    transform.position = spawnPlaceFromBrokenBridge_Town.localPosition;
                    
                }
                playerAnimator.animator.Play("Player_Idle_Right");
                playerAnimator.animator.SetBool("idle", true);
            }
            else if(gameManager.GetOutDoorType() == WitchGameManager.OutDoorType.magicalBridge)
            {
                playerAnimator.animator.Play("Player_Idle_Right");
                playerAnimator.animator.SetBool("idle", true);
                if(playerSaveSO.outDoorTypeNow == WitchGameManager.OutDoorType.town)
                {
                    transform.position = spawnPlaceFromTown.localPosition;
                    playerAnimator.animator.Play("Player_Idle_Left");
                    playerAnimator.animator.SetBool("idle", true);
                }
                else if(playerSaveSO.modeLevel == levelMode.finishQuest)
                {
                    transform.position = spawnPlaceFromTown.localPosition;
                    playerAnimator.animator.Play("Player_Idle_Left");
                    playerAnimator.animator.SetBool("idle", true);
                }

                
            }
            else if(gameManager.GetOutDoorType() == WitchGameManager.OutDoorType.brokenBridge_Graveyard)
            {
                playerAnimator.animator.Play("Player_Idle_Right");
                playerAnimator.animator.SetBool("idle", true);
            }
            else if(gameManager.GetOutDoorType() == WitchGameManager.OutDoorType.brokenBridge_Town)
            {
                playerAnimator.animator.Play("Player_Idle_Left");
                playerAnimator.animator.SetBool("idle", true);
            }
        }
        playerSaveSO.placePlayerNow = gameManager.GetPlace();
        playerSaveSO.outDoorTypeNow = gameManager.GetOutDoorType();
    }


    public void HasReset(){
        if(playerSaveSO.isResetDay){
            playerSaveSO.isResetDay = false;
        }
        else if(playerSaveSO.isFromOutside){
            playerSaveSO.isFromOutside = false;
        }
        #if UNITY_EDITOR
        EditorUtility.SetDirty(playerSaveSO);
        #endif
        
        // Debug.Log(hasReset);
    }
    public void player_HasReset_Place(){
        if(playerSaveSO.isResetDay){
            playerPosition = GetComponent<Transform>();
            playerPosition.position = bedPlayerPosition.position;
        }
        else if(playerSaveSO.isFromOutside){
            playerPosition = GetComponent<Transform>();
            playerPosition.position = new Vector3(-1f,-7.75f,0f);
        }
    }
    public void HasDone_GoOutDialogue(){
        
        playerSaveSO.isSubmitPotion = false;
        // Debug.Log(hasReset);
        #if UNITY_EDITOR
        EditorUtility.SetDirty(playerSaveSO);
        #endif
    }
    public void player_GoOutDialogue_Place(){
        playerPosition = GetComponent<Transform>();
        playerPosition.position = bedPlayerPosition.position;
    }
    public int GetPlayerLevel(){
        return playerSaveSO.level;
    }
    public levelMode GetPlayerLevelMode(){
        return playerSaveSO.modeLevel;
    }

    public void ChangeFirstTimeTutorial(){
        playerSaveSO.isFirstTime_Tutorial = false;
    }
    public bool GetFirstTimeTutorial(){
        return playerSaveSO.isFirstTime_Tutorial;
    }
    public void ChangeFirstTimeTutorialPuzzle(){
        playerSaveSO.isFirstTime_TutorialPuzzle = false;
    }
    public bool IsFirstTime_TutorialPuzzle(){
        return playerSaveSO.isFirstTime_TutorialPuzzle;
    }

    public void ChangePlayerLevel(){
        if(playerSaveSO.level == 0){
            playerSaveSO.level = 1;
        }
        else if(playerSaveSO.level == 1){
            playerSaveSO.level = 2;
        }
        else if(playerSaveSO.level == 2){
            playerSaveSO.level = 3;
        }
        else if(playerSaveSO.level == 3){
            playerSaveSO.level = 4;
        }
        else if(playerSaveSO.level == 4){
            playerSaveSO.level = 5;
        }
        else if(playerSaveSO.level == 5){
            playerSaveSO.level = 6;
        }
        
        #if UNITY_EDITOR
        EditorUtility.SetDirty(playerSaveSO);
        #endif
        
    }
    public void ChangePlayerMode(levelMode mode){
        playerSaveSO.modeLevel = mode;
        #if UNITY_EDITOR
        EditorUtility.SetDirty(playerSaveSO);
        #endif
    }
    public void resetDay(){
        playerSaveSO.isResetDay = true;
        #if UNITY_EDITOR
        EditorUtility.SetDirty(playerSaveSO);
        #endif
    }

    public void ResetDay_Sleep(){
       
        playerSaveSO.isResetDay = true;
        if(PlayerPrefs.GetString("pilihanIDEN") == "ID"){
            SceneManager.LoadSceneAsync("InDoor_ID");
        }
        else{
            SceneManager.LoadSceneAsync("InDoor_EN");
        }
        #if UNITY_EDITOR
        EditorUtility.SetDirty(playerSaveSO);
        #endif
    }
    public void ResetDay_SubmitPotion(){
        playerSaveSO.isSubmitPotion = true;
        if(PlayerPrefs.GetString("pilihanIDEN") == "ID"){
            SceneManager.LoadSceneAsync("InDoor_ID");
        }
        else{
            SceneManager.LoadSceneAsync("InDoor_EN");
        }
        
        #if UNITY_EDITOR
        EditorUtility.SetDirty(playerSaveSO);
        #endif
    }
    public void Go_OutsideNow(){
        if(playerSaveSO.modeLevel == levelMode.outside){
            playerSaveSO.isFromOutside = true;
            #if UNITY_EDITOR
            EditorUtility.SetDirty(playerSaveSO);
            #endif
        }
        if(PlayerPrefs.GetString("pilihanIDEN") == "ID"){
            SceneManager.LoadSceneAsync("OutDoor_InFrontHouse_ID");
        }
        else{
            SceneManager.LoadSceneAsync("OutDoor_InFrontHouse_EN");
        }
    }
    public void Go_InsideNow(){
        if(PlayerPrefs.GetString("pilihanIDEN") == "ID"){
            SceneManager.LoadSceneAsync("InDoor_ID");
        }
        else{
            SceneManager.LoadSceneAsync("InDoor_EN");
        }
    }
    public bool GetIsReset(){
        return playerSaveSO.isResetDay;
    }
    public bool GetIsPlayerFromOutside(){
        return playerSaveSO.isFromOutside;
    }
    
    public bool GetIsSubmitPotion(){
        return playerSaveSO.isSubmitPotion;
    }
    public bool GetIsResetSave(){
        return playerSaveSO.isResetSave;
    }
    public void HasResetSave(){
        playerSaveSO.isResetSave = false;
        #if UNITY_EDITOR
            EditorUtility.SetDirty(playerSaveSO);
            #endif
    }

    public int GetMaxLevel(){
        return maxLevel;
    }
    public PlayerSave GetPlayerSave(){
        return playerSaveSO;
    }
    public bool GetIsMagicalPuzzleThisLevelSolved()
    {
        return playerSaveSO.isMagicalBridgeSolved;
    }
    public void ChangeIsMagicalBridgeSolve(bool change)
    {
        playerSaveSO.isMagicalBridgeSolved = change;
    }




}
