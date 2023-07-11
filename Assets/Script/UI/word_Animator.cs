using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class word_Animator : MonoBehaviour
{
    [SerializeField]private PenumbukUI penumbukUI;
    public void StartTumbuk(){
        penumbukUI.ShowWordUI();
    }
}
