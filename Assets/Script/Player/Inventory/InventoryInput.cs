using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryInput : MonoBehaviour
{
    public static InventoryInput Instance;
    [SerializeField]private GameInput gameInput;
    [SerializeField]private WitchGameManager gameManager;
    
    //Input pas inventory lg nyala
    private void Awake() {
        Instance = this;

    }
    private void Update()
    {
        
    }
}
