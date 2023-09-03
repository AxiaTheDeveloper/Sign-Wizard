using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class TimelineManager : MonoBehaviour
{
    public static TimelineManager Instance{get; private set;}
    [SerializeField]private PlayableDirector director;

    public enum TimelineType{
        intro, cauldron_Success, Go_Outside, TutorialCauldron, TutorialChest, TutorialDictionary, TutorialBed, TutorialTumbuk, TutorialSubmit, TutorialPuzzle,none, finishQuest_Diantar1, finishQuest_Diantar2
    }
    private TimelineType type;

    [Header("Cauldron Type")]
    [SerializeField]private TimelineAsset cauldronSuccess;
    [SerializeField]private TimelineAsset goOutside;
    [SerializeField]private TimelineAsset introWalk;
    [SerializeField]private TimelineAsset tutorialCauldron, tutorialChest, tutorialDictionary, tutorialBed, tutorialTumbuk, tutorialSubmit, tutorialPuzzle;
    [SerializeField]private TimelineAsset finishQuestDiantar1, finishQuestDiantar2;
    [SerializeField]private Cauldron cauldron;
    [SerializeField]private FadeNight_StartEnd fadeNight;
    [SerializeField]private DialogueManager dialogueManager;
    [SerializeField]private Animator playerAnimator;
    
    private void Awake() {
        Instance = this;
    }

    private void Start() {
        director.stopped += OnTimelineStopped;
    }

    private void OnTimelineStopped(PlayableDirector director)
    {
        if(type == TimelineType.cauldron_Success){
            type = TimelineType.none;
            cauldron.CreatePotion();
        }
        else if(type == TimelineType.Go_Outside){
            type = TimelineType.none;
            fadeNight.ShowOutsideLight();
        }
        else if(type == TimelineType.intro){
            type = TimelineType.none;
            dialogueManager.ShowDialogue_Intro2();
        }
        else if(type == TimelineType.finishQuest_Diantar1){
            type = TimelineType.none;
            dialogueManager.ShowDialogue_dialogueHolder_FinishQuestDiantar();
        }
        else if(type == TimelineType.finishQuest_Diantar2){
            type = TimelineType.none;
            WitchGameManager.Instance.ChangeToInGame(WitchGameManager.InGameType.normal);
            if(playerAnimator){
                playerAnimator.Play("Player_Idle_Up_Left");
                playerAnimator.SetBool("idle", true);
            }
        }
        else if(type == TimelineType.TutorialCauldron){
            type = TimelineType.none;
            dialogueManager.ShowDialogue_Tutorial(DialogueManager.DialogueTutorial.playerCauldron);
        }
        else if(type == TimelineType.TutorialChest){
            type = TimelineType.none;
            dialogueManager.ShowDialogue_Tutorial(DialogueManager.DialogueTutorial.playerChest);
        }
        else if(type == TimelineType.TutorialDictionary){
            type = TimelineType.none;
            dialogueManager.ShowDialogue_Tutorial(DialogueManager.DialogueTutorial.playerDictionary);
        }
        else if(type == TimelineType.TutorialBed){
            type = TimelineType.none;
            dialogueManager.ShowDialogue_Tutorial(DialogueManager.DialogueTutorial.playerBed);
        }
        else if(type == TimelineType.TutorialTumbuk){
            type = TimelineType.none;
            dialogueManager.ShowDialogue_Tutorial(DialogueManager.DialogueTutorial.playerTumbuk);
        }
        else if(type == TimelineType.TutorialSubmit){
            type = TimelineType.none;
            dialogueManager.ShowDialogue_Tutorial(DialogueManager.DialogueTutorial.playerSubmitPotion);
        }
        else if(type == TimelineType.TutorialSubmit){
            type = TimelineType.none;
            dialogueManager.ShowDialogue_Tutorial(DialogueManager.DialogueTutorial.playerSubmitPotion);
        }
        else if(type == TimelineType.TutorialPuzzle){
            type = TimelineType.none;
            dialogueManager.ShowDialogue_Tutorial(DialogueManager.DialogueTutorial.playerPuzzle);
        }
    }

    public void Start_CauldronSuccess(){
        director.playableAsset = cauldronSuccess;
        type = TimelineType.cauldron_Success;
        director.Play();
    }
    public void Start_GoOutside(){
        director.playableAsset = goOutside;
        type = TimelineType.Go_Outside;
        director.Play();
    }
    public void Start_IntroWalk(){
        director.playableAsset = introWalk;
        type = TimelineType.intro;
        director.Play();
    }
    public void Start_FinishQuest(TimelineType typeFinishQuest){
        type = typeFinishQuest;
        WitchGameManager.Instance.ChangeToCinematic();
        if(type == TimelineType.finishQuest_Diantar1)
        {
            director.playableAsset = finishQuestDiantar1;
        }
        else if(type == TimelineType.finishQuest_Diantar2)
        {
            director.playableAsset = finishQuestDiantar2;
        }
        director.Play();
        
    }

    public void Start_Tutorials(TimelineType typeTutorial){
        type = typeTutorial;
        WitchGameManager.Instance.ChangeToCinematic();
        if(type == TimelineType.TutorialCauldron){
            director.playableAsset = tutorialCauldron;
        }
        else if(type == TimelineType.TutorialChest){
            director.playableAsset = tutorialChest;
        }
        else if(type == TimelineType.TutorialDictionary){
            director.playableAsset = tutorialDictionary;
        }
        else if(type == TimelineType.TutorialBed){
            director.playableAsset = tutorialBed;
        }
        else if(type == TimelineType.TutorialTumbuk){
            director.playableAsset = tutorialTumbuk;
        }
        else if(type == TimelineType.TutorialSubmit){
            director.playableAsset = tutorialSubmit;
        }
        else if(type == TimelineType.TutorialPuzzle){
            director.playableAsset = tutorialPuzzle;
        }
        director.Play();
    }
}
