using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class Cauldron : MonoBehaviour
{
    [SerializeField]private PotionRecipeScriptableObject[] recipeList;
    private PotionRecipeScriptableObject recipeChosen;
    
    [SerializeField]private InventoryUI cauldronUI_Inventory;
    [SerializeField]private CauldronUI cauldronUI_Cook;
    private InventoryCauldron inventCauldron;
    [SerializeField]private WitchGameManager gameManager;
    [SerializeField]private PlayerInventory playerInventory;
    
    private List<CauldronItem> cauldronItems;
    private int item_Counter;
    [SerializeField]private int CauldronSize;
    [SerializeField]private Announcement_SuccesfullGetItem announcementUI;
    [SerializeField]private DialogueManager dialogueManager;


    [Header("This is for cooking part")]
    
    [SerializeField]private float maxRotation, minRotation;
    [SerializeField]private float maxSpeed, speed;
    private float totalAngle, speedRotation, rotation, finalRotation;//final buat nyimpen aja buat cek si player trakhir ambil apa
    [SerializeField]private float waitCloseUI;// waktu countdown utk nutup ui abis slsai fire stage

    [SerializeField]private WordInput wordInput;
    [SerializeField]private WordManager[] wordManager;
    [SerializeField]private FinishWordDoFunction finishFunction;
    [Header("FireSize - Level || per array dihitung level, array 0 = level 0")]
    [SerializeField]private float[] fireSizeMin;
    [SerializeField]private float[] fireSizeMax;

    private void Start() 
    {
        playerInventory.OnQuitCauldron += playerInventory_OnQuitCauldron;
        playerInventory.OnStartCookingCauldron += playerInventory_OnStartCookingCauldron;
        inventCauldron = cauldronUI_Inventory.GetInventoryCauldron();
        inventCauldron.OnItemCauldron += inventCauldron_OnItemCauldron;
        cauldronItems = new List<CauldronItem>();
        item_Counter = 0;
        for(int i=0;i<CauldronSize;i++)
        {
            CauldronItem newCauldron = new CauldronItem();
            cauldronItems.Add(newCauldron.EmptyItem());
        }

        finishFunction.OnStopCauldronFire += finishFunction_OnStopCauldronFire;
        CountFireSpeed();
    }

    private void inventCauldron_OnItemCauldron(object sender, InventoryCauldron.OnItemCauldronEventArgs e)
    {
        if(e.isAdd)
        {
            AddItemCauldron(e.Position);
        }
        else
        {
            RemoveItemCauldron(e.Position);
        }
    }

    private void playerInventory_OnQuitCauldron(object sender, EventArgs e)
    {
        CloseWholeUI();
    }
    private void playerInventory_OnStartCookingCauldron(object sender, EventArgs e)
    {
        InventoryScriptableObject playerInvent = playerInventory.GetPlayerInventory();
        
        if(item_Counter > 0 && !playerInvent.isFull)
        {
            //kalo gamau di cek pas abis milih api, ceknya ya di atas sini
            
            gameManager.ChangeInterfaceType(WitchGameManager.InterfaceType.CauldronFire);
            wordInput.ChangeAdaWord(true);
            wordManager[0].createWord();
            
            
            cauldronUI_Cook.ShowWordUI();
        }
        else if(item_Counter == 0)
        {
            dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.tidakAdaIngredientMasuk_Cauldron);
        }
        else if(playerInvent.isFull)
        {
            dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.tidakAdaTempatPotion_Cauldron);
        }
    }

    private void finishFunction_OnStopCauldronFire(object sender, EventArgs e)
    {
        gameManager.ChangeInterfaceType(WitchGameManager.InterfaceType.InventoryAndCauldron); // ini biar dia ga updet lagi
        cauldronUI_Cook.UpdateVisualNeedle(rotation);
        finalRotation = rotation;
        StartCoroutine(StartCountDownCloseUI_FireStage());
    }

    private void Update() 
    {
        if(gameManager.IsInterfaceType() == WitchGameManager.InterfaceType.CauldronFire) FireStage();
    }

    private void AddItemCauldron(int selectItem)
    {
        InventorySlot item;
        CauldronItem cauldronItem;
        for(int i=0;i<CauldronSize;i++){
            if(cauldronItems[i].isEmpty)
            {
                cauldronItem = new CauldronItem();
                item = playerInventory.GetPlayerInventory().TakeDataFromSlot(selectItem);
                cauldronItems[i] = cauldronItem.AddItem(item.itemSO, item.quantity, selectItem);
                cauldronUI_Cook.UpdateVisualInventorySlot(i, cauldronItems[i]);
                item_Counter++;
                break;
                //updet visual
            }
        }
        
    }

    private void RemoveItemCauldron(int selectItem)
    {
        CauldronItem cauldronItem;
        for(int i=0;i<CauldronSize;i++)
        {
            if(!cauldronItems[i].isEmpty && cauldronItems[i].position_InInventory == selectItem)
            {
                cauldronItem = new CauldronItem();
                // Debug.Log("di posisi "+ selectItem + " di urutan ke " + i);
                // Debug.Log("sebelum " + selectItem + " " + cauldronItems[i].itemSO);
                cauldronItems[i] = cauldronItem.EmptyItem();
                // Debug.Log("sesudah" + selectItem + " " + cauldronItems[i].itemSO);
                cauldronUI_Cook.UpdateVisualInventorySlot(i, cauldronItems[i]);
                item_Counter--;
                break;
            }
        }

    }

    private void CountFireSpeed()
    {
        totalAngle = minRotation - maxRotation;
        speedRotation = speed/maxSpeed;
        rotation = minRotation;
        // Debug.Log(speedRotation + " " + totalAngle + " " + totalAngle * speedRotation * Time.deltaTime);
        cauldronUI_Cook.UpdateVisualNeedle(rotation);
    }

    private void FireStage()
    {
        if(rotation <= maxRotation)
        {
            rotation = maxRotation;
            gameManager.ChangeInterfaceType(WitchGameManager.InterfaceType.InventoryAndCauldron); // ini biar dia ga updet lagi
            cauldronUI_Cook.UpdateVisualNeedle(rotation);
            finalRotation = rotation;
            StartCoroutine(StartCountDownCloseUI_FireStage());
        }
        else
        {
            rotation = rotation - (totalAngle * speedRotation * Time.deltaTime);
              
            cauldronUI_Cook.UpdateVisualNeedle(rotation);
        }
    }

    //ini countdownnya misal firestage ud abis ga lsg nutup biar keliatan arrow ada di mana
    private IEnumerator StartCountDownCloseUI_FireStage()
    {
        SoundManager.Instance.PlayStartFire();
        yield return new WaitForSeconds(waitCloseUI);
        CheckRecipe_ItemStage();
        //di sini jg bisa cek api pas ga, apakah ada resep (kalau ga di cek di atas), kalo ga ada resep jalankan animasi a, kalau ada resep jalankan animasi b, trus munculi di layar abis animasi dapet potion apa se
    }

    private void CheckRecipe_ItemStage()
    {
        recipeChosen = null;
        
        for(int i=0;i<recipeList.Length;i++)
        {
            PotionRecipeScriptableObject recipe = recipeList[i];
            bool isContainMatch = true;
            if(cauldronItems.Count == recipe.ingredientArray.Length)
            {

                // for(int j=0;j<cauldronItems.Count;j++){
                
                //     if(recipe.ingredientArray[j].ingredientName != cauldronItems[j].itemSO){
                //         isContainMatch = false;
                //         break;
                //     }
                // }
                foreach(CauldronItem cauldronItem in cauldronItems)
                {
                    bool isInTheRecipe = false;
                    foreach(PotionRecipeScriptableObject.Ingredients itemIngredient in recipe.ingredientArray)
                    {
                        if(cauldronItem.itemSO == itemIngredient.ingredientName)
                        {
                            
                            isInTheRecipe = true;
                            break;
                        }
                    }
                    if(!isInTheRecipe)
                    {
                        isContainMatch = false;
                        break;
                    }
                }
                if(isContainMatch)
                {
                    recipeChosen = recipe;
                    break;
                }
            }
            
        }
        if(recipeChosen)
        {
            if(!checkIngredient_Quantity())
            {
                if(isFireSizeCorrect(recipeChosen))
                {
                    cauldronUI_Inventory.HideCauldronInventory_Only();
                    cauldronUI_Cook.HideCookUI();
                    gameManager.ChangeToCinematic();
                    CountFireSpeed();
                    
                    announcementUI.AddData(recipeChosen.output_Potion);
                    CreatePotion();
                    //play the timeline or animation or anything,
                }
                else
                {
                    CloseWholeUI();
                    dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.tidakBerhasilJadi_Cauldron);
                    // Debug.Log("Tidak berhasil jadi"); //ato kalo mo nanti sesuai quality ya disesuaiin di sini, itu semua tetep diambil item, tp pas mo kasih, nah kasih potion yg jeleknya, jd nanti di scriptableobject recipe bakal ada 2 macam potiuon, high quality, ama low quality plg gitu, trus ini penentu nanti ksh ke player yg high ato low

                    //ini kalo jd kek di atas, brarti ini ntr cuma kek if recipechosen != null semua yg di isfiresizecorrect itu dijalanin dl, br dicek isfiresizecorrect buat kasih tau mo keluarin potion yg mana
                }
            }
            else
            {
                CloseWholeUI();
                dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.ingredientKurang_Cauldron);
            }
            
        } 
        else
        {
            CloseWholeUI();
            //ato play animasi gagal masak - yg semuanya diset di cauldronui
            dialogueManager.ShowDialogue_WrongChoice_WithoutBahan(DialogueManager.DialogueWrongChoice.tidakAdaResep_CauldronPenumbuk);
            // Debug.Log("Tidak ada resep dengan ingredient tersebut");
        }
        
    }
    private bool checkIngredient_Quantity()
    {
        bool isLowResource = false;
        // for(int i=0;i<recipeChosen.ingredientArray.Length;i++){
        //     if(cauldronItems[i].quantity < recipeChosen.ingredientArray[i].quantity){
        //         isLowResource = true;
        //         break;
        //     }
        // }
        foreach(CauldronItem cauldronItem in cauldronItems)
        {
            foreach(PotionRecipeScriptableObject.Ingredients itemIngredient in recipeChosen.ingredientArray)
            {
                if(cauldronItem.itemSO == itemIngredient.ingredientName)
                {
                    if(cauldronItem.quantity < itemIngredient.quantity)
                    {
                        isLowResource = true;
                        break;
                    }
                }
            }
        }
        return isLowResource;
    }
    public bool isFireSizeCorrect(PotionRecipeScriptableObject recipe)
    {
        int fireLevel = recipe.fireSizeLevel;
        
        finalRotation = Mathf.Abs(finalRotation);
        // Debug.Log(fireSizeMin[fireLevel] + " " + fireSizeMax[fireLevel] + "=" + finalRotation);
        if(finalRotation >= fireSizeMin[fireLevel] && finalRotation <= fireSizeMax[fireLevel]) return true;
        else return false;
    }

    public void CreatePotion()
    {
        //ini bakal dipanggil pas timeline masak potion berakhir.
        for(int j=0;j<cauldronItems.Count;j++)
        {
            if(!cauldronItems[j].isEmpty){
                int quantityTaken = 0;
                foreach(PotionRecipeScriptableObject.Ingredients itemIngredient in recipeChosen.ingredientArray)
                {
                    if(cauldronItems[j].itemSO == itemIngredient.ingredientName)
                    {
                        quantityTaken = itemIngredient.quantity;
                        break;
                    }
                }
                int position = cauldronItems[j].position_InInventory;                    
                playerInventory.GetPlayerInventory().TakeItemFromSlot(position, quantityTaken);
            }
        }

        playerInventory.GetPlayerInventory().AddItemToSlot(recipeChosen.output_Potion, 1);
        #if UNITY_EDITOR
        EditorUtility.SetDirty(playerInventory.GetPlayerInventory());
        #endif
        
        cauldronUI_Inventory.DeselectItemCauldron_Only();
        announcementUI.Show();
        // Debug.Log("Anda berhasil membuat potion " + recipeChosen.output_Potion);
    }
    public void ShowWholeUI()
    {
        wordInput.GetWordManager(wordManager);
        cauldronUI_Inventory.ShowInventoryUI();
        cauldronUI_Cook.ShowCookUI();
        // playerInventory.CauldronOpen();
        gameManager.ChangeInterfaceType(WitchGameManager.InterfaceType.InventoryAndCauldron);
    }
    public void CloseWholeUI()
    {
        gameManager.ChangeToInGame();
        cauldronUI_Cook.HideCookUI();
        cauldronUI_Inventory.HideInventoryUI();
        CountFireSpeed();
    }
    
}
