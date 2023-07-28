using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControlUIMainMenu : MonoBehaviour
{
    [SerializeField]private MainMenuUI mainMenu;
    
    [SerializeField]private Sprite mainMenus, option, credits, reset;
    [SerializeField]private Image image;

    private void Start() {
        image.sprite = mainMenus;
        mainMenu.OnChange += mainMenu_OnChange;
    }

    private void mainMenu_OnChange(object sender, EventArgs e)
    {
        if(mainMenu.GetTypeMainMenu() == MainMenuUI.mainMenuType.normal || mainMenu.GetTypeMainMenu() == MainMenuUI.mainMenuType.language){
            image.sprite = mainMenus;
        }
        else if(mainMenu.GetTypeMainMenu() == MainMenuUI.mainMenuType.option){
            image.sprite = option;
        }
        else if(mainMenu.GetTypeMainMenu() == MainMenuUI.mainMenuType.reset){
            image.sprite = reset;
        }
        else if(mainMenu.GetTypeMainMenu() == MainMenuUI.mainMenuType.credits){
            image.sprite = credits;
        }
    }
}
