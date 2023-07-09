using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]private Button startButton, optionsButton, quitButton, sibiButton, bisindoButton;
    [SerializeField]private GameObject selectSibi, selectBisindo;
    private string selectLanguage;
    [SerializeField]private PlayerSave playerSaveSO;

    [SerializeField]private GameObject optionUI;
    private const string PLAYER_PREF_PILIHAN_BAHASA = "pilihanBahasa";
    private void Awake() {
        startButton.onClick.AddListener(() => {
            PlayerPrefs.SetString(PLAYER_PREF_PILIHAN_BAHASA, selectLanguage);
            if(playerSaveSO.modeLevel == levelMode.outside){
                playerSaveSO.isFromOutside = true;
                SceneManager.LoadScene("OutDoor");
            }
            else if(playerSaveSO.modeLevel == levelMode.MakingPotion){
                SceneManager.LoadScene("InDoor");
            }
        });
        optionsButton.onClick.AddListener(() => {
            // optionUI.SetActive(true);
        });
        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });
        sibiButton.onClick.AddListener(() => {
            selectLanguage = "SIBI";
            selectSibi.SetActive(true);
            selectBisindo.SetActive(false);
        });
        bisindoButton.onClick.AddListener(() => {
            selectLanguage = "BISINDO";
            selectSibi.SetActive(false);
            selectBisindo.SetActive(true);
        });
    }
    private void Start() {
        selectLanguage = PlayerPrefs.GetString(PLAYER_PREF_PILIHAN_BAHASA, "BISINDO");
        if(selectLanguage == "BISINDO"){
            selectSibi.SetActive(false);
            selectBisindo.SetActive(true);
        }
        else if(selectLanguage == "SIBI"){
            selectSibi.SetActive(true);
            selectBisindo.SetActive(false);
        }
        
    }

    private void ShowOptionUI(){
        optionUI.SetActive(true);
    }
    private void HideOptionUI(){
        optionUI.SetActive(false);
    }

}
