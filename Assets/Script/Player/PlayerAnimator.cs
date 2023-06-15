using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    [SerializeField]private Animator animatorController;
    private const string ANIMATOR_HORIZONTAL = "Horizontal";
    private const string ANIMATOR_VERTICAL = "Vertical";
    private const string ANIMATOR_LAST_HORIZONTAL = "LastHorizontal";
    private const string ANIMATOR_LAST_VERTICAL = "LastVertical";
    private const string ANIMATOR_SPEED = "Speed";
    private Vector2 keyInput, lastKeyInput;


    private float lastSaveHorizontal, lastSaveVertical;

    private void Update() {
        keyInput = GameInput.Instance.GetInputMovement();
        lastKeyInput = GameInput.Instance.GetLastInputMovementAnimation();
        
        animatorController.SetFloat(ANIMATOR_HORIZONTAL, keyInput.x);
        animatorController.SetFloat(ANIMATOR_VERTICAL, keyInput.y);
        animatorController.SetFloat(ANIMATOR_LAST_HORIZONTAL, lastKeyInput.x);
        animatorController.SetFloat(ANIMATOR_LAST_VERTICAL, lastKeyInput.y);
        animatorController.SetFloat(ANIMATOR_SPEED, keyInput.sqrMagnitude);

    }
}
