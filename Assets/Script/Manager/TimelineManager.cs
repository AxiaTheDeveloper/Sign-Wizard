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
        intro, cauldron_Success, none
    }
    private TimelineType type;

    [Header("Cauldron Type")]
    [SerializeField]private TimelineAsset cauldronSuccess;
    [SerializeField]private Cauldron cauldron;
    
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
    }

    public void Start_CauldronSuccess(){
        director.playableAsset = cauldronSuccess;
        type = TimelineType.cauldron_Success;
        director.Play();
    }
}
