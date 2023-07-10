using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class TimelineManager : MonoBehaviour
{
    public static TimelineManager Instance{get; private set;}
    [SerializeField]private PlayableDirector director;

    private enum TimelineType{
        intro, cauldron_Success, Go_Outside,none
    }
    private TimelineType type;

    [Header("Cauldron Type")]
    [SerializeField]private TimelineAsset cauldronSuccess;
    [SerializeField]private TimelineAsset goOutside;
    [SerializeField]private TimelineAsset introWalk;
    [SerializeField]private Cauldron cauldron;
    [SerializeField]private FadeNight_StartEnd fadeNight;
    
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
        if(type == TimelineType.Go_Outside){
            type = TimelineType.none;
            fadeNight.ShowOutsideLight();
        }
        if(type == TimelineType.intro){
            type = TimelineType.none;
            DialogueManager.Instance.ShowDialogue_Intro2();
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
}
