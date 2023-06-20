using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronUI : MonoBehaviour
{
    [SerializeField]private List<InventoryItemUI> CauldronItem_UI_List;
    [SerializeField]private Cauldron cauldron;

    //untuk api
    [SerializeField]private Transform needle;
    [SerializeField]private GameObject wordPlace;

    private void Start() {
        ResetAllVisual();
        HideCookUI();
    }


    private void ResetAllVisual(){
        foreach(InventoryItemUI inventSlot in CauldronItem_UI_List){
            inventSlot.ResetData();
        }
    }
    public void UpdateVisualInventorySlot(int position, CauldronItem item){
        // Debug.Log(position);
        if(!item.itemSO){
            CauldronItem_UI_List[position].ResetData();
        }
        else{
            CauldronItem_UI_List[position].SetItemData(item.itemSO,item.quantity);
        }
        
    }
    public void UpdateVisualNeedle(float rotation){
        needle.eulerAngles = new Vector3(0, 0, rotation);
    }

    public void ShowCookUI(){
        // WitchGameManager.Instance.ChangeInterfaceType(3);//invent+cauldron

        gameObject.SetActive(true);
        // inventUIDesc.EmptyDescUI();
        // UpdateVisual_InventDescription();
    }
    public void HideCookUI(){
        // WitchGameManager.Instance.ChangeToInGame();
        gameObject.SetActive(false);
        wordPlace.SetActive(false);
        
        
    }

    public void ShowWordUI(){
        wordPlace.SetActive(true);
        
    }
}
