using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderVisualVii : MonoBehaviour
{
    private Animator animator;
    [SerializeField]private GameObject PlayerChara;
    private PlayerAnimator playerAnimator;
    [SerializeField]private Vector3 playerStandPoint;
    [SerializeField]private float playerMoveDuration;
    private GameObject player;
    [SerializeField]private float batasY, batasXKiri,batasXKanan;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update() 
    {
        if(player)
        {
            if(player.transform.position.y >= batasY)
            {
                animator.Play("IdleFront_Vii");
                // animator.Play("IdleLeft_Vii");
                // animator.Play("IdleRight_Vii");
                // Debug.Log("Di belakang");
            }
            else
            {
                if(player.transform.position.x > batasXKiri && player.transform.position.x < batasXKanan)
                {
                    animator.Play("IdleFront_Vii");
                    // Debug.Log("Di depan Pas");
                }
                else if(player.transform.position.x <= batasXKiri)
                {
                    animator.Play("IdleLeft_Vii");
                    // Debug.Log("Di depan Kiri");
                }
                else if(player.transform.position.x >= batasXKanan)
                {
                    animator.Play("IdleRight_Vii");
                    // Debug.Log("Di depan Kanan");
                }
            }
        }
    }
    public void PlayerMoveBackAfterDialogue()
    {
        playerAnimator = PlayerChara.GetComponentInChildren<PlayerAnimator>();
        playerAnimator.changeLastInGameType(WitchGameManager.InGameType.puzzle);
        if(PlayerChara.transform.position != playerStandPoint)
        {
            if(PlayerChara.transform.position.y > playerStandPoint.y)
            {
                if(PlayerChara.transform.position.x == playerStandPoint.x)
                {
                    playerAnimator.PlayAnimatorCinematic(new Vector2(0, -1));
                }
                else if(PlayerChara.transform.position.x > playerStandPoint.x)
                {
                    playerAnimator.PlayAnimatorCinematic(new Vector2(-1, 0));
                }
                else if(PlayerChara.transform.position.x < playerStandPoint.x)
                {
                    playerAnimator.PlayAnimatorCinematic(new Vector2(1, -1));
                }
                
            }
            else
            {
                if(PlayerChara.transform.position.x > playerStandPoint.x)
                {
                    playerAnimator.PlayAnimatorCinematic(new Vector2(-1, 0));
                }
                else if(PlayerChara.transform.position.x < playerStandPoint.x)
                {
                    playerAnimator.PlayAnimatorCinematic(new Vector2(1, -1));
                }
            }
        }
        LeanTween.move(PlayerChara, playerStandPoint, playerMoveDuration).setOnComplete(
            ()=> FinishMovingPlayer()
        );
        
    }
    public void FinishMovingPlayer()
    {
        playerAnimator.animator.Play("Player_Idle_Up");
        playerAnimator.animator.SetBool("idle", true);
        playerAnimator.PlayAnimatorCinematic(new Vector2(0, 0));
        DialogueManager.Instance.ShowDialogue_ChatWithTravelingMerchant2();
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            player = null;
            animator.Play("IdleFront_Vii");
        }
    }
}
