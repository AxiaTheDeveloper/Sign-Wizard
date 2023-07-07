using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Announcement_SuccesfullGetItem : MonoBehaviour
{
    [SerializeField]private Image itemImage;
    [SerializeField]private TextMeshProUGUI itemTitle, itemDesc;

    private void Start() {
        ResetData();
        gameObject.SetActive(false);
    }

    private void Update() {
        if(gameObject.activeSelf && (GameInput.Instance.GetInputNextLine_Dialogue()||GameInput.Instance.GetInputEscape())){
            Hide();
        }
    }
    private void ResetData(){
        itemImage.gameObject.SetActive(false);
        itemTitle.text = "";
        itemDesc.text = "";
    }

    public void AddData(ItemScriptableObject itemSO_Success){
        itemImage.sprite = itemSO_Success.itemSprite;
        itemTitle.text = itemSO_Success.itemName;
        itemDesc.text = itemSO_Success.Desc;
    }

    public void Show(){
        itemImage.gameObject.SetActive(true);
        gameObject.SetActive(true);

    }
    public void Hide(){
        gameObject.SetActive(false);
        WitchGameManager.Instance.ChangeToInGame();
    }
}
