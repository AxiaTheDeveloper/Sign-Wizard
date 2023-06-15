using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInteraction : MonoBehaviour
{

    public static PlayerInteraction Instance{get;private set;}

    private Vector2 keyInput;
    private Vector3 directionMovement = new Vector3(0,0,0);
    [SerializeField]private float interactDistance;
    
    
    [SerializeField]private LayerMask layerMask;
    private Collider2D collPlayer;



    private RaycastHit2D hitObject;
    private InteractObject selectedObject;


    public event EventHandler<OnSelectedInteractObjectEventArgs> OnSelectedInteractObject; 
    public class OnSelectedInteractObjectEventArgs : EventArgs{
        public InteractObject selectedObject;
    }
    
    private void Awake() {
        Instance = this;
        collPlayer = GetComponent<Collider2D>();
        
    }


    private void Update() {
        
        HandleSelectObjectInteractions();
        if(WitchGameManager.Instance.IsInGame()){
            if(GameInput.Instance.GetInputInteract() && selectedObject){
                // HandleInteractions();
                selectedObject.Interacts();

            }
        }
        
        
    }

    private void HandleSelectObjectInteractions(){
        keyInput = GameInput.Instance.GetLastInputMovement();
        directionMovement.Set(keyInput.x,keyInput.y,0);

        hitObject = Physics2D.Raycast(transform.position, directionMovement, interactDistance, (int)layerMask);
        if(hitObject.collider != null){
            InteractObject interactObject = hitObject.collider.transform.GetComponent<InteractObject>();
            if(selectedObject != interactObject){
                SetSelectedInteractObject(interactObject);
            }
        }
        else{
            SetSelectedInteractObject(null);
        }
        
        
        // Debug.DrawRay(transform.position, directionMovement, Color.green);

    }

    private void SetSelectedInteractObject(InteractObject selectedObject){
        this.selectedObject = selectedObject;

        OnSelectedInteractObject?.Invoke(this, new OnSelectedInteractObjectEventArgs{
            selectedObject = selectedObject
        });
    }

}
