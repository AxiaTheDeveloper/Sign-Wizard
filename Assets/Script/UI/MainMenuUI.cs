using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]private GameObject selectSibi, selectBisindo, selectASL;
    private string selectLanguage;
    [SerializeField]private PlayerSave playerSaveSO;

    [SerializeField] private GameObject optionUI, creditsUI, resetUI;
    private GameObject mainMenu_NoButton;
    [Header("Button UI, Auto")]
    [SerializeField] private GameObject buttonStart, buttonOption, buttonCredits, buttonQuit;
    private const string PLAYER_PREF_PILIHAN_BAHASA = "pilihanBahasa";
    public event EventHandler OnChange;
    [SerializeField]private FadeMainMenu fade;

    [Header("No Button")]
    [SerializeField]private GameInput gameInput;
    [SerializeField]private GameObject[] selectArrows;
    private int selection;
    public enum mainMenuType{
        normal, language, option, credits, reset, none
    }
    private mainMenuType type;
    [SerializeField]private GameObject pilihanLanguage;
    [SerializeField]private float inputCooldownTimerMax;
    private float inputCooldownTimer;

    [Header("Pause")]
    [SerializeField]private GameObject[] selectArrowsPause;
    private int selectionPause;
    [SerializeField]private GameObject musicSlider, SoundSlider;
    private bool isMusicOn, isSoundOn, isLanguageOn;
    [SerializeField]private SoundManager soundManager;
    [SerializeField]private BGMManager bgmManager;
    private string selectionOptionLanguage;
    [SerializeField]private GameObject pilihanIDEN, arrowLanguageID, arrowLanguageEN;

    [Header("Reset")]
    [SerializeField]private GameObject[] selectChoiceReset;
    private bool isYesReset;
    [SerializeField] private GameObject resetNotif;
    [SerializeField] private Animator gameResetAnim;

    [Header("Quit")]
    [SerializeField]private GameSaveManager gameSaveManager;
    [SerializeField]private InventoryScriptableObject playerInvent, chest;
    [Header("ID EN")]
    [SerializeField]private string bahasaGame;


    private void Awake() {
        if(playerSaveSO.isFirstTimeInGame){
            playerSaveSO.isFirstTimeInGame = false;
            #if UNITY_EDITOR
                EditorUtility.SetDirty(playerSaveSO);
            #endif
            gameSaveManager.LoadData(playerSaveSO, playerInvent, chest);
        }
    }

    
    

    private void Start() {
        if(!PlayerPrefs.HasKey(PLAYER_PREF_PILIHAN_BAHASA))PlayerPrefs.SetString(PLAYER_PREF_PILIHAN_BAHASA, "BISINDO");
        selectLanguage = PlayerPrefs.GetString(PLAYER_PREF_PILIHAN_BAHASA);
        
        AssignGameObject();
        changeBahasa(selectLanguage);

        selection = 0;
        type = mainMenuType.none;
        pilihanLanguage.SetActive(false);
        UpdateSelectArrow();

        resetNotif.SetActive(false);

        //pause
        optionUI.gameObject.SetActive(false);
        musicSlider.gameObject.SetActive(false);
        pilihanIDEN.SetActive(false);
        SoundSlider.gameObject.SetActive(false);
        selectionPause = 0;
        if(!PlayerPrefs.HasKey("pilihanIDEN"))PlayerPrefs.SetString("pilihanIDEN", bahasaGame);
        selectionOptionLanguage = PlayerPrefs.GetString("pilihanIDEN");
        UpdateVisualLanguageOption();
        UpdateSelectArrowPause();

        creditsUI.SetActive(false);

        isYesReset = false;
        resetUI.SetActive(false);
        UpdateSelectChoice_Reset();
    }
    private void UpdateVisualLanguageOption(){
        if(selectionOptionLanguage == "ID"){
            arrowLanguageID.SetActive(true);
            arrowLanguageEN.SetActive(false);
        }
        else if(selectionOptionLanguage == "EN"){
            arrowLanguageEN.SetActive(true);
            arrowLanguageID.SetActive(false);
        }
    }
    private void UpdateSelectChoice_Reset(){
        if(isYesReset){
            selectChoiceReset[0].SetActive(true);
            selectChoiceReset[1].SetActive(false);
        }
        else{
            selectChoiceReset[0].SetActive(false);
            selectChoiceReset[1].SetActive(true);
        }
    }
    private void Update() {
        Vector2 keyInputArrow = gameInput.GetInputArrow();
        if(type == mainMenuType.normal){
            
            moveSelection_normal(keyInputArrow);
            if(gameInput.GetInputSelectItemForCauldron() && inputCooldownTimer <= 0){
                inputCooldownTimer = inputCooldownTimerMax;
                // Debug.Log("Load 1");
                Select();
            }
            if(gameInput.GetInputOpenInventory() && inputCooldownTimer <= 0){
                inputCooldownTimer = inputCooldownTimerMax;
                resetUI.SetActive(true);
                type = mainMenuType.reset;
            }
        }
        else if(type == mainMenuType.language){
            Deselect();
            if(keyInputArrow.y == -1 && selectLanguage == "BISINDO"){
                selectLanguage = "SIBI";
                changeBahasa(selectLanguage);
            }
            else if(keyInputArrow.y == -1 && selectLanguage == "SIBI"){
                selectLanguage = "ASL";
                changeBahasa(selectLanguage);
            }
            else if(keyInputArrow.y == 1 && selectLanguage == "SIBI"){
                selectLanguage = "BISINDO";
                changeBahasa(selectLanguage);
            }
            else if(keyInputArrow.y == 1 && selectLanguage == "ASL"){
                selectLanguage = "SIBI";
                changeBahasa(selectLanguage);
            }
        }
        else if(type == mainMenuType.option){
            Deselect();
            
            moveSelection_option(keyInputArrow);
            if(gameInput.GetInputSelectItemForCauldron() && inputCooldownTimer <= 0){
                inputCooldownTimer = inputCooldownTimerMax;
                Select_Option();
            }
            if(isMusicOn && keyInputArrow.x == 1){
                bgmManager.UpdateBGM_Volume(0.1f);
            }
            else if(isMusicOn && keyInputArrow.x == -1){
                bgmManager.UpdateBGM_Volume(-0.1f);
            }
            if(isSoundOn && keyInputArrow.x == 1){
                soundManager.UpdateSound_Volume(0.1f);
            }
            else if(isSoundOn && keyInputArrow.x == -1){
                soundManager.UpdateSound_Volume(-0.1f);
            }
            if(isLanguageOn && keyInputArrow.x == 1){
                if(selectionOptionLanguage == "ID"){
                    selectionOptionLanguage = "EN";
                    UpdateVisualLanguageOption();
                    PlayerPrefs.SetString("pilihanIDEN", selectionOptionLanguage);
                }
            }
            else if(isLanguageOn && keyInputArrow.x == -1){
                if(selectionOptionLanguage == "EN"){
                    selectionOptionLanguage = "ID";
                    UpdateVisualLanguageOption();
                    PlayerPrefs.SetString("pilihanIDEN", selectionOptionLanguage);
                }
            }
        }
        else if(type == mainMenuType.credits){
            Deselect();
        }
        else if(type == mainMenuType.reset){
            if((gameInput.GetInputOpenInventory() || gameInput.GetInputEscape()|| gameInput.GetInputEscapeMainMenu()) && inputCooldownTimer <= 0){
                inputCooldownTimer = inputCooldownTimerMax;
                isYesReset = false;
                UpdateSelectChoice_Reset();
                resetUI.SetActive(false);
                
                type = mainMenuType.normal;
                OnChange?.Invoke(this,EventArgs.Empty);
            }
            if(keyInputArrow.x == -1 && !isYesReset){
                isYesReset = true;
                UpdateSelectChoice_Reset();
            }
            else if(keyInputArrow.x == 1 && isYesReset){
                isYesReset = false;
                UpdateSelectChoice_Reset();
            }
            if(gameInput.GetInputSelectItemForCauldron() && inputCooldownTimer <= 0){

                if(isYesReset){
                    playerSaveSO.level = 1;
                    playerSaveSO.modeLevel = levelMode.outside;
                    playerSaveSO.isFromOutside = true;
                    playerSaveSO.isResetDay = false;
                    playerSaveSO.isSubmitPotion = false;
                    playerSaveSO.isResetSave = true;
                    playerSaveSO.isFirstTime_Tutorial = true;
                    #if UNITY_EDITOR
                    EditorUtility.SetDirty(playerSaveSO);
                    #endif
                    gameSaveManager.SaveData(playerSaveSO, playerInvent, chest);
                }

                inputCooldownTimer = inputCooldownTimerMax;
                
                UpdateSelectChoice_Reset();
                resetUI.SetActive(false);
                // Debug.Log("Reached");
                if(isYesReset)  StartCoroutine(TheGameIsResetNotif());
                isYesReset = false;

                type = mainMenuType.normal;
                OnChange?.Invoke(this,EventArgs.Empty);
            }
        }
        if(inputCooldownTimer > 0){
            inputCooldownTimer--;
        }
    }
    private void moveSelection_normal(Vector2 keyInputArrow){
        if(keyInputArrow.y == 1 && selection > 0 ){
            selection--;
        }
        else if(keyInputArrow.y == -1 && selection < selectArrows.Length-1){
            selection++;
        }
        
        UpdateSelectArrow();
    }
    private void moveSelection_option(Vector2 keyInputArrow){
        if(!isMusicOn && !isSoundOn && !isLanguageOn){
            if(keyInputArrow.y == 1 && selectionPause > 0 ){
                selectionPause--;
            }
            else if(keyInputArrow.y == -1 && selectionPause < selectArrowsPause.Length-1){
                selectionPause++;
            }
            
            UpdateSelectArrowPause();
        }
        
    }
    private void Select(){
        if(selection == 0){
            type = mainMenuType.none;
            fade.ShowUI();
        }
        else if(selection == 1){
            type = mainMenuType.language;
            pilihanLanguage.SetActive(true);
            buttonStart.SetActive(false);
            buttonOption.SetActive(false);
            buttonCredits.SetActive(false);
            buttonQuit.SetActive(false);
            if (buttonQuit != null) buttonQuit.SetActive(false);
            OnChange?.Invoke(this,EventArgs.Empty);
        }
        else if(selection == 2){
            type = mainMenuType.option;
            optionUI.gameObject.SetActive(true);
            OnChange?.Invoke(this,EventArgs.Empty);
        }
        else if(selection == 3){
            type = mainMenuType.credits;
            creditsUI.gameObject.SetActive(true);
            OnChange?.Invoke(this,EventArgs.Empty);
        }
        else if(selection == 4){
            
            playerSaveSO.isFirstTimeInGame = true;
            fade.QuitAndFadeOut();
        }
    }
    public void SelectToPlay(){
            if(playerSaveSO.modeLevel == levelMode.outside){
                if(playerSaveSO.isFromOutside){
                    if(playerSaveSO.level == 1)
                    {
                        if(selectionOptionLanguage == "ID"){
                            LevelLoader.Instance.LoadScene("OutDoor_InFrontHouse_ID");
                        }
                        else{
                            LevelLoader.Instance.LoadScene("OutDoor_InFrontHouse_EN");
                        }
                    }
                    else{
                        if(playerSaveSO.outDoorTypeNow == WitchGameManager.OutDoorType.town)
                        {
                            if(selectionOptionLanguage == "ID"){
                                LevelLoader.Instance.LoadScene("OutDoor_Town_ID");
                            }
                            else{
                                LevelLoader.Instance.LoadScene("OutDoor_Town_EN");
                            }
                        }
                        else if(playerSaveSO.outDoorTypeNow == WitchGameManager.OutDoorType.puzzleToTown)
                        {   
                            // Debug.Log("harusnya ke sini???");
                            if(selectionOptionLanguage == "ID"){
                                LevelLoader.Instance.LoadScene("OutDoor_PuzzleToTown_ID");
                            }
                            else{
                                LevelLoader.Instance.LoadScene("OutDoor_PuzzleToTown_EN");
                            }
                        }
                        else if(playerSaveSO.outDoorTypeNow == WitchGameManager.OutDoorType.inFrontOfHouse)
                        {
                            if(selectionOptionLanguage == "ID"){
                                LevelLoader.Instance.LoadScene("OutDoor_InFrontHouse_ID");
                            }
                            else{
                                LevelLoader.Instance.LoadScene("OutDoor_InFrontHouse_EN");
                            }
                        }
                    }
                    
                }
                else if(playerSaveSO.isSubmitPotion){
                    if(selectionOptionLanguage == "ID"){
                        LevelLoader.Instance.LoadScene("InDoor_ID");
                    }
                    else{
                        LevelLoader.Instance.LoadScene("InDoor_EN");
                    }
                }
                else{
                    if(!playerSaveSO.isFromOutside){
                        if(selectionOptionLanguage == "ID"){
                            LevelLoader.Instance.LoadScene("InDoor_ID");
                        }
                        else{
                            LevelLoader.Instance.LoadScene("InDoor_EN");
                        }
                    }
                }
            }
            else if(playerSaveSO.modeLevel == levelMode.MakingPotion){
                if(playerSaveSO.isFirstTime_Tutorial)
                {
                    playerSaveSO.isFromOutside = true;
                    if(selectionOptionLanguage == "ID"){
                        LevelLoader.Instance.LoadScene("OutDoor_InFrontHouse_ID");
                    }
                    else{
                        LevelLoader.Instance.LoadScene("OutDoor_InFrontHouse_EN");
                    }
                }
                else{
                    if(playerSaveSO.placePlayerNow == WitchGameManager.Place.outdoor)
                    {
                        if(playerSaveSO.outDoorTypeNow == WitchGameManager.OutDoorType.town)
                        {
                            if(selectionOptionLanguage == "ID"){
                                LevelLoader.Instance.LoadScene("OutDoor_Town_ID");
                            }
                            else{
                                LevelLoader.Instance.LoadScene("OutDoor_Town_EN");
                            }
                        }
                        else if(playerSaveSO.outDoorTypeNow == WitchGameManager.OutDoorType.puzzleToTown)
                        {   
                            // Debug.Log("harusnya ke sini???");
                            if(selectionOptionLanguage == "ID"){
                                LevelLoader.Instance.LoadScene("OutDoor_PuzzleToTown_ID");
                            }
                            else{
                                LevelLoader.Instance.LoadScene("OutDoor_PuzzleToTown_EN");
                            }
                        }
                        else if(playerSaveSO.outDoorTypeNow == WitchGameManager.OutDoorType.inFrontOfHouse)
                        {
                            if(selectionOptionLanguage == "ID"){
                                LevelLoader.Instance.LoadScene("OutDoor_InFrontHouse_ID");
                            }
                            else{
                                LevelLoader.Instance.LoadScene("OutDoor_InFrontHouse_EN");
                            }
                        }
                    }
                    else{
                        if(selectionOptionLanguage == "ID"){
                            LevelLoader.Instance.LoadScene("InDoor_ID");
                        }
                        else{
                            LevelLoader.Instance.LoadScene("InDoor_EN");
                        }
                    }
                    
                }
                
            }
            else if(playerSaveSO.modeLevel == levelMode.finishQuest){
                if(playerSaveSO.outDoorTypeNow == WitchGameManager.OutDoorType.town)
                {
                    if(selectionOptionLanguage == "ID"){
                        LevelLoader.Instance.LoadScene("OutDoor_Town_ID");
                    }
                    else{
                        LevelLoader.Instance.LoadScene("OutDoor_Town_EN");
                    }
                }
                else if(playerSaveSO.outDoorTypeNow == WitchGameManager.OutDoorType.puzzleToTown)
                {
                    if(selectionOptionLanguage == "ID"){
                        LevelLoader.Instance.LoadScene("OutDoor_PuzzleToTown_ID");
                    }
                    else{
                        LevelLoader.Instance.LoadScene("OutDoor_PuzzleToTown_EN");
                    }
                }
                else if(playerSaveSO.outDoorTypeNow == WitchGameManager.OutDoorType.inFrontOfHouse)
                {
                    if(selectionOptionLanguage == "ID"){
                        LevelLoader.Instance.LoadScene("OutDoor_InFrontHouse_ID");
                    }
                    else{
                        LevelLoader.Instance.LoadScene("OutDoor_InFrontHouse_EN");
                    }
                }
            }
    }
    private void Deselect(){
        
        if(type == mainMenuType.language){
            if((gameInput.GetInputSelectItemForCauldron() || gameInput.GetInputEscape() || gameInput.GetInputEscapeMainMenu()) && inputCooldownTimer <= 0){
                
                inputCooldownTimer = inputCooldownTimerMax;
                pilihanLanguage.SetActive(false);
                buttonStart.SetActive(true);
                buttonOption.SetActive(true);
                buttonCredits.SetActive(true);
                buttonQuit.SetActive(true);
                if (buttonQuit != null) buttonQuit.SetActive(true);
                type = mainMenuType.normal;
                OnChange?.Invoke(this,EventArgs.Empty);
            }
        }
        else if(type == mainMenuType.option){
            if((gameInput.GetInputEscape() ||gameInput.GetInputEscapeMainMenu()) && inputCooldownTimer <= 0){
                inputCooldownTimer = inputCooldownTimerMax;
                isMusicOn = false;
                isSoundOn = false;
                isLanguageOn = false;
                SoundSlider.gameObject.SetActive(false);
                musicSlider.gameObject.SetActive(false);
                pilihanIDEN.SetActive(false);
                optionUI.SetActive(false);
                type = mainMenuType.normal;
                OnChange?.Invoke(this,EventArgs.Empty);
            }
        }
        else if(type == mainMenuType.credits){
            if((gameInput.GetInputSelectItemForCauldron() || gameInput.GetInputEscape()||gameInput.GetInputEscapeMainMenu()) && inputCooldownTimer <= 0){
                inputCooldownTimer = inputCooldownTimerMax;
                // pilihanLanguage.SetActive(false);
                type = mainMenuType.normal;
                creditsUI.gameObject.SetActive(false);
                OnChange?.Invoke(this,EventArgs.Empty);
            }
        }
        
    }

    private void Select_Option(){
        if(selectionPause == 0){
            isMusicOn = false;
            isSoundOn = false;
            isLanguageOn = false;
            SoundSlider.gameObject.SetActive(false);
            musicSlider.gameObject.SetActive(false);
            pilihanIDEN.SetActive(false);
            optionUI.SetActive(false);
            type = mainMenuType.normal;
            OnChange?.Invoke(this,EventArgs.Empty);
        }
        else if(selectionPause == 1){
            if(isMusicOn){
                musicSlider.gameObject.SetActive(false);
                isMusicOn = false;
            }
            else if(!isMusicOn){
                musicSlider.gameObject.SetActive(true);
                isMusicOn = true;
            }
            
        }
        else if(selectionPause == 2){
            if(isSoundOn){
                SoundSlider.gameObject.SetActive(false);
                isSoundOn = false;
            }
            else if(!isSoundOn){
                SoundSlider.gameObject.SetActive(true);
                isSoundOn = true;
            }
        }
        else if(selectionPause == 3){
            if(isLanguageOn){
                pilihanIDEN.SetActive(false);
                isLanguageOn = false;
            }
            else if(!isLanguageOn){
                pilihanIDEN.SetActive(true);
                isLanguageOn = true;
            }
        }
    }
    private void changeBahasa(string bahasa){
        if(bahasa == "BISINDO"){
            selectSibi.SetActive(false);
            selectASL.SetActive(false);
            selectBisindo.SetActive(true);
        }
        else if(bahasa == "SIBI"){
            selectSibi.SetActive(true);
            selectASL.SetActive(false);
            selectBisindo.SetActive(false);
        }
        else if(bahasa == "ASL"){
            selectSibi.SetActive(false);
            selectASL.SetActive(true);
            selectBisindo.SetActive(false);
        }
        PlayerPrefs.SetString(PLAYER_PREF_PILIHAN_BAHASA, selectLanguage);
    }
    private void UpdateSelectArrow(){
        foreach(GameObject selectarrow in selectArrows){
            selectarrow.SetActive(false);
        }
        selectArrows[selection].SetActive(true);
    }
    private void UpdateSelectArrowPause(){
        foreach(GameObject selectarrow in selectArrowsPause){
            selectarrow.SetActive(false);
        }
        selectArrowsPause[selectionPause].SetActive(true);
    }

    private void ShowOptionUI(){
        optionUI.SetActive(true);
    }
    private void HideOptionUI(){
        optionUI.SetActive(false);
    }
    public mainMenuType GetTypeMainMenu(){
        return type;
    }
    public void ChangeToNormal(){
        type = mainMenuType.normal;
    }

    private void AssignGameObject()
    {
        if(resetNotif == null)
            resetNotif = GameObject.Find("CanvasForTransition").transform.GetChild(1).gameObject;
        resetNotif.SetActive(true);
        gameResetAnim = resetNotif.GetComponent<Animator>();
        mainMenu_NoButton = GameObject.Find("MainMenu_NoButton").gameObject;
        buttonStart = mainMenu_NoButton.transform.GetChild(0).gameObject;
        buttonOption = mainMenu_NoButton.transform.GetChild(2).gameObject;
        buttonCredits = mainMenu_NoButton.transform.GetChild(3).gameObject;
        buttonQuit = mainMenu_NoButton.transform.GetChild(4).gameObject;
    }

    private IEnumerator TheGameIsResetNotif()
    {
        resetNotif.SetActive(true);
        gameResetAnim.Play("NotifResetAnim");
        // Debug.Log((float)gameResetAnim.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(gameResetAnim.GetCurrentAnimatorStateInfo(0).length - 0.2f);
        resetNotif.SetActive(false);
        yield return null;
    }

}
