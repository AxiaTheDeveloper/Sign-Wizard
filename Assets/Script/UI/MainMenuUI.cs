using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]private GameObject selectSibi, selectBisindo, selectASL;
    private string selectLanguage;
    [SerializeField]private PlayerSave playerSaveSO;

    [SerializeField]private GameObject optionUI, creditsUI, resetUI;
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
    private bool isMusicOn, isSoundOn;
    [SerializeField]private SoundManager soundManager;
    [SerializeField]private BGMManager bgmManager;
    [Header("Reset")]
    [SerializeField]private GameObject[] selectChoiceReset;
    private bool isYesReset;
    

    private void Start() {
        selectLanguage = PlayerPrefs.GetString(PLAYER_PREF_PILIHAN_BAHASA, "BISINDO");
        changeBahasa(selectLanguage);

        selection = 0;
        type = mainMenuType.none;
        pilihanLanguage.SetActive(false);
        UpdateSelectArrow();


        //pause
        optionUI.gameObject.SetActive(false);
        musicSlider.gameObject.SetActive(false);
        SoundSlider.gameObject.SetActive(false);
        selectionPause = 0;
        UpdateSelectArrowPause();

        creditsUI.SetActive(false);

        isYesReset = false;
        resetUI.SetActive(false);
        UpdateSelectChoice_Reset();
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
                    playerSaveSO.isFromOutside = false;
                    playerSaveSO.isResetDay = false;
                    playerSaveSO.isSubmitPotion = false;
                    playerSaveSO.isResetSave = true;
                    playerSaveSO.isFirstTime_Tutorial = true;
                    #if UNITY_EDITOR
                    EditorUtility.SetDirty(playerSaveSO);
                    #endif
                }

                inputCooldownTimer = inputCooldownTimerMax;
                isYesReset = false;
                UpdateSelectChoice_Reset();
                resetUI.SetActive(false);
                
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
        if(!isMusicOn && !isSoundOn){
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
            fade.ShowUI();
        }
        else if(selection == 1){
            type = mainMenuType.language;
            pilihanLanguage.SetActive(true);
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
            Application.Quit();
        }
    }
    public void SelectToPlay(){
        PlayerPrefs.SetString(PLAYER_PREF_PILIHAN_BAHASA, selectLanguage);
            if(playerSaveSO.modeLevel == levelMode.outside){
                if(playerSaveSO.isFromOutside){
                    if(playerSaveSO.isIndonesia){
                        SceneManager.LoadSceneAsync("OutDoor_ID");
                    }
                    else{
                        SceneManager.LoadSceneAsync("OutDoor_EN");
                    }
                    
                }
                else if(playerSaveSO.isSubmitPotion){
                    if(playerSaveSO.isIndonesia){
                        SceneManager.LoadSceneAsync("InDoor_ID");
                    }
                    else{
                        SceneManager.LoadSceneAsync("InDoor_EN");
                    }
                }
                else{
                    if(!playerSaveSO.isFromOutside){
                        playerSaveSO.isFromOutside = true;
                        // #if UNITY_EDITOR
                        // EditorUtility.SetDirty(playerSaveSO);
                        // #endif
                    }
                }
            }
            else if(playerSaveSO.modeLevel == levelMode.MakingPotion){
                if(playerSaveSO.isIndonesia){
                    SceneManager.LoadSceneAsync("InDoor_ID");
                }
                else{
                    SceneManager.LoadSceneAsync("InDoor_EN");
                }
            }
    }
    private void Deselect(){
        
        if(type == mainMenuType.language){
            if((gameInput.GetInputSelectItemForCauldron() || gameInput.GetInputEscape() || gameInput.GetInputEscapeMainMenu()) && inputCooldownTimer <= 0){
                
                inputCooldownTimer = inputCooldownTimerMax;
                pilihanLanguage.SetActive(false);
                type = mainMenuType.normal;
                OnChange?.Invoke(this,EventArgs.Empty);
            }
        }
        else if(type == mainMenuType.option){
            if((gameInput.GetInputEscape() ||gameInput.GetInputEscapeMainMenu()) && inputCooldownTimer <= 0){
                inputCooldownTimer = inputCooldownTimerMax;
                isMusicOn = false;
                isSoundOn = false;
                SoundSlider.gameObject.SetActive(false);
                musicSlider.gameObject.SetActive(false);
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
            SoundSlider.gameObject.SetActive(false);
            musicSlider.gameObject.SetActive(false);
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

}
