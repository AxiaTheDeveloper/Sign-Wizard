using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance {get; private set;}
    [SerializeField]private PlayerSaveManager playerSaveManager;
    [SerializeField]private QuestScriptableObject[] questList;
    private QuestScriptableObject quest;
    [SerializeField]private QuestScriptableObject noQuest, finishQuest;
    private int levelNow, totalPotion;
    private ItemScriptableObject[] potionList;
    private List<bool> isPotionCheck_Right; // untuk check apakah si potion di array yang sama uda sama dengan potion yang dikirim belom
    //misal quest butuh 2 potion A (gbs di stack ato mungkin bisa ? lol, ya ini misal kalo gabisa), trus potion A, potion A, yang dikasih misal potion A dn B, misal potion A array 0 udah di cek oh bener, brarti break, brearti pas pengecekan potion B, si array 0 gausa di cek, cuma buat itu doang, yea smth like that lol

    [SerializeField]private QuestLogUI questUI;
    [SerializeField]private QuestBox questBoxUI;
    private void Awake() {
        Instance = this;
        levelNow = playerSaveManager.GetPlayerLevel();
        quest = questList[levelNow];
        totalPotion = quest.totalPotion;
        potionList = quest.potionWantList;
        
    }
    private void Start(){
        if(playerSaveManager.GetPlayerLevelMode() == levelMode.outside){
            // Debug.Log("test");
            questUI.SetData(noQuest);
        }
        else if(playerSaveManager.GetPlayerLevelMode() == levelMode.MakingPotion){
            // Debug.Log("gelooo");
            questUI.SetData(quest);
        }
        else if(playerSaveManager.GetPlayerLevelMode() == levelMode.finishQuest){
            // Debug.Log("gelooo");
            questUI.SetData(finishQuest);
        }
        if(WitchGameManager.Instance.GetPlace() == WitchGameManager.Place.outdoor && playerSaveManager.GetPlayerLevelMode() == levelMode.outside){
            questBoxUI.SetData(quest);
        }

        isPotionCheck_Right = new List<bool>();
        for(int i=0;i<totalPotion;i++){
            isPotionCheck_Right.Add(false);
        }
        
    }
    public void UpdateData_QuestLog(){
        questUI.SetData(quest);
    }
    public void UpdateData_FinishQuest(){
        questUI.SetData(finishQuest);
    }

    public bool CheckPotion(List<CauldronItem> cauldronItems){
        bool isPotionMatch = true;
        foreach(CauldronItem item in cauldronItems){
            for(int i=0;i<totalPotion;i++){
                if(isPotionCheck_Right[i]){
                    continue;
                }
                if(potionList[i] == item.itemSO){
                    isPotionCheck_Right[i] = true;
                    isPotionMatch = true;
                    break;
                }
                else{
                    isPotionMatch = false;
                }
            }
            if(!isPotionMatch){
                break;
            }
        }

        //kalo true - mainkan scene/timeline si player kirim potion dan dapet surat bagus, hasil timeline = naik level = beda quest
        //kalo false - mainkan scene/timeline player kirim dan dpt surat dimarahin, hasil timeline = level sama, quest sama
        Reset_IsPotionCheck();
        return isPotionMatch;
        
    }

    private void Reset_IsPotionCheck(){
        for(int i=0;i<totalPotion;i++){
            isPotionCheck_Right[i] = false;
        }
    }
    public int GetTotalPotionNeed(){
        return totalPotion;
    }

    public float GetProgressPerTumbuk_QuestNow(){
        return quest.progressPerTumbuk_Quest;
    }
    public string GetSendername()
    {
        return quest.nameSender;
    }
    public bool CheckPotion_BeforeGoToForest(InventoryScriptableObject playerInventory)
    {
        bool isPotionMatch = true;
        for(int i=0;i<totalPotion;i++){
            isPotionMatch = false;
            foreach(InventorySlot item in playerInventory.inventSlot)
            {
                if(potionList[i] == item.itemSO){
                    isPotionMatch = true;
                    break;
                }
            }
            if(!isPotionMatch){
                break;
            }
        }
        return isPotionMatch;
    }
}
