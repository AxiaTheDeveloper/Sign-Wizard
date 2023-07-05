using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField]private GameObject pauseUI;
    [SerializeField]private WitchGameManager gameManager;
    [SerializeField]private GameInput gameInput;

    [SerializeField]private Button resumeButton, mainButton;
    
    [SerializeField]private float escapeCooldownTimerMax;
    private float escapeCooldownTimer = 0;

    private void Awake() {
        resumeButton.onClick.AddListener(() => {
            gameManager.PauseGame();
            HideUI();
            escapeCooldownTimer = escapeCooldownTimerMax;
        });
    }
    void Start()
    {
        // escapeCooldownTimer = 0;
        HideUI();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.IsInGame()){
            if(gameInput.GetInputEscape() && escapeCooldownTimer <= 0){
                
                ShowUI();
                gameManager.PauseGame();
                escapeCooldownTimer = escapeCooldownTimerMax;
            }  
            else if(escapeCooldownTimer > 0){
                escapeCooldownTimer -= Time.deltaTime;
            }
            
            
        }
        else if(gameManager.isPause()){
            if(gameInput.GetInputEscape() && escapeCooldownTimer <= 0){
                HideUI();
                gameManager.PauseGame();
                escapeCooldownTimer = escapeCooldownTimerMax;
            }
            else if(escapeCooldownTimer > 0){
                escapeCooldownTimer -= Time.deltaTime;
            }
            
        }
        else{
            escapeCooldownTimer = escapeCooldownTimerMax;
        }
        
    }

    private void ShowUI(){
        pauseUI.SetActive(true);
    }
    private void HideUI(){
        pauseUI.SetActive(false);
    }


}
