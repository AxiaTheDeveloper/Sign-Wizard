using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PenumbukUI : MonoBehaviour
{
    [SerializeField]private InventoryItemUI PenumbukItem_UI;
    [SerializeField]private Penumbuk penumbuk;

    //untuk progress
    [SerializeField]private GameObject wordPlace;

    [SerializeField]private Slider slider;
    [SerializeField]private Image sliderImage;

    private void Start() {
        ResetAllVisual();
        HidePenumbukUI();
        penumbuk.OnChangeProgress += penumbuk_OnChangeProgress;
    }

    private void penumbuk_OnChangeProgress(object sender, Penumbuk.OnChangeProgressEventArgs e)
    {
        slider.value = e.progressFill;
        Debug.Log(e.progressFill + "sini lo");
        if(e.progressFill >= 0.6f){
            sliderImage.color = new Color32(50,255,50,255);

        }
        else if(e.progressFill >= 0.3f){
            sliderImage.color = new Color32(255,170,92,255);

        }
        else{
            sliderImage.color = new Color32(255,26,0,255);
        }
    }

    private void ResetAllVisual(){
        PenumbukItem_UI.ResetData();
        slider.value = 0;

    }
    public void UpdateVisualInventorySlot(CauldronItem item){
        // Debug.Log(position);
        if(!item.itemSO){
            PenumbukItem_UI.ResetData();
        }
        else{
            PenumbukItem_UI.SetItemData(item.itemSO,item.quantity);
        }
        
    }

    public void ShowPenumbukUI(){
        gameObject.SetActive(true);
    }
    public void HidePenumbukUI(){
        // WitchGameManager.Instance.ChangeToInGame();
        gameObject.SetActive(false);
        wordPlace.SetActive(false);
        
        
    }

    public void ShowWordUI(){
        wordPlace.SetActive(true);
    }
    public void CloseWordUI(){
        wordPlace.SetActive(false);
    }
}
