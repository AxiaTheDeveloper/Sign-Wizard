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
    [SerializeField]private Animator progressBar_Animator, word_Animator;
    private bool isAnimationDone = false;

    private void Start() {
        ResetAllVisual();
        gameObject.SetActive(false);
        wordPlace.SetActive(false);
        PenumbukItem_UI.gameObject.SetActive(false);
        penumbuk.OnChangeProgress += penumbuk_OnChangeProgress;
    }

    private void penumbuk_OnChangeProgress(object sender, Penumbuk.OnChangeProgressEventArgs e)
    {
        slider.value = e.progressFill;
        // Debug.Log(e.progressFill + "sini lo");
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
        isAnimationDone = false;
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
        isAnimationDone = false;
        gameObject.SetActive(false);
        wordPlace.SetActive(false);
        word_Animator.SetBool("isTumbuk", false);
        progressBar_Animator.SetBool("isTumbuk", false);
    }
    public void ShowTumbukUI(){
        PenumbukItem_UI.gameObject.SetActive(true);
        word_Animator.SetBool("isTumbuk", true);
        progressBar_Animator.SetBool("isTumbuk", true);
    }

    public void ShowWordUI(){
        isAnimationDone = true;
        wordPlace.SetActive(true);
    }
    public void CloseWordUI(){
        PenumbukItem_UI.gameObject.SetActive(false);
        wordPlace.SetActive(false);
        word_Animator.SetBool("isTumbuk", false);
        progressBar_Animator.SetBool("isTumbuk", false);
        isAnimationDone = false;
    }
    public bool GetisAnimationDone(){
        return isAnimationDone;
    }

    
}
