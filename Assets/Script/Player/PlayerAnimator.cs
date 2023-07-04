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

    }
}
