using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerDirection{
    Left, Right
}
public class PuzzleToTown_Pembatas : MonoBehaviour
{
    [SerializeField]private WitchGameManager gameManager;
    [SerializeField]private PlayerAnimator playerAnimator;
    [SerializeField]private GameObject player;
    [SerializeField]private PlayerDirection direction;

    private Vector3 NextPosition1, NextPosition2;
    [SerializeField]private float playerMoveDuration1,playerMoveDuration2;

    //kan jadi pembatas bakal dibikin per block, nanti bakal ada penanda apakah tile di depannya bisa dimasukkin player ato ga, kalo gabisa ntr muncul dialog dan ga usah animasi maju, jd di start tuh, start ama finish semuanya bakal di set, trus kalo ada perubahan di bagian startpos ama finishpos baru bakal dicek pembatas di tile itu

    private void Start() 
    {
        if(gameManager == null)gameManager = WitchGameManager.Instance;
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player") && gameManager.IsInGameType() == WitchGameManager.InGameType.normal)
        {
            gameManager.ChangeToInGame(WitchGameManager.InGameType.puzzle);
            gameManager.ChangeToCinematic();
            if(direction == PlayerDirection.Right)
            {
                if(other.transform.position.y >= NextPosition1.y)
                {
                    playerAnimator.PlayAnimatorCinematic(new Vector2(1,0));
                }
                else
                {
                    playerAnimator.PlayAnimatorCinematic(new Vector2(1,1));
                }
            }
            else if (direction == PlayerDirection.Right)
            {
                if(other.transform.position.y >= NextPosition1.y)
                {
                    playerAnimator.PlayAnimatorCinematic(new Vector2(-1,0));
                }
                else
                {
                    playerAnimator.PlayAnimatorCinematic(new Vector2(-1,1));
                }
            }
            
            
            LeanTween.move(player, NextPosition1, playerMoveDuration1).setOnComplete(
                ()=> FinishMove()
            );
        }
    }
    private void FinishMove()
    {
        if(direction == PlayerDirection.Right)
        {
            playerAnimator.PlayAnimatorCinematic(new Vector2(1,0));
        }
        else if(direction == PlayerDirection.Left)
        {
            playerAnimator.PlayAnimatorCinematic(new Vector2(-1,0));
        }
        LeanTween.move(player, NextPosition2, playerMoveDuration2).setOnComplete(
            ()=> FinishMove2()
        );

    }
    private void FinishMove2()
    {
        playerAnimator.PlayAnimatorWhileMovingPuzzle(Vector2.zero);
        gameManager.ChangeToInGame(WitchGameManager.InGameType.puzzle);
    }
    public PlayerDirection GetPlayerDirectionFrom()
    {
        return direction;
    }
    public void SetNextPosition(Vector3 tilePosition)
    {
        NextPosition2 = new Vector3(tilePosition.x, tilePosition.y+1,0f);
        NextPosition1 = new Vector3(transform.position.x,tilePosition.y+1,0f); 
    }
}
