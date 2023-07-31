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
        intro, cauldron_Success, Go_Outside, TutorialCauldron, TutorialChest, TutorialDictionary, TutorialBed, TutorialTumbuk ,none
    }
    private TimelineType type;

    [Header("Cauldron Type")]
    [SerializeField]private TimelineAsset cauldronSuccess;
    [SerializeField]private TimelineAsset goOutside;
    [SerializeField]private TimelineAsset introWalk;
    [SerializeField]private TimelineAsset tutorialCauldron, tutorialChest, tutorialDictionary, tutorialBed, tutorialTumbuk;
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
        // Debug.Log("halo");
        director.playableAsset = introWalk;
        type = TimelineType.intro;
        director.Play();
        // Debug.Log("napa jd masalah jir");
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
        director.Play();
    }
}
