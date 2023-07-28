using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentInsideControl : MonoBehaviour
{
    [SerializeField]private GameObject level2Gift, level3Gift, level4Gift;
    [SerializeField]private PlayerSaveManager playerSave;
    private void Awake() {
        if(playerSave.GetPlayerLevelMode() == levelMode.MakingPotion){
            if(playerSave.GetPlayerLevel() == 3){
                level2Gift.SetActive(true);
            }
            else if(playerSave.GetPlayerLevel() == 4){
                level2Gift.SetActive(true);
                level3Gift.SetActive(true);
            }
            else if(playerSave.GetPlayerLevel() > 4){
                level2Gift.SetActive(true);
                level3Gift.SetActive(true);
                level4Gift.SetActive(true);
            }
        }
        
    }
}
