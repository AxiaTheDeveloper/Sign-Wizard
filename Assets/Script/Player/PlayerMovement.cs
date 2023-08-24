using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private WitchGameManager gameManager;
    [SerializeField]private GameInput gameInput;
    [SerializeField]private Rigidbody2D rb;
    private Vector2 keyInput, keyInputPuzzle;

    [Header("This is for Player Movement")]
    [SerializeField]private float speedMovement;

    private bool wasFromOtherInterface = false;
    [Header("This is for Player Movement Puzzle")]
    private bool canWalk = true;
    [SerializeField] private float totalMoveBlock;
    [SerializeField]private float durationMovementPuzzle;
    [SerializeField]private PlayerAnimator playerAnimator;
    [SerializeField]private int playerPuzzlePositionNow;

    private void Start() {
        gameManager = WitchGameManager.Instance;
        canWalk = true;
    }


    private void Update() {
        if(gameManager.IsInGame()){
            if(!wasFromOtherInterface){
                if(gameManager.IsInGameType() == WitchGameManager.InGameType.normal)
                {
                    keyInput = gameInput.GetInputMovement();
                    if(keyInputPuzzle != Vector2.zero){
                        keyInputPuzzle = Vector2.zero;
                    }
                }
                else if(gameManager.IsInGameType() == WitchGameManager.InGameType.puzzle)
                {
                    if(keyInput != Vector2.zero){
                        keyInput = Vector2.zero;
                        PlayerWalk();
                    }
                    if(gameInput.GetInputMovementPuzzle() != Vector2.zero)
                    {
                        keyInputPuzzle = gameInput.GetInputMovementPuzzle();
                        if(canWalk)
                        {
                            //checker kalo ke arah sini itu tuh bisa dilewatin ga
                            // if(CanMove())
                            // {
                                canWalk = false;
                                PlayerMoveInPuzzle();
                                gameManager.ChangeToCinematic();
                            // }
                            
                            // movePuzzleCooldown = movePuzzleCooldownMax;
                        }
                    }
                    
                }
                
            }
            else if(wasFromOtherInterface && gameInput.GetInputMovement() != Vector2.zero){
                wasFromOtherInterface = false;
            }   
            
        }
        else{
            if(keyInput != Vector2.zero){
                keyInput = Vector2.zero;
                PlayerWalk();
                
            }
            if(keyInputPuzzle != Vector2.zero){
                keyInputPuzzle = Vector2.zero;
            }
            if(!wasFromOtherInterface){
                wasFromOtherInterface = true;
            }
            
        }
    }
    private void FixedUpdate() {
        if(gameManager.IsInGameType() == WitchGameManager.InGameType.normal) PlayerWalk();
    }
    private void PlayerWalk(){
        rb.MovePosition(rb.position + keyInput * speedMovement * Time.fixedDeltaTime);
    }

    public Vector2 GetKeyInput(){
        return keyInput;
    }
    public float GetKeyInputX(){
        return keyInput.x;
    }

    private void PlayerMoveInPuzzle()
    {
        Vector3 playerMovePosition = new Vector3(transform.position.x + keyInputPuzzle.x * totalMoveBlock, transform.position.y + keyInputPuzzle.y * totalMoveBlock, 0f);
        playerAnimator.PlayAnimatorWhileMovingPuzzle(keyInputPuzzle);
        LeanTween.move(this.gameObject, playerMovePosition, durationMovementPuzzle).setOnComplete(
            ()=> FinishMoving()
        );
    }

    private void FinishMoving()
    {
        // Debug.Log("Finish");
        gameManager.ChangeToInGame(WitchGameManager.InGameType.puzzle);
        canWalk = true;
        playerAnimator.PlayAnimatorWhileMovingPuzzle(Vector2.zero);
    }
    private bool CanMove()
    {
        if(keyInputPuzzle.y == 1)
        {
            //kasihtau ke tile manager dr posisi player sekarang, kalo dimajuin tu bisa ga, kek maksudnya apakah tile yg sekarang itu 
        }
        else if(keyInputPuzzle.y == -1)
        {

        }
        else if(keyInputPuzzle.x == 1)
        {

        }
        else if(keyInputPuzzle.x == -1)
        {

        }
        return false;
    }

    public void GetPositionNow(int positionNow)
    {
        playerPuzzlePositionNow = positionNow;
    }


}
