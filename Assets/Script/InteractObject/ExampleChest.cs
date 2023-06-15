using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Contoh salah satu anakkan interact object~~~
public class ExampleChest : MonoBehaviour
{
    [SerializeField]private WordInput wordInput;
    [SerializeField]private PlayerInventory playerInventory;
    [SerializeField]private InventoryUI ChestUI;
    [SerializeField]private GameObject wordUI;
    // private List<GameObject>
    private int chosenWord;
    
    [SerializeField]private WordManager[] wordManager;
    private WitchGameManager gameManager;

    private void Awake(){
        gameManager = WitchGameManager.Instance;
    }

    private void Start(){
        playerInventory.OnQuitChest += playerInventory_OnQuitChest;
    }

    private void playerInventory_OnQuitChest(object sender, EventArgs e)
    {
        CloseWholeUI();
    }

    


    public void ShowWholeUI(){
        //nyalain UI yg isinya kek gambar doang blm tulisan
        wordInput.GetWordManager(wordManager);
        wordInput.ChangeisOnlyOneWord(false);
        ChestUI.ShowInventoryUI();
        // wordUI.SetActive(true);
        gameManager.ChangeInterfaceType(4);
        // change game state
    }
    public void CloseWholeUI(){
        gameManager.ChangeToInGame();
        ChestUI.HideInventoryUI();
        // wordUI.SetActive(true);
        //change game state
        //di sini delete semua gameobject ???
    }
}
